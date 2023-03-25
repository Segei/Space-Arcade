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
            Accelerate(Vector2.Normalize(Direction) * Acceleration);
        }

        private void RotateToTarget(float timeDelta)
        {
            float angelRotation = Direction.AngleBetweenWithDirection(Target.Position - Position);
            if (!float.IsNaN(angelRotation))
            {
                Rotate(angelRotation * timeDelta * TurnAcceleration);
            }
        }
    }
}
