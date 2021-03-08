using System;
using System.Drawing;

using Asteroid.Properties;

namespace Asteroid
{
    class Ship : SpaceBody
    {        
        private int hp = 100;
        public UIController Controller { get; }

        public Ship(Point pos, Point dir, Size size, Action<string> log, UIController controller) : base(pos, dir, size, log)
        {
            Controller = controller ?? throw new ArgumentNullException(nameof(controller));
            controller.ChangePositon += Ship_ChangePositon;
            image = new Bitmap(Resources.ship, size);
            image.MakeTransparent();

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
            Game.Buffer.Graphics.DrawString($"HP: {hp}", SystemFonts.DefaultFont, Brushes.Yellow, Pos.X, Pos.Y - 20);
        }

        public override void Update() { }
        public override void Collide(SpaceBody other)
        {
            if (other is IScoreable)
            {
                Game.Score++;
                log("Ship scored");
            }
            if (other is IHealable)
            {
                hp += 25;
                log("Ship healed");
            }
            else
            {
                log("Ship damaged");
                hp -= 25;
            }
            if (hp <= 0) Die();
        }

        public Point GetFiringPosition()
        {
            return new Point(Pos.X + Size.Width, Pos.Y + Size.Height / 2);
        }
        private void Ship_ChangePositon(object sender, PositionChangeEventArgs e)
        {
            switch (e.Direction)
            {
                case Direction.Up:
                    Pos.Y -= Dir.Y;
                    break;
                case Direction.Down:
                    Pos.Y += Dir.Y;
                    break;
                case Direction.Left:
                    Pos.X -= Dir.X;
                    break;
                case Direction.Right:
                    Pos.X += Dir.X;
                    break;
                default:
                    break;
            }
            if (Pos.Y < 0)
                Pos.Y = 0;
            if (Pos.Y > Game.Height - Size.Height)
                Pos.Y = Game.Height - Size.Height;
            if (Pos.X < 0)
                Pos.X = 0;
            if (Pos.X > Game.Width - Size.Width)
                Pos.X = Game.Width - Size.Width;
        }
    }
}
