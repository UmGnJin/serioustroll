using System;

namespace noname.util
{
    public class Point 
    {
        public int x, y;

        public Point() { }
        public Point(Point p)
        {
            x = p.x;
            y = p.y;
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point Set(int x, int y)
        {
            this.x = x;
            this.y = y;
            return this;
        }
        public Point Set(Point p)
        {
            x = p.x;
            y = p.y;
            return this;
        }
        public Point Clone()
        {
            return new Point(this);
        }
        public Point Scale(int n)
        {
            x *= n;
            y *= n;
            return this;
        }
        public Point Offset(int x, int y)
        {
            this.x += x;
            this.y += y;
            return this;
        }
        public Point Offset(Point p)
        {
            x += p.x;
            y += p.y;
            return this;
        }
        public float Distance(Point p)
        {
            return (float)Math.Sqrt(Math.Pow(p.x - x, 2) + Math.Pow(p.y - y, 2));
        }
        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point p = (Point)obj;
                return p.x == x && p.y == y;
            }
            else
                return false;
        }
    }
}