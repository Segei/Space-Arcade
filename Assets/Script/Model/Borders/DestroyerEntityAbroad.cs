using System;
using Script.Model.Entities;
using Script.Model.Interfaces;
using Script.Model.Tools;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Script.Model.Borders
{
    public class DestroyerEntityAbroad : IUpdate
    {
        private Vector2 MinBorder;
        private Vector2 MaxBorder;
        private EntityContainer entityContainer;
        public Action<IUpdate> OnRemove { get; set; }

        public DestroyerEntityAbroad(Vector2 border, float padding, EntityContainer container)
        {
            MinBorder = new Vector2(-padding, -padding);
            MaxBorder = new Vector2(border.X + padding, border.Y + padding);
            entityContainer = container;
        }



        public void Update(float timeDelta)
        {
            foreach (OtherEntity entity in entityContainer.Entity)
            {
                CheckOnDestroy(entity);
            }
            foreach (OtherEntity entity in entityContainer.Bullet)
            {
                CheckOnDestroy(entity);
            }
        }

        private void CheckOnDestroy<T>(T entity) where T : OtherEntity
        {
            if (entity.Transformable.Position.СomparisonForSmaller(MinBorder)
                || MaxBorder.СomparisonForSmaller(entity.Transformable.Position))
            {
                entity.DestroyOnEndCheck = true;
            }
        }
    }
}
