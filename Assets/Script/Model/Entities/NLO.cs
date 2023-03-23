using System;
using Script.Model.Physics;

namespace Script.Model.Entities
{
    public class NLO : OtherCollision
    {
        public new NLOMovable Transformable { get; protected set; }
        public NLO()
        {
            Transformable = new NLOMovable();
            Collision = new Collision(Transformable);
        }
    }
}
