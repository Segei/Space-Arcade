using System.Numerics;
using Script.Model.Tools;

namespace Script.Model.Entities
{
    public class ShipMovable : ShipEngienSettings
    {
        private float turnVelosity;


        public void MoveForward()
        {
            Accelerate(Vector2.Normalize(Direction) * Acceleration);
        }

        public void SetMaxSpeed(float value)
        {
            MaxSpeed = value;
        }

        public void SetTurn(float value)
        {
            turnVelosity = value;
        }

        public override void Update(float timeDelta)
        {
            Rotate(turnVelosity * TurnAcceleration * timeDelta);
            base.Update(timeDelta);
        }
    }
}
