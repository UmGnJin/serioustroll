using noname.util;
using System.Collections.Generic;
using System;
using noname.rooms;
using noname.painters;
using UnityEngine;
using Random = System.Random;
using Rect = noname.util.Rect;
using System.Linq;

namespace noname
{
    public enum LevelSize
    {
        SMALL = 4,
        NORMAL = 10,
        LARGE = 16
        //SMALL(4~8), NORMAL(10~14), LARGE(16~20)
    }

    public class Level 
    {
        public int width, height, length;
        public int[] map;

        public int entrance;
        public int exit;
        public List<Room> rooms;
        Painter painter;

        public LevelSize levelsize = LevelSize.NORMAL;
        public int roomNum;
        public Rect levelr;
        public static Random rand = new Random();
        public void Create()
        {
            Random random = new Random();
            roomNum = random.Next(0, 6) + (int)levelsize;
            InitRooms();

            foreach(Room r1 in rooms)
            {
                foreach(Room r2 in rooms)
                {
                    if (r1.GetHashCode() == r2.GetHashCode())
                        continue;
                    if(r1.IsNeighbour(r2))
                    {
                        r1.Connect(r2);
                        r2.Connect(r1);
                    }
                }
            }

            PaintRooms();
            foreach (Room r in rooms)
                Debug.Log(r.Info());
        }
        public void InitRooms()
        {
            rooms = new List<Room>();
            rooms.Add(new UpStairsRoom());
            for(int i = 0; i < roomNum - 2; i++)
            {
                rooms.Add(new EmptyRoom());
            }
            rooms.Add(new DownStairsRoom());

            PlaceRooms();
            levelr = LevelRect();
            MoveRooms();
            map = new int[length];
            for (int i = 0; i < length; i++)
                map[i] = Terrain.WALL;
        }
        public void PlaceDoors(Room r)
        {
            foreach(Room room in r.connection.Keys.ToList())
            {
                var door = r.connection[room];
                if (door != null)
                    continue;
                var i = r.Intersect(r, room);
                if (i.Width() == 0)
                    door = new Room.Door(i.x - 1, rand.Next(i.y + 1, i.yMax - 1));
                else
                    door = new Room.Door(rand.Next(i.x + 1, i.xMax - 1), i.y - 1);

                if (r.connection.ContainsKey(room))
                    r.connection[room] = door;
                else
                    r.connection.Add(room, door);
                room.connection[r] = door;
            }
        }
        protected internal virtual void PaintDoors(Room r)
        {
            foreach (var n in r.connection.Keys)
            {
               
                var d = r.connection[n];
                var door = d.x + d.y * width;

                switch (d.type)
                {
                    case Room.Door.Type.EMPTY:
                        map[door] = Terrain.EMPTY;
                        break;
                    case Room.Door.Type.REGULAR:
                        map[door] = Terrain.DOOR;
                        break;
                }
            }
        }
        public void PlaceRooms()
        {
            rooms[0].SetPosition(0, 0);
            Room prev = rooms[0];
            rooms[0].placed = true;
            foreach (Room r in rooms)
            {
                if (prev.GetHashCode() == r.GetHashCode())
                    continue; 

                r.SetPosition(prev.x, prev.y);
                double angle = rand.NextDouble() * 2 * Math.PI;                               
                int xOrigin = r.x;
                int yOrigin = r.y;
                int count = 1;
                while (CheckOverlap(r))
                {
                    r.SetPosition(xOrigin + (int)(count * Math.Sin(angle)), yOrigin + (int)(count * Math.Cos(angle)));
                    count++;
                    if (count > 100)
                        break;
                }
                count = 0;
                int max = 0;
                while(!r.IsNeighbour(prev))
                {
                    Rect rect = r.Intersect(r, prev);
                    int xDir = 0, yDir = 0;

                    if(rect.Width() < 0)
                        xDir = -rect.Width();
                    else if(rect.Width() > 0 && rect.Width() < 3)
                        xDir = 3 - rect.Width();

                    if(rect.Height() < 0)
                        yDir = -rect.Height();
                    else if (rect.Height() > 0 && rect.Height() < 3)
                        yDir = 3 - rect.Height();

                    if (rect.Width() == 0 && rect.Height() == 0)
                    {
                        //Debug.Log(".");
                        bool hor = rand.Next(2) == 0 ? true : false;
                        if (hor)
                            xDir = 3;
                        else
                            yDir = 3;
                    }
                    if (r.y > prev.y)
                        yDir *= -1;
                    if (r.x > prev.x)
                        xDir *= -1;

                    int index = -1;
                    if (MoveRoom(r, xDir, yDir, out index) && r.IsNeighbour(prev))
                    {
                        //rect = r.Intersect(r, prev);
                        break;
                    }
                    if (max++ > 2)
                    {
                        if (count++ > rooms.Count)
                        {
                            Debug.Log("failed to attach room " + rooms.IndexOf(r) + ", " + rect.Width() + " " + rect.Height());
                            break;
                        }
                        if(index != -1)
                            prev = rooms[index];
                        max = 0;
                        continue;
                    }
                    
                }
                r.placed = true;
                prev = r;
            }   
            
        }
        public bool MoveRoom(Room r, int xDir, int yDir, out int index)
        {
            r.MovePosition(xDir, yDir);
            int i = -1;
            if(CheckOverlap(r, out i))
            {
                r.MovePosition(-xDir, -yDir);
                index = i;
                return false;
            }
            index = -1;
            return true;
        }
        public bool CheckOverlap(Room r)
        {
            foreach(Room r1 in rooms)
            {
                if(r.GetHashCode() == r1.GetHashCode() || !r1.placed)
                    continue;
                Rect rect = r.Intersect(r, r1);
                if (rect.Width() > 0 && rect.Height() > 0)
                {
                    //Debug.Log("Intersects with Room number " + rooms.IndexOf(r1) + ", Intersect Range : " + rect.Width() + ", " + rect.Height());
                    return true;
                }
            }
            return false;
        }
        public bool CheckOverlap(Room r, out int index)
        {
            foreach (Room r1 in rooms)
            {
                if (r.GetHashCode() == r1.GetHashCode() || !r1.placed)
                    continue;
                Rect rect = r.Intersect(r, r1);
                if (rect.Width() > 0 && rect.Height() > 0)
                {
                    //Debug.Log("Intersects with Room number " + rooms.IndexOf(r1) + ", Intersect Range : " + rect.Width() + ", " + rect.Height());
                    index = rooms.IndexOf(r1);
                    return true;
                }
            }
            index = -1;
            return false;
        }
        public void PaintRooms()
        {
            foreach(Room r in rooms)
            {
                PlaceDoors(r);
                r.Paint(this);
                PaintDoors(r);
            }
        }
        public Rect LevelRect()
        {
            Rect rect = new Rect();
            foreach(Room r in rooms)
            {
                if(r.x < rect.x)
                    rect.x = r.x;
                if (r.y < rect.y)
                    rect.y = r.y;
                if (r.xMax > rect.xMax)
                    rect.xMax = r.xMax;
                if (r.yMax > rect.yMax)
                    rect.yMax = r.yMax;
            }
            rect.x -= 1;
            rect.y -= 1;
            width = rect.Width();
            height = rect.Height();
            length = width * height;
            return rect;
        }
        public void MoveRooms()
        {
            int xDir = -levelr.x;
            int yDir = -levelr.y;

            levelr.SetPosition(-1, -1);
            foreach(Room r in rooms)
            {
                r.MovePosition(xDir, yDir);
            }
        }
        
    }
}