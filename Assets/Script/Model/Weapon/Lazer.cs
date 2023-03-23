using System;
using Script.Model.Interfaces;

namespace Script.Model.Weapon
{
    internal class Lazer : IWeapon, IAmunition
    {
        public int CurrentCountBullet { get; set; }
        public float TimeReloadNextBullet { get; set; }

        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
