using System.Collections;
using System.Collections.Generic;
using noname.rooms;

namespace noname.util
{
    public class RNG 
    {
        public static void GenerateRNG(List<Room> rooms)
        {
            foreach(Room room in rooms)
            {
                foreach(Room room2 in rooms)
                {
                    if (room.GetHashCode() == room2.GetHashCode())
                        continue;
                    if(Neighbours(room, room2, rooms))
                    {
                        room.Connect(room2);
                        room2.Connect(room);
                    }
                }
            }
        }
        private static bool Neighbours(Room r1, Room r2, List<Room> rooms)
        {
            float distance = r1.Center().Distance(r2.Center());
            foreach(Room r in rooms)
            {
                if ((r.GetHashCode() == r1.GetHashCode()) || (r.GetHashCode() == r2.GetHashCode()))
                    continue;
                if (r1.Center().Distance(r.Center()) < distance && r2.Center().Distance(r.Center()) < distance)
                    return false;
            }
            return true;
        }
    }
}