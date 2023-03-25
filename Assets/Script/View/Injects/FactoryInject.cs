using Assets.Script.Model.Borders;
using Assets.Script.Model.Factories;
using Script.Model.Borders;
using Script.Model.Entities;
using Script.Model.UpdateSystem;
using Script.View.VIewElement;
using UnityEngine;
using Zenject;
using Vector2 = System.Numerics.Vector2;

public class FactoryInject : MonoInstaller
{
    private AsteroidFactory asteroidFactory;
    private NLOFactory nloFactory;
    private ShipFactory shipFactory;
    private Ship ship;
    private BordersToSpawn border;
    private Portals portals;
    private Vector2 sizeCanvas => new(settings.Canvas.sizeDelta.x, settings.Canvas.sizeDelta.y);

    [SerializeField] private int countSlinters;
    [SerializeField] private int maxBulletInShip;
    [SerializeField] private float timeReloadOneBullet, timeSpawnAsteroid, timeSpawnNLo;
    [SerializeField] private StartServerSettings settings;

    [Inject] private StartUpdate update;
    [Inject(Id = "NLO")] private ViewEntity viewNLO;
    [Inject(Id = "Asteroid")] private ViewEntity viewAsteroid;
    [Inject(Id = "Splinter")] private ViewEntity viewSplinter;
    [Inject(Id = "Ship")] private ViewEntity viewShip;


    public override void InstallBindings()
    {
        portals = new(sizeCanvas);
        border = new(sizeCanvas, 100);
        shipFactory = new(maxBulletInShip, timeReloadOneBullet, sizeCanvas / 2, new(viewShip.Width, viewShip.Height), settings.EntityConteiner, settings.Update, border, 0f);
        shipFactory.OnSpawnEntity += e => ship = (Ship)e;
        shipFactory.SpawnEntity();

        asteroidFactory = new AsteroidFactory(new(viewAsteroid.Width, viewAsteroid.Height),
                new(viewSplinter.Width, viewSplinter.Height), countSlinters, ship.Transformable, settings.EntityConteiner, settings.Update,
                border, timeSpawnAsteroid);
        nloFactory = new(new(viewNLO.Width, viewNLO.Height), ship.Transformable, settings.EntityConteiner, settings.Update, border, timeSpawnNLo);

        _ = Container.Bind<Ship>().FromInstance(ship).AsSingle().NonLazy();
        _ = Container.Bind<AsteroidFactory>().FromInstance(asteroidFactory).AsSingle().NonLazy();
        _ = Container.Bind<NLOFactory>().FromInstance(nloFactory).AsSingle().NonLazy();
    }

    public override void Start()
    {
        base.Start();


        portals.AddActors(ship.Transformable);
        update.AddListener(portals);
        update.AddListener(asteroidFactory);
        update.AddListener(nloFactory);
    }
}