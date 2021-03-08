using System;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    class Medikit : SpaceBody, IHealable
    {
        public Medikit(Point pos, Point dir, Size size, Action<String> log) : base(pos, dir, size, log)
        {
            image = new Bitmap(Resources.medikit, size);
            image.MakeTransparent();
        }

        public override void Collide(SpaceBody other)
        {
            log("Medikit consumed");
            Die();
        }
    }
}
