using Assets.Script.Model.Borders;
using Script.Model.Entities;
using Script.Model.Physics;
using Script.Model.UpdateSystem;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Script.Model.Factories
{
    public class NLOFactory : Factory
    {
        private Transformable target;
        private Vector2 halfSize;


        public NLOFactory(Vector2 size, Transformable ship, EntityContainer container, 
            StartUpdate update, BordersToSpawn border, float cooldownTimeToSpawnEntity) 
            : base(container, update, border, cooldownTimeToSpawnEntity)
        {
            target = ship;
            halfSize = size / 2;
        }

        public override void SpawnEntity()
        {
            Debug.Log("Start spawn nLO");
            NLOMovable engine = new()
            {
                Target = target,
                MaxSpeed = 150,
                TurnAcceleration = 150,
                Acceleration = 20,
                SecondsToStop = 30,
                Position = bordersToSpawn.GetPointToSpawn()
            };

            OtherEntity nLO = new(engine);
            nLO.Collision.HalfSize = halfSize;

            startUpdate.AddUpdateOnEnd(engine);

            Debug.Log("nLO" + OnSpawnEntity);
            OnSpawnEntity?.Invoke(nLO);
            Debug.Log("End spawn nLO");
        }
    }
}
