using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asteroid.Properties;

namespace Asteroid
{
    class Planet : SpaceBody
    {
        private static Random random = new Random();
        private static Bitmap[] _bitmaps = { Resources.planet01t, Resources.planet02t, Resources.planet03t };
        private readonly Bitmap _image;

        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            int select = random.Next(_bitmaps.Length);
            _image = new Bitmap(_bitmaps[select], size.Width, size.Height);
            _image.MakeTransparent();
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y);
        }        
    }
}
