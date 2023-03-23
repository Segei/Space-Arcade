using System;
using System.Numerics;

namespace Script.Model.Physics
{
    public class Collision
    {
        private Transformable transformable;
        public Vector2 Center => transformable.Position;

        public float HalfWidth;
        public float HalfHeight;


        public Collision(Transformable value)
        {
            transformable = value;
        }

        public bool CollisionDetected(Collision other)
        {
            return !(HalfWidth == 0.0f || HalfHeight == 0.0f || other.HalfWidth == 0.0f || other.HalfHeight == 0.0f
        || Math.Abs(Center.X - other.Center.X) > HalfWidth + other.HalfWidth
        || Math.Abs(Center.Y - other.Center.Y) > HalfHeight + other.HalfHeight);
        }


    }
}
