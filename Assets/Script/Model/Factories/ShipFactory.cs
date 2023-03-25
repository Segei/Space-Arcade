using Assets.Script.Model.Borders;
using Script.Model.Entities;
using Script.Model.UpdateSystem;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Script.Model.Factories
{
    public class ShipFactory : Factory
    {
        private Vector2 halfSize, startPosition;
        private int maxBullet;
        private float timeReload;


        public ShipFactory(int maxBullet, float timeReloadBullet, Vector2 position, Vector2 size,
            EntityContainer container, StartUpdate update, BordersToSpawn border,
            float cooldownTimeToSpawnEntity) : base(container, update, border, cooldownTimeToSpawnEntity)
        {
            halfSize = size / 2;
            startPosition = position;
            this.maxBullet = maxBullet;
            timeReload = timeReloadBullet;
        }

        public override void SpawnEntity()
        {
            ShipMovable engine = new()
            {
                MaxSpeed = 300,
                TurnAcceleration = 150,
                Acceleration = 40,
                SecondsToStop = 30,
                Position = startPosition
            };

            Ship ship = new(engine, maxBullet, timeReload);
            ship.Collision.HalfSize = halfSize;

            startUpdate.AddUpdateOnEnd(engine);
            startUpdate.AddUpdateOnEnd(ship.MainWeapon);
            startUpdate.AddUpdateOnEnd(ship.SecondWeapon);
            ship.MainWeapon.OnShoot += e => startUpdate.AddUpdateOnEnd(e.Transformable);
            entityContainer.Ship = ship;
            OnSpawnEntity?.Invoke(ship);
        }
    }
}
