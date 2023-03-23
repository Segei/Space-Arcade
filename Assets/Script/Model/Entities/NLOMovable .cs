using Script.Model.Physics;
using Script.Model.Tools;
using Vector2 = System.Numerics.Vector2;

namespace Script.Model.Entities
{
    public class NLOMovable : ShipEngienSettings
    {
        public Transformable Target { set; private get; }
        public override void Update(float timeDelta)
        {
            RotateToTarget(timeDelta);
            MoveForward();
            base.Update(timeDelta);
        }

        public void MoveForward()
        {
            Vector2 result = new Vector2(0, 1).DirectionForce(Turn);
            Accelerate(Vector2.Normalize(result) * Acceleration);
        }

        private void RotateToTarget(float timeDelta)
        {
            float angelRotation = new Vector2(0, -1).DirectionForce(Turn).AngleBetweenWithDirection(Position - Target.Position);
            if (!float.IsNaN(angelRotation))
            {
                Rotate(angelRotation * timeDelta * TurnAcceleration);
            }
        }
    }
}
