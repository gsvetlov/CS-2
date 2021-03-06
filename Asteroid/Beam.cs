using System.Diagnostics;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    class Beam : SpaceBody
    {
        private static Bitmap[] _bitmaps = { Resources.laser01t };
        private readonly Bitmap _image;

        public Beam(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _image = new Bitmap(Resources.laser01t, size.Width, size.Height);
            _image.MakeTransparent();
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y);
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (IsOutOfScreen(Pos.X, -Size.Width, Game.Width) || IsOutOfScreen(Pos.Y, -Size.Height, Game.Height))
            {
                Die();
                Debug.WriteLine("Beam is out of screen");
            }
        }
        public override void Collide(SpaceBody other)
        {
            Debug.WriteLine("Beam died");
            Die();
        }
    }
}
