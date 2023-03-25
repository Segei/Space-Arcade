using System;
using System.Numerics;

namespace Script.Model.Physics
{
    public class Collision
    {
        private Transformable transformable;
        public Vector2 Center => transformable.Position;

        public Vector2 HalfSize;

        public Collision(Transformable value)
        {
            transformable = value;
        }

        public bool CollisionDetected(Collision other)
        {
            return !(HalfSize.X == 0.0f || HalfSize.Y == 0.0f || other.HalfSize.X == 0.0f || other.HalfSize.Y == 0.0f
        || (Math.Abs(Center.X - other.Center.X) > HalfSize.X + other.HalfSize.X
        || Math.Abs(Center.Y - other.Center.Y) > HalfSize.Y + other.HalfSize.Y));
        }


    }
}
