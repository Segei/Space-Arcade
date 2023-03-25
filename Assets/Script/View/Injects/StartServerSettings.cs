using Assets.Script.Model.Borders;
using Assets.Script.Model.UpdateSystem;
using Script.Model.Entities;
using Script.Model.Physics;
using Script.Model.UpdateSystem;
using UnityEngine;
using Zenject;
using Vector2 = System.Numerics.Vector2;

public class StartServerSettings : MonoInstaller
{
    public RectTransform Canvas;
    public EntityContainer EntityConteiner;
    public StartUpdate Update;
    private DetectorCollision detectorCollision;
    private EntityGarbageCollector collector;
    private DestroyerEntityAbroad entityAbroad;
    private BillingSystem billingSystem;

    private Vector2 sizeCanvas => new(Canvas.sizeDelta.x, Canvas.sizeDelta.y);


    public override void InstallBindings()
    {
        Update = new()
        {
            FramePerSecond = 256
        };
        EntityConteiner = new();
        detectorCollision = new(EntityConteiner);
        entityAbroad = new(sizeCanvas, 200, EntityConteiner);
        collector = new(EntityConteiner);
        billingSystem = new(EntityConteiner);

        _ = Container.Bind<StartUpdate>().FromInstance(Update).AsSingle().NonLazy();
        _ = Container.Bind<RectTransform>().FromInstance(Canvas).AsSingle().NonLazy();
        _ = Container.Bind<EntityContainer>().FromInstance(EntityConteiner).AsSingle().NonLazy();
        _ = Container.Bind<BillingSystem>().FromInstance(billingSystem).AsSingle().NonLazy();
    }

    public override void Start()
    {
        base.Start();

        Update.AddListener(detectorCollision);
        Update.AddListener(billingSystem);
        Update.AddListener(entityAbroad);
        Update.OnUpdate += collector.LateUpdate;
        Update.Start();
    }
    private void OnDisable()
    {
        if (Update != null)
        {
            Update.Stop();
        }
    }
}