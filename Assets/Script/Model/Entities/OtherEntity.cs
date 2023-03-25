using System;
using Script.Model.Physics;
using Collision = Script.Model.Physics.Collision;

namespace Script.Model.Entities
{
    [System.Serializable]
    public class OtherEntity
    {
        public bool DestroyOnEndCheck = false;
        public bool DestroyOnColision = true;
        public Transformable Transformable { get; protected set; }
        public Collision Collision { get; protected set; }
        public Action<OtherEntity> OnDestroyed;


        public OtherEntity(Transformable transformable)
        {
            Transformable = transformable;
            Collision = new Collision(Transformable);
            Instance(Transformable);
        }

        protected void Instance(Transformable transformable)
        {
            OnDestroyed += e => transformable.Remove();
        }

        public void CheckOnDestroy()
        {
            if (DestroyOnEndCheck)
            {
                OnDestroyed?.Invoke(this);
            }
        }
    }
}
