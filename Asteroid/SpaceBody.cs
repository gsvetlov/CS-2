using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    public abstract class SpaceBody
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public SpaceBody(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public abstract void Draw();

        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;

            if (Pos.X < Size.Width || Pos.X > Game.Width - Size.Width) 
                Dir.X = -Dir.X;            
            if (Pos.Y < Size.Height || Pos.Y > Game.Height - Size.Width)
                Dir.Y = -Dir.Y;
            
        }
    
    }
}
