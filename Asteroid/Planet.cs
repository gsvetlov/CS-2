using System.Diagnostics;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    class Planet : SpaceBody
    {
        private static Bitmap[] _bitmaps = { Resources.planet01t, Resources.planet02t, Resources.planet03t };
        private readonly Bitmap _image;

        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            int select = Utils.Random(_bitmaps.Length);
            _image = new Bitmap(_bitmaps[select], size.Width, size.Height);
            _image.MakeTransparent();
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y);
        }

        public override void Collide(SpaceBody other)
        {
            if (other.GetType() == this.GetType())
            {
                base.Collide(other);
                Debug.WriteLine("Planets collided!");
            }

        }
    }
}
