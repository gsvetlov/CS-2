using System;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    class Planet : SpaceBody
    {
        public Planet(Point pos, Point dir, Size size, Action<string> log) : base(pos, dir, size, log)
        {
            int select = Utils.Random(Bitmaps.Length);
            image = new Bitmap(Bitmaps[select], size.Width, size.Height);
            image.MakeTransparent();
        }

        public static Bitmap[] Bitmaps { get; } = { Resources.planet01t, Resources.planet02t, Resources.planet03t };

        public override void Collide(SpaceBody other)
        {
            if (other is Planet)
            {
                base.Collide(other);
                log("Planets collided!");
            }

        }
    }
}
