using System.Collections.Generic;

namespace Script.Model.Entities
{
    public class EntityContainer
    {
        public Ship Ship { get; set; }
        public List<OtherEntity> Entity { get; set; }
        public List<OtherEntity> Bullet { get; set; }

        public EntityContainer()
        {
            Entity = new List<OtherEntity>();
            Bullet = new List<OtherEntity>();
        }
    }
}
