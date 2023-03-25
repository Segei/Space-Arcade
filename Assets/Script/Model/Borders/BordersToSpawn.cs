using Script.Model.Tools;
using UnityEngine;
using Random = System.Random;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Script.Model.Borders
{
    public class BordersToSpawn
    {
        private Vector2[] points = new Vector2[4];
        private Random random;

        public BordersToSpawn(Vector2 border, float padding)
        {
            points[0] = new Vector2(-padding, -padding);
            points[1] = new Vector2(-padding, border.Y + padding);
            points[2] = new Vector2(border.X + padding, border.Y + padding);
            points[3] = new Vector2(border.X + padding, -padding);

            random = new Random();
        }

        public Vector2 GetPointToSpawn()
        {
            int indexPoint = random.Next(3);
            Vector2 pintA = points[indexPoint];
            indexPoint = ExtendingClasses.Repeat(++indexPoint, 3);
            Vector2 pointB = points[indexPoint];
            float lerp = (float)random.NextDouble();
            return Vector2.Lerp(pintA, pointB, lerp);
        }
    }
}
