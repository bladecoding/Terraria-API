using System;
using Microsoft.Xna.Framework;

namespace XNAHelpers
{
    public static class MathHelper
    {
        public const float RadianPI = 57.29578f; // 180.0 / Math.PI
        public const float DegreePI = 0.01745329f; // Math.PI / 180.0
        public const float TwoPI = 6.28319f; // Math.PI * 2

        public static Random Random = new Random();

        public static float RadianToDegree(float radian)
        {
            return radian * RadianPI;
        }

        public static float DegreeToRadian(float degree)
        {
            return degree * DegreePI;
        }

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        }

        public static Vector2 RadianToVector2(float radian, float length)
        {
            return RadianToVector2(radian) * length;
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(DegreeToRadian(degree));
        }

        public static Vector2 DegreeToVector22(float degree)
        {
            return RadianToVector2(DegreeToRadian(degree));
        }

        public static Vector2 DegreeToVector2(float degree, float length)
        {
            return RadianToVector2(DegreeToRadian(degree), length);
        }

        public static float Vector2ToRadian(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public static float Vector2ToDegree(Vector2 direction)
        {
            return RadianToDegree(Vector2ToRadian(direction));
        }

        public static float LookAtRadian(Vector2 fromPosition, Vector2 toPosition)
        {
            return (float)Math.Atan2(toPosition.Y - fromPosition.Y, toPosition.X - fromPosition.X);
        }

        public static Vector2 LookAtVector2(Vector2 fromPosition, Vector2 toPosition)
        {
            return RadianToVector2(LookAtRadian(fromPosition, toPosition));
        }

        public static float TwoVector2Distance(Vector2 fromPosition, Vector2 toPosition)
        {
            return (float)Math.Sqrt(Math.Pow(toPosition.X - fromPosition.X, 2) + Math.Pow(toPosition.Y - fromPosition.Y, 2));
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public static Vector2 RandomPointOnCircle(Vector2 pos, float radius)
        {
            return pos + RadianToVector2(Random.NextAngle(), radius);
        }
    }
}