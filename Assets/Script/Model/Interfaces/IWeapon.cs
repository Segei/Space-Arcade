using System;
using Script.Model.Entities;

namespace Script.Model.Interfaces
{
    public interface IWeapon : IUpdate
    {
        public Action<OtherEntity> OnShoot { get; set; }
        void Attack();
    }
}
