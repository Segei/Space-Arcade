using System.Collections.Generic;

namespace Script.Model.Entities
{
    public class EntityContainer
    {
        public Ship Ship { get; set; }
        public List<OtherCollision> Asteroids { get; set; }
        public List<NLO> NLOs { get; set; }
        public List<OtherCollision> Bullet { get; set; }

        public EntityContainer()
        {
            Asteroids = new List<OtherCollision>();
            NLOs = new List<NLO>();
            Bullet = new List<OtherCollision>();
        }
    }
}
