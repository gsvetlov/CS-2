using System;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    class Beam : SpaceBody
    {
        public Beam(Point pos, Point dir, Size size, Action<string> log) : base(pos, dir, size, log)
        {
            image = new Bitmap(Resources.laser01t, size.Width, size.Height);
            image.MakeTransparent();
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (IsOutOfScreen(Pos.X, -Size.Width, Game.Width) || IsOutOfScreen(Pos.Y, -Size.Height, Game.Height))
            {
                log("Beam is off screen");
                Die();
            }
        }
        public override void Collide(SpaceBody other)
        {
            if (other is IScoreable)
                Game.Score++;
            Die();
        }
    }
}
