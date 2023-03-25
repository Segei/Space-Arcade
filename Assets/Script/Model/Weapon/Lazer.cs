using System;
using System.Collections.Generic;
using System.Numerics;
using Script.Model.Entities;
using Script.Model.Interfaces;
using Script.Model.Physics;

namespace Script.Model.Weapon
{
    internal class Lazer : IWeapon, IAmunition
    {
        private Ship ship;
        private float cooldown, timePassed, timeToReload, timeToDestroy;
        private int maxBullet;
        private List<OtherEntity> lazerParts = new();
        public int CurrentCountBullet { get; set; }
        public float TimeReloadNextBullet { get; set; }



        public Lazer(Ship ship, float cooldown, int maxBullet, float timeToReload)
        {
            this.ship = ship;
            this.cooldown = cooldown;
            this.maxBullet = maxBullet;
            this.timeToReload = timeToReload;
            CurrentCountBullet = maxBullet;
            TimeReloadNextBullet = timeToReload;
        }

        public Action<OtherEntity> OnShoot { get; set; }
        public Action<IUpdate> OnRemove { get; set; }

        public void Attack()
        {
            if (timePassed < cooldown || CurrentCountBullet <= 0)
            {
                return;
            }

            for (int i = 0; i < 20; i++)
            {
                Transformable engine = new()
                {
                    Position = ship.Transformable.Position + (Vector2.Normalize(ship.Transformable.Direction) * ship.Collision.HalfSize.Y * 1.2f)
                    + (Vector2.Normalize(ship.Transformable.Direction) * i * 100),
                    Turn = ship.Transformable.Turn
                };
                OtherEntity lazer = new(engine);
                lazer.DestroyOnColision = false;
                OnShoot?.Invoke(lazer);
                lazerParts.Add(lazer);
            }
            timePassed = 0;
            CurrentCountBullet--;
        }

        public void Update(float timeDelta)
        {
            timePassed += timeDelta;
            if (TimeReloadNextBullet < timeToReload)
            {
                TimeReloadNextBullet += timeDelta;
            }
            else
            {
                if (CurrentCountBullet < maxBullet)
                {
                    CurrentCountBullet++;
                    TimeReloadNextBullet = 0;
                }
            }

            if (lazerParts.Count == 0)
            {
                return;
            }
            timeToDestroy += timeDelta;
            if(timeToDestroy >= 0.05)
            {
                lazerParts.ForEach(e => e.DestroyOnEndCheck = true);
                lazerParts.Clear();
                timeToDestroy = 0;
            }
        }
    }
}
