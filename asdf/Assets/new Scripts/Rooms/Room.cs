using noname.util;
using System.Collections.Generic;
using System;

namespace noname.rooms
{
    public abstract class Room : Rect
    {
        public Dictionary<Room, Door> connection = new Dictionary<Room, Door>();
        public const int MINROOMSIZE = 3;
        public const int MAXROOMSIZE = 9;
        public int width, height;
        public bool placed = false;

        public bool IsNeighbour(Room r)
        {
            Rect rect = Intersect(this, r);
            if ((rect.Width() != 0 || rect.Height() < 3) && (rect.Height() != 0 || rect.Width() < 3))
                return false;


            return true;
        }

        public void Connect(Room r)
        {
            if (connection.ContainsKey(r))
                return;
            connection.Add(r, null);
            r.connection.Add(this, null);
        }


        public void DefaultSet()
        {
            x = 0;
            y = 0;
            xMax = x + width;
            yMax = y + height;
        }
        public String Info()
        {
            return "Position : (" + (x - 1) + ", " + (y + 1) + "), " +"width : " + Width() + ", height : " + Height();
        }

        public Point Center()
        {
            return new Point((x + xMax) / 2, (y + yMax) / 2);
        }
        public abstract void Paint(Level l);

        public class Door : Point
        {
            public enum Type
            { 
                EMPTY, REGULAR
            }
            public Type type = Type.REGULAR;

            public Door() { }
            public Door(Point p) : base(p) { }
            public Door(int x, int y) : base(x, y) { }

            public void Set(Type t)
            {
                if (t.CompareTo(type) > 0)
                    type = t;
            }
        }

    }
}
