using Script.Model.Entities;
using Script.Model.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.View.VIewElement
{
    public class ViewLazerAmmo : MonoBehaviour
    {
        [Inject] private Ship ship;
        [SerializeField] private TMP_Text countBullet, timeReload;
        private IAmunition target;


        private void Start()
        {
            target = ship.SecondWeapon;
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            countBullet.text = string.Format("Колличество выстрелов {0}", target.CurrentCountBullet);
            timeReload.text = string.Format("Перезарядка {0}", target.TimeReloadNextBullet);
        }
    }
}
