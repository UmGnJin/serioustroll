using System.Collections;
using noname.util;
using System.Collections.Generic;
using noname.painters;

namespace noname.rooms
{
    public class DownStairsRoom : Room
    {

        public DownStairsRoom()
        {
            width = 4;
            height = 4;
            DefaultSet();
        }
        public override void Paint(Level l)
        {
            DownStairRoomPainter erg = new DownStairRoomPainter();
            erg.Paint(l, this);
        }

    }
}