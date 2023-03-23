using System;
using Script.Model.Entities;
using Script.Model.Interfaces;

namespace Script.Model.Physics
{
    public class DetectorCollision : IUpdate
    {
        public Action<IUpdate> Remove { get; set; }
        private EntityContainer container;


        public DetectorCollision(EntityContainer entityContainer)
        {
            container = entityContainer;
        }
        public void Update(float timeDelta)
        {
            foreach (NLO nlo in container.NLOs)
            {
                CheckCollisionEnemy(nlo);
            }

            foreach (OtherCollision asteroid in container.Asteroids)
            {
                CheckCollisionEnemy(asteroid);
            }

            foreach (OtherCollision bullet in container.Bullet)
            {
                bullet.CheckOnDestroy();
            }
        }

        private void CheckCollisionEnemy(OtherCollision collision)
        {
            if (container.Ship.Collision.CollisionDetected(collision.Collision))
            {
                container.Ship.Destroyed?.Invoke();
            }
            foreach (OtherCollision bullet in container.Bullet)
            {
                if (bullet.Collision.CollisionDetected(collision.Collision))
                {
                    collision.Destroyed?.Invoke();
                    bullet.DestroyOnEndCheck = true;
                }
            }
        }
    }
}
