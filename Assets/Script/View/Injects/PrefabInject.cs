using Script.View.VIewElement;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PrefabInject", menuName = "Installers/PrefabInject")]
public class PrefabInject : ScriptableObjectInstaller<PrefabInject>
{
    [SerializeField] private ViewEntity viewNLO;
    [SerializeField] private ViewEntity viewAsteroid;
    [SerializeField] private ViewEntity viewSplinter;
    [SerializeField] private ViewEntity viewShip;
    [SerializeField] private ViewEntity viewBullet;
    [SerializeField] private ViewEntity viewLazerPart;

    public override void InstallBindings()
    {
        Container.Bind<ViewEntity>().WithId("NLO").FromInstance(viewNLO).NonLazy();
        Container.Bind<ViewEntity>().WithId("Asteroid").FromInstance(viewAsteroid).NonLazy();
        Container.Bind<ViewEntity>().WithId("Splinter").FromInstance(viewSplinter).NonLazy();
        Container.Bind<ViewEntity>().WithId("Ship").FromInstance(viewShip).NonLazy();
        Container.Bind<ViewEntity>().WithId("Bullet").FromInstance(viewBullet).NonLazy();
        Container.Bind<ViewEntity>().WithId("LazerPart").FromInstance(viewLazerPart).NonLazy();
    }
}