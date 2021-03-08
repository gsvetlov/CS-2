using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    static class GameObjectValidator
    {
        private static readonly int maxSpeed = 20;
        private static readonly int maxSize = 100;
        public static void Validate(Point position, Point direction, Size size)
        {
            if (position.X < 0 || position.X > Game.Width || position.Y < 0 || position.Y > Game.Height)
                throw new GameObjectException(nameof(position));
            if (direction.X < -maxSpeed || direction.Y < -maxSpeed || direction.X > maxSpeed || direction.Y > maxSpeed)
                throw new GameObjectException(nameof(direction));
            if (size.Height < 1 || size.Height > maxSize || size.Width < 1 || size.Width > maxSize)
                throw new GameObjectException(nameof(size));
        }
    }
}
