using System;
using System.Numerics;
using Assets.Script.Model.Tools;

namespace Assets.Script.Model
{
    public class Ship : Transformable
    {
        public float MaxSpeed, Acceleration, TurnAcceleration;
        private float turnVelosity;
        public void MoveForward()
        {
            Vector2 position = new Vector2(0, 1);
            Vector2 result = new Vector2()
            {
                X = position.X * (float)Math.Cos(Turn.GetRadians()) - position.Y * (float)Math.Sin(Turn.GetRadians()),
                Y = position.X * (float)Math.Sin(Turn.GetRadians()) + position.Y * (float)Math.Cos(Turn.GetRadians())
            };
            Accelerate(Vector2.Normalize(result) * Acceleration);
        }

        public void SetMaxSpeed(float value)
        {
            MaxSpeed = value;
        }

        public void SetTurn(float value)
        {
            turnVelosity = value;
        }

        protected override void NormalizeVelocyty()
        {
            Velocity = Velocity.ClampingMagnitude(MaxSpeed);
        }

        public override void Update(float timeDelta)
        {
            Rotate(turnVelosity * TurnAcceleration * timeDelta);
            base.Update(timeDelta);
        }
    }
}
