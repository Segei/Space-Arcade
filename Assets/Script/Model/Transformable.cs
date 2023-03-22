using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Script.Model.Tools
{
    public abstract class Transformable : IUpdate
    {
        private float decelerationTime = 0;
        public Vector2 Velocity = new();
        public Vector2 Position = new();
        public float Turn = 0;
        public float SecondsToStop = 0;
        public Action<IUpdate> Remove { get; set; }


        public virtual void Update(float timeDelta)
        {
            if (SecondsToStop > 0)
            {
                Velocity -= Velocity * (decelerationTime / SecondsToStop)/10;
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

        protected abstract void NormalizeVelocyty();
    }
}
