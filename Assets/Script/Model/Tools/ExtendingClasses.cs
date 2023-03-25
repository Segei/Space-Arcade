using System;
using System.Numerics;

namespace Script.Model.Tools
{
    public static class ExtendingClasses
    {
        public static float Repeat(float value, float maxValue) 
        {
            return (float)Math.Clamp(value - (Math.Floor(value / maxValue) * maxValue), 0f, maxValue);
        }
        public static int Repeat(int value, int maxValue)
        {
            return value > maxValue ? 0 : value;
        }

        public static float GetRadians(this float value)
        {
            return value * (float)Math.PI / 180;
        }
        public static float GetEuler(this float value)
        {
            return (value * 180) / (float)Math.PI;
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

        public static Vector2 DirectionForce(this Vector2 position, float rotationAboutPosition)
        {
            return new Vector2()
            {
                X = position.X * (float)Math.Cos(rotationAboutPosition.GetRadians()) - position.Y * (float)Math.Sin(rotationAboutPosition.GetRadians()),
                Y = position.X * (float)Math.Sin(rotationAboutPosition.GetRadians()) + position.Y * (float)Math.Cos(rotationAboutPosition.GetRadians())
            };
        }

        public static float AngleBetween(this Vector2 direction, Vector2 target)
        { 
            return ((float)Math.Acos(Vector2.Dot(target, direction) / (direction.GetModule() * target.GetModule()))).GetEuler();         
        }

        public static bool СomparisonForSmaller(this Vector2 vector, Vector2 other)
        {
            return vector.X < other.X || vector.Y < other.Y;
        }

        public static float GetModule(this Vector2 vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y *vector.Y);
        }

        public static float AngleBetweenWithDirection(this Vector2 direction, Vector2 target)
        {
            float num = AngleBetween(direction, target);
            float num2 = Sign(direction.X * target.Y - direction.Y * target.X);
            return num * num2;
        }

        public static float Sign(float f)
        {
            return (f >= 0f) ? 1f : (-1f);
        }
    }
}
