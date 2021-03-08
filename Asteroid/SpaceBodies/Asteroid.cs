using System;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    public class Asteroid : SpaceBody, IScoreable
    {
        private static readonly Bitmap[] bitmaps = { Resources.asteroid01t, Resources.asteroid03t };
      
        public Asteroid(Point pos, Point dir, Size size, Action<string> log) : base(pos, dir, size, log)
        {
            int selector = Utils.Random(bitmaps.Length);
            image = new Bitmap(bitmaps[selector], size.Width, size.Height);
            image.MakeTransparent();
        }
        public override void Collide(SpaceBody other)
        {
            if (other is Asteroid)
                base.Collide(other);
            else
                Die();
        }
    }
}
