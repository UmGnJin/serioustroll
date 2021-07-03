using System.Collections;
using System.Collections.Generic;
using System;
using noname.painters;

namespace noname.rooms
{
    public abstract class StandardRoom : Room
    {
        public Painter painter;

        private static List<Type> rooms = new List<Type>
        {
            typeof(EmptyRoom),
        };

        
    }
}