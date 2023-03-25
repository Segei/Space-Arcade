using System;
using Script.Model.Entities;
using Script.Model.Interfaces;

namespace Script.Model.Physics
{
    public class DetectorCollision : IUpdate
    {
        public Action<IUpdate> OnRemove { get; set; }
        private EntityContainer container;


        public DetectorCollision(EntityContainer entityContainer)
        {
            container = entityContainer;
        }
        public void Update(float timeDelta)
        {
            foreach (OtherEntity nlo in container.Entity)
            {
                CheckCollisionEnemy(nlo);
            }
        }

        private void CheckCollisionEnemy(OtherEntity entity)
        {
            Ship ship = container.Ship;
            if (ship.Collision.CollisionDetected(entity.Collision))
            {
                if (ship.DestroyOnColision)
                {
                    ship.DestroyOnEndCheck = true;
                }
            }
            foreach (OtherEntity bullet in container.Bullet)
            {
                if (bullet.Collision.CollisionDetected(entity.Collision))
                {
                    if (bullet.DestroyOnColision)
                    {
                        bullet.DestroyOnEndCheck = true;
                    }

                    if (entity.DestroyOnColision)
                    {
                        entity.DestroyOnEndCheck = true;
                    }
                }
            }
        }
    }
}
