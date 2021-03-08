using System;
using System.Drawing;

namespace Asteroid
{
    public abstract class SpaceBody : ICollidable<SpaceBody>
    {
        protected Action<String> log;
        protected Bitmap image;
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public SpaceBody(Point pos, Point dir, Size size, Action<string> log)
        {
            GameObjectValidator.Validate(pos, dir, size);
            Pos = pos;
            Dir = dir;
            Size = size;
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.log($"{GetType()} created");
        }

        public bool IsTerminated { get; protected set; } = false;

        public virtual void Draw() => Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y);

        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;

            if (IsOutOfScreen(Pos.X, Size.Width, Game.Width))
                Dir.X = -Dir.X;
            if (IsOutOfScreen(Pos.Y, Size.Height, Game.Height))
                Dir.Y = -Dir.Y;
        }

        protected virtual bool IsOutOfScreen(int x, int size, int screenSize) => x < 0 || x > screenSize - size;

        public virtual Rectangle CollisionRange => new Rectangle(Pos, Size);

        public virtual bool HasCollision(ICollidable<SpaceBody> other) =>
            !this.Equals(other) && CollisionRange.IntersectsWith(other.CollisionRange);
        
        public virtual void Collide(SpaceBody other)
        {
            // над столкновениями надо поработать, так как сейчас объекты "влетают" внутрь друг друга и вызывают цепочку столкновений
            // пока разведём их рандомно в стороны
            Dir = Utils.RandomDir(1, Math.Max(Math.Abs(Dir.X), Math.Abs(Dir.Y)), true);
            other.Dir = Utils.RandomDir(1, Math.Max(Math.Abs(other.Dir.X), Math.Abs(other.Dir.Y)), true);
        }
        protected void Die()
        {
            log($"{GetType()} has died");
            IsTerminated = true;
            Dir = Point.Empty;
        }

    }
}
