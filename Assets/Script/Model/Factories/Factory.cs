using System;
using Assets.Script.Model.Borders;
using Script.Model.Entities;
using Script.Model.Interfaces;
using Script.Model.UpdateSystem;

namespace Assets.Script.Model.Factories
{
    public abstract class Factory : IUpdate
    {
        private float cooldown, timePassed;
        protected BordersToSpawn bordersToSpawn;
        protected StartUpdate startUpdate;
        protected EntityContainer entityContainer;
        public Action<OtherEntity> OnSpawnEntity;


        public Action<IUpdate> OnRemove { get; set; }

        public Factory(EntityContainer container, StartUpdate update, BordersToSpawn border, float cooldownTimeToSpawnEntity)
        {
            entityContainer = container;
            startUpdate = update;
            bordersToSpawn = border;
            cooldown = cooldownTimeToSpawnEntity;
        }

        public abstract void SpawnEntity();

        public void Update(float timeDelta)
        {
            if (cooldown == 0)
            {
                return;
            }
            timePassed += timeDelta;
            if (timePassed >= cooldown)
            {
                timePassed = 0;
                SpawnEntity();
            }
        }
    }
}
