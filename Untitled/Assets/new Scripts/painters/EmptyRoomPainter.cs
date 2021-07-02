using System.Collections;
using System.Collections.Generic;
using noname.rooms;

namespace noname.painters
{
    public class EmptyRoomPainter : Painter
    {
        public override void Paint(Level l, Room r)
        {
            int pos = r.y  * l.width + r.x;
            for (int i = 0; i < r.Height(); i++, pos += l.width)
            {
                for (int j = pos; j < pos + r.Width(); j++)
                {
                    int tile;
                    tile = Terrain.GROUND;
                    if (i == r.Height() - 1 || j == r.Width() + pos - 1)
                        tile = Terrain.WALL;
                    l.map[j] = tile;
                }
            }
        }
    }
}