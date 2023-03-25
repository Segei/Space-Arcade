using Script.Model.Interfaces;
using Script.Model.Physics;
using Script.Model.Weapon;
using UnityEngine;

namespace Script.Model.Entities
{
    public class Ship : OtherEntity
    {
        public IWeapon MainWeapon { get; private set; }
        public IAmunition SecondWeapon { get; private set; }


        public Ship(Transformable transformable, int maxBullet, float timeReloadBullet) : base(transformable)
        {
            MainWeapon = new Gatling(this, 0.5f);
            SecondWeapon = new Lazer(this, 0.3f, maxBullet, timeReloadBullet);
        }
    }
}
