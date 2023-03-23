using System;
using Script.Model.Physics;

namespace Script.Model.Entities
{
    public class OtherCollision
    {
        public bool DestroyOnEndCheck = false;
        public Transformable Transformable { get; protected set; }
        public Collision Collision { get; protected set; }
        public Action Destroyed;

        public void CheckOnDestroy()
        {
            if (DestroyOnEndCheck)
            {
                Destroyed?.Invoke();
            }
        }
    }
}
