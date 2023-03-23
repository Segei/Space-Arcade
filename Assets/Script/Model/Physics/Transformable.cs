using System;
using Script.Model.Interfaces;
using Script.Model.Tools;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Script.Model.Physics
{
    public class Transformable : IUpdate
    {
        public float MaxSpeed;
        private float decelerationTime = 0;
        public Vector2 Velocity = new();
        public Vector2 Position = new();
        public float Turn = 0;
        public float SecondsToStop = 0;
        public Action<IUpdate> Remove { get; set; }


        public virtual void Update(float timeDelta)
        {
            if (SecondsToStop > 0 && decelerationTime / SecondsToStop > 0)
            {
                Velocity -= Velocity * (decelerationTime / SecondsToStop);
            }
            if (decelerationTime < SecondsToStop)
            {
                decelerationTime += timeDelta;
            }
            Velocity = Velocity.Length() > 1 ? Velocity : Vector2.Zero;
            Position += Velocity * timeDelta;
        }

        public void Rotate(float angleDelta)
        {
            Turn = ExtendingClasses.Repeat(Turn + angleDelta, 360);
        }

        public void Accelerate(Vector2 acceleration)
        {
            decelerationTime = 0;
            Velocity += acceleration;
            NormalizeVelocyty();
        }

        protected virtual void NormalizeVelocyty()
        {
            Velocity = Velocity.ClampingMagnitude(MaxSpeed);
        }
    }
}
