using System;
using System.Collections.Generic;
using System.Numerics;
using Script.Model.Interfaces;
using Script.Model.Physics;
using Script.Model.Tools;

namespace Script.Model.Borders
{
    public class Portals : IUpdate
    {
        private Vector2 border;
        private List<Transformable> transformables;
        public Action<IUpdate> Remove { get; set; }



        public Portals(Vector2 value)
        {
            border = value;
            transformables = new();
        }

        public void AddActors(Transformable value)
        {
            transformables.Add(value);
        }

        public void Update(float timeDelta)
        {
            foreach (Transformable transformable in transformables)
            {
                transformable.Position = transformable.Position.Repeat(border);
            }
        }
    }
}
