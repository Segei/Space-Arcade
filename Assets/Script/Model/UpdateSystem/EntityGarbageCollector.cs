using System.Collections.Generic;
using Script.Model.Entities;
using UnityEngine;

namespace Assets.Script.Model.UpdateSystem
{
    public class EntityGarbageCollector
    {
        private EntityContainer entityContainer;

        public EntityGarbageCollector(EntityContainer container)
        {
            entityContainer = container;
        }

        public void LateUpdate()
        {
            entityContainer.Ship.CheckOnDestroy();
            ClearList(entityContainer.Entity);
            ClearList(entityContainer.Bullet);
        }
        private void ClearList<T>(List<T> entities) where T : OtherEntity
        {
            for (int i = 0; i < entities.Count;)
            {
                if (entities[i].DestroyOnEndCheck)
                {
                    entities[i].CheckOnDestroy();
                    entities.RemoveAt(i);
                    continue;
                }
                i++;
            }
        }
    }
}
