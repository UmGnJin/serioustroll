using System.Collections;
using System.Collections.Generic;
using noname.util;
using noname.painters;

namespace noname.rooms
{
    public class UpStairsRoom : Room
    {

        public UpStairsRoom()
        {
            width = 6;
            height =6;
            DefaultSet();
        }
        public override void Paint(Level l)
        {
            UpStairRoomPainter erg = new UpStairRoomPainter();
            erg.Paint(l, this);
        }
    }
}