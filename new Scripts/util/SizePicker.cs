using System;
using System.Collections.Generic;

namespace noname.util
{
    public class SizePicker
    {
        static Random r = new Random();

        public int Pick(int min, int max)
        {
            if (max <= min)
                return -1;

            List<int> list = new List<int>();
            while(max - min > 1)
            {
                for (int i = min; i <= max; i++)
                {
                    list.Add(i);
                }
                min++;
                max--;
            }
            return list[r.Next(list.Count)];
        }
    }
}