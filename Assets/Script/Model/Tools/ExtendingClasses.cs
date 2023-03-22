using System;
using System.Numerics;

namespace Assets.Script.Model.Tools
{
    public static class ExtendingClasses
    {
        public static float Repeat(float value, float maxValue) 
        {
            return (float)Math.Clamp(value - (Math.Floor(value / maxValue) * maxValue), 0f, maxValue);
        }

        public static float GetRadians(this float value)
        {
            return value * (float)Math.PI / 180;
        }

        public static Vector2 Repeat(this Vector2 value, Vector2 border)
        {
            return new Vector2()
            {
                X = Repeat(value.X, border.X),
                Y = Repeat(value.Y, border.Y),
            };
        }

        public static Vector2 ClampingMagnitude(this Vector2 vector, float maxMagnitude)
        {
            return vector.Length() > maxMagnitude ? Vector2.Normalize(vector) * maxMagnitude : vector;
        }
    }
}
