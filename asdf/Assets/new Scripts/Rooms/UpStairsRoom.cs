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
            SizePicker sp = new SizePicker();
            width = sp.Pick(MINROOMSIZE + 1, MAXROOMSIZE - 2);
            height = sp.Pick(MINROOMSIZE + 1, MAXROOMSIZE - 2);
            DefaultSet();
        }
        public override void Paint(Level l)
        {
            UpStairRoomPainter erg = new UpStairRoomPainter();
            erg.Paint(l, this);
        }
    }
}