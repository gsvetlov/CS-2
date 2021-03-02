using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asteroid.Properties;

namespace Asteroid
{
    public class Asteroid : SpaceBody
    {
        private static Random random = new Random();
        private static Bitmap[] _bitmaps = { Resources.asteroid01t, Resources.asteroid03t };
        private readonly Bitmap _image;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            int selector = random.Next(_bitmaps.Length);
            _image = new Bitmap(_bitmaps[selector], size.Width, size.Height);
            _image.MakeTransparent();
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

    }
}
