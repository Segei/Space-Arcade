using System;
using System.Numerics;
using Script.Model.Entities;
using Script.Model.Interfaces;
using Script.Model.Physics;

namespace Script.Model.Weapon
{
    public class Gatling : IWeapon
    {
        private Ship ship;
        private float cooldown, timePassed;

        public Gatling(Ship ship, float cooldown)
        {
            this.ship = ship;
            this.cooldown = cooldown;
        }

        public Action<OtherEntity> OnShoot { get; set; }
        public Action<IUpdate> OnRemove { get; set; }

        public void Attack()
        {
            if(timePassed < cooldown)
            {
                return;
            }

            Transformable engine = new()
            {
                Position = ship.Transformable.Position + (Vector2.Normalize(ship.Transformable.Direction) * ship.Collision.HalfSize.Y),
                Turn = ship.Transformable.Turn
            };
            engine.Velocity = engine.Direction * 400;

            OtherEntity bullet = new(engine);

            timePassed = 0;

            OnShoot?.Invoke(bullet);
        }

        public void Update(float timeDelta)
        {
            timePassed += timeDelta;
        }
    }
}
