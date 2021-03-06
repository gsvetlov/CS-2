using System.Drawing;

namespace Asteroid
{
    public interface ICollidable<T>
    {
        Rectangle CollisionRange { get; }
        bool HasCollision(ICollidable<T> other);
        void Collide(T other);

    }
}