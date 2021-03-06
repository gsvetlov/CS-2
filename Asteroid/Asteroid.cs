using System.Diagnostics;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    public class Asteroid : SpaceBody
    {
        private static Bitmap[] _bitmaps = { Resources.asteroid01t, Resources.asteroid03t };
        private readonly Bitmap _image;
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            int selector = Utils.Random(_bitmaps.Length);
            _image = new Bitmap(_bitmaps[selector], size.Width, size.Height);
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
                Debug.WriteLine("Asteroid collided!");
            }
            else
            {
                Die();
                Debug.WriteLine("Asteroid died!");
            }
        }
    }
}
