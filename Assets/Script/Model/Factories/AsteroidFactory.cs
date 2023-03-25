using System;
using Assets.Script.Model.Borders;
using Script.Model.Entities;
using Script.Model.Physics;
using Script.Model.Tools;
using Script.Model.UpdateSystem;
using UnityEngine;
using Random = System.Random;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Script.Model.Factories
{
    public class AsteroidFactory : Factory
    {
        private Transformable target;
        private Vector2 asteroidHalfSize, splinterHalfSize;
        private int countSliters;
        public Action<OtherEntity> OnSpawnSplinter;
        private Random rand;


        public AsteroidFactory(Vector2 asteroidSize, Vector2 splinterSize, int countSliters,
            Transformable ship, EntityContainer container, StartUpdate update, BordersToSpawn border,
            float cooldownTimeToSpawnEntity) : base(container, update, border, cooldownTimeToSpawnEntity)
        {
            target = ship;
            asteroidHalfSize = asteroidSize / 2;
            splinterHalfSize = splinterSize / 2;
            this.countSliters = countSliters;
            rand = new Random();
        }

        public override void SpawnEntity()
        {
            Transformable engine = new()
            {
                MaxSpeed = 900,
                Position = bordersToSpawn.GetPointToSpawn()
            };
            engine.Velocity = (target.Position - engine.Position) / 10;
            engine.Rotate(engine.Direction.AngleBetweenWithDirection(target.Position - engine.Position));

            OtherEntity asteroid = new(engine);
            asteroid.Collision.HalfSize = asteroidHalfSize;
            asteroid.OnDestroyed += SpawnSplinter;

            startUpdate.AddUpdateOnEnd(engine);
            OnSpawnEntity.Invoke(asteroid);
        }
        private void SpawnSplinter(OtherEntity mainAsteroid)
        {
            mainAsteroid.OnDestroyed -= SpawnSplinter;
            for (int i = 0; i < countSliters; i++)
            {
                Transformable engine = new()
                {
                    MaxSpeed = mainAsteroid.Transformable.MaxSpeed * 2,
                    Position = mainAsteroid.Transformable.Position,
                    Turn = mainAsteroid.Transformable.Turn,
                };
                engine.Rotate((float)((rand.NextDouble() * (15 + 15)) - 15));
                engine.Velocity = engine.Direction * mainAsteroid.Transformable.Velocity.Length() * 1.5f;

                OtherEntity splinter = new(engine);
                splinter.Collision.HalfSize = splinterHalfSize;

                startUpdate.AddUpdateOnEnd(engine);
                OnSpawnSplinter?.Invoke(splinter);
            }
        }
    }
}
