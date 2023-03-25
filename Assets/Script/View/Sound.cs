using Script.Model.Entities;
using Script.View.VIewElement;
using UnityEngine;
using Zenject;

namespace Assets.Script.View
{
    public class Sound : MonoBehaviour
    {
        [Inject] private Ship ship;
        [SerializeField] private ViewFactory viewFactory;
        private bool playShipDestroy, playNLODied, playAsteroidDestroy, playBulletShoot, playLazerShoot;
        [SerializeField] private AudioSource shipDestroy, NLODied, asteroidDestroy, bulletShoot, lazerShoot;


        private void Start()
        {
            ship.OnDestroyed += e => playShipDestroy = true;
            viewFactory.AsteroidDestroy += () => playAsteroidDestroy = true;
            viewFactory.NLODied += () => playNLODied = true;
            viewFactory.BulletShoot += () => playBulletShoot = true;
            viewFactory.LazerShoot += () => playLazerShoot = true;
        }


        private void Update()
        {
            if (playShipDestroy)
            {
                shipDestroy.Play();
                playShipDestroy = false;
            }
            if (playNLODied)
            {
                NLODied.Play();
                playNLODied = false;
            }
            if (playAsteroidDestroy)
            {
                asteroidDestroy.Play();
                playAsteroidDestroy = false;
            }
            if (playBulletShoot)
            {
                bulletShoot.Play();
                playBulletShoot = false;
            }
            if (playLazerShoot)
            {
                lazerShoot.Play();
                playLazerShoot = false;
            }
        }
    }
}
