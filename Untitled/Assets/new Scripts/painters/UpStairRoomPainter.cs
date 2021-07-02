using noname.rooms;
using System;
using System.Collections.Generic;
using noname.util;

namespace noname.painters
{
    public class UpStairRoomPainter : Painter
    {
        public static Random rand = new Random();
        public override void Paint(Level l, Room r)
        {
            int pos = r.y * l.width + r.x;
            int x = rand.Next(r.x + 1, r.xMax - 2);
            int y = rand.Next(r.y + 1, r.yMax - 2);
            int door = y * l.width + x;
            bool stair = false;
            for (int i = 0; i < r.Height(); i++, pos += l.width)
            {
                for (int j = pos; j < pos + r.Width(); j++)
                {
                    int tile;
                    tile = Terrain.GROUND;
                    if (i == r.Height() - 1 || j == r.Width() + pos - 1)
                        tile = Terrain.WALL;
                    if(!stair && j == door)
                    {
                        stair = true;
                        tile = Terrain.STAIRS_UP;
                    }
                    l.map[j] = tile;
                }
            }
        }
    }
}