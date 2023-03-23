using System;
using Script.Model.Interfaces;
using Script.Model.Physics;
using Script.Model.Weapon;

namespace Script.Model.Entities
{
    public class Ship : OtherCollision
    {
        public new ShipMovable Transformable { get; private set; }
        public IWeapon MainWeapon { get; private set; }
        public IWeapon SecondWeapon { get; private set; }
        

        public Ship()
        {
            Transformable = new ShipMovable();
            MainWeapon = new Gatling();
            SecondWeapon = new Lazer();
            Collision = new(Transformable);
        }
    }
}
