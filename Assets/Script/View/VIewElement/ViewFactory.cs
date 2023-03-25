using System;
using System.Collections.Generic;
using Assets.Script.Model.Factories;
using Script.Model.Entities;
using UnityEngine;
using Zenject;

namespace Script.View.VIewElement
{
    public class ViewFactory : MonoBehaviour
    {
        [Inject] private AsteroidFactory asteroidFactory;
        [Inject] private NLOFactory nloFactory;
        [Inject] private Ship ship;
        [Inject] private RectTransform canvas;
        [Inject] private EntityContainer entityContainer;
        [Inject(Id = "Asteroid")] private ViewEntity viewAsteroid;
        [Inject(Id = "Splinter")] private ViewEntity viewSplinter;
        [Inject(Id = "Bullet")] private ViewEntity viewBullet;
        [Inject(Id = "LazerPart")] private ViewEntity viewLazerPart;
        [Inject(Id = "Ship")] private ViewEntity viewShip;
        [Inject(Id = "NLO")] private ViewEntity viewNLO;
        private List<OtherEntity> delayedSpawnAsteroid = new List<OtherEntity>();
        private List<OtherEntity> delayedSpawnSplinter = new List<OtherEntity>();
        private List<OtherEntity> delayedSpawnNLO = new List<OtherEntity>();
        private List<OtherEntity> delayedSpawnBullet = new List<OtherEntity>();
        private List<OtherEntity> delayedSpawnLazerPart = new List<OtherEntity>();
        private List<OtherEntity> delayedDestroy= new List<OtherEntity>();
        public Action NLODied, AsteroidDestroy, BulletShoot, LazerShoot;


        private Dictionary<OtherEntity, ViewEntity> objectToDestroy = new();

        private void Start()
        {
            asteroidFactory.OnSpawnEntity += delayedSpawnAsteroid.Add;
            asteroidFactory.OnSpawnSplinter += delayedSpawnSplinter.Add;
            nloFactory.OnSpawnEntity += delayedSpawnNLO.Add;
            ship.MainWeapon.OnShoot += delayedSpawnBullet.Add;
            ship.MainWeapon.OnShoot += e => BulletShoot?.Invoke();
            ship.SecondWeapon.OnShoot += delayedSpawnLazerPart.Add;
            ship.SecondWeapon.OnShoot += e=> LazerShoot?.Invoke();

            ViewEntity instance = Instantiate(viewShip, canvas);
            SettingView(ship, instance);
        }


        private void Update()
        {
            delayedDestroy.ForEach(DestroyView);
            delayedDestroy.Clear();

            delayedSpawnAsteroid.ForEach(e =>  SpawnEntity(e, viewAsteroid, entityContainer.Entity));
            delayedSpawnAsteroid.ForEach(e => e.OnDestroyed += a => AsteroidDestroy?.Invoke());
            delayedSpawnAsteroid.Clear();

            delayedSpawnSplinter.ForEach(e => SpawnEntity(e, viewSplinter, entityContainer.Entity));
            delayedSpawnSplinter.ForEach(e => e.OnDestroyed += a => AsteroidDestroy?.Invoke());
            delayedSpawnSplinter.Clear();

            delayedSpawnNLO.ForEach(e => SpawnEntity(e, viewNLO, entityContainer.Entity));
            delayedSpawnNLO.ForEach(e => e.OnDestroyed += a => NLODied?.Invoke());
            delayedSpawnNLO.Clear();

            delayedSpawnBullet.ForEach(e => SpawnEntity(e, viewBullet, entityContainer.Bullet));
            delayedSpawnBullet.Clear();

            delayedSpawnLazerPart.ForEach(e => SpawnEntity(e, viewLazerPart, entityContainer.Bullet));
            delayedSpawnLazerPart.Clear();
        }

        private void SpawnEntity(OtherEntity entity, ViewEntity view, List<OtherEntity> entities)
        {
            entities.Add(entity);
            ViewEntity viewInstance = Instantiate(view, canvas);
            viewInstance.RectTransform.anchoredPosition = new(-500, -500);
            SettingView(entity, viewInstance);
            objectToDestroy.Add(entity, viewInstance);
            entity.OnDestroyed += delayedDestroy.Add;
        }

        private void SettingView(OtherEntity entity, ViewEntity view)
        {
            entity.Collision.HalfSize.X = view.Width / 2;
            entity.Collision.HalfSize.Y = view.Height / 2;
            view.SetTarget(entity.Transformable);
        }

        private void DestroyView(OtherEntity entity)
        {
            Destroy(objectToDestroy[entity].gameObject);
            _ = objectToDestroy.Remove(entity);
        }
    }
}
