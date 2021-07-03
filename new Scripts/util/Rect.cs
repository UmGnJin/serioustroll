using System;

namespace noname.util
{
    public class Rect
    {
        public int x, y, xMax, yMax;

        public Rect() : this(0, 0, 0 ,0)
        {

        }
        public Rect(Rect r)
        {
            x = r.x;
            y = r.y;
            xMax = r.xMax;
            yMax = r.yMax;
        }
        public Rect(int x, int y, int xMax, int yMax)
        {
            this.x = x;
            this.y = y;
            this.xMax = xMax;
            this.yMax = yMax;
        }
        public void Set(int x, int y, int xMax, int yMax)
        {
            this.x = x;
            this.y = y;
            this.xMax = xMax;
            this.yMax = yMax;
        }
        public void SetPosition(int x, int y)
        {
            int dx = this.x - x;
            int dy = this.y - y;
            this.x = x;
            this.y = y;
            xMax -= dx;
            yMax -= dy;
        }
        public void MovePosition(int x, int y)
        {
            this.x += x;
            this.y += y;
            xMax += x;
            yMax += y;
        }

        public virtual int Width() { return xMax - x; }
        public virtual int Height() { return yMax - y; }

        
        public Rect Intersect(Rect r1, Rect r2)
        {
            Rect r = new Rect();
            r.x = Math.Max(r1.x, r2.x);
            r.y = Math.Max(r1.y, r2.y);
            r.xMax = Math.Min(r1.xMax, r2.xMax);
            r.yMax = Math.Min(r1.yMax, r2.yMax);

            return r;
        }
    }
}