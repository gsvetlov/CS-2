using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Asteroid
{
    public abstract class SpaceBody : ICollidable<SpaceBody>
    {        
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public SpaceBody(Point pos, Point dir, Size size)
        {
            GameObjectValidator.Validate(pos, dir, size);
            Pos = pos;
            Dir = dir;
            Size = size;
        }        

        public bool isTerminated { get; protected set; } = false;

        public abstract void Draw();

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

        public virtual bool HasCollision(ICollidable<SpaceBody> other)
        {
            if (!this.Equals(other) && CollisionRange.IntersectsWith(other.CollisionRange))
            {
                Debug.WriteLine($"Collision at {CollisionRange.X},{CollisionRange.Y}:{CollisionRange.Width},{CollisionRange.Height} with {other.CollisionRange.X},{other.CollisionRange.X}:{other.CollisionRange.Width},{other.CollisionRange.Height}");
                return true;
            }
            return false;
        }
        public virtual void Collide(SpaceBody other)
        {
            // над столкновениями надо поработать, так как сейчас объекты "влетают" внутрь друг друга и вызывают цепочку столкновений
            // пока разведём их рандомно в стороны
            Dir = Utils.RandomDir(1, Math.Max(Math.Abs(Dir.X), Math.Abs(Dir.Y)), true);
            other.Dir = Utils.RandomDir(1, Math.Max(Math.Abs(other.Dir.X), Math.Abs(other.Dir.Y)), true);
        }
        protected void Die()
        {
            isTerminated = true;
            Dir = Point.Empty;
        }
       
    }
}
