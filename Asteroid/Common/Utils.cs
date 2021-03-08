using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    static class Utils
    {
        private static readonly Random random = new Random();
        public static int Random(int max) => random.Next(max);
        public static int Random(int min, int max) => random.Next(min, max);
        public static Point RandomPos(int width, int height) => new Point(random.Next(width), random.Next(height));
        public static Point RandomDir(int min, int max, bool doubleSide = false)
        {
            int x;
            int y;
            if (doubleSide)
            {
                x = GetSides();
                y = GetSides();
            }
            else
            {
                x = random.Next(min, max);
                y = random.Next(min, max);
            }

            return new Point(x, y);

            int GetSides()
            {
                int v = random.Next(-max, max);
                if (v < min && v > -min) return min;
                return v;
            }
        }
    }
}
