using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Script.Model.Entities;
using Script.Model.Interfaces;

namespace Assets.Script.Model.UpdateSystem
{
    public class BillingSystem : IUpdate
    {
        private EntityContainer entityContainer;
        public Action<IUpdate> OnRemove { get; set; }
        public int Store;


        public BillingSystem(EntityContainer container)
        {
            entityContainer = container;
        }
              
        public void Update(float timeDelta)
        {
            foreach(var entity in entityContainer.Entity)
            {
                if (entity.DestroyOnEndCheck)
                {
                    Store++;
                }
            }
        }
    }
}
