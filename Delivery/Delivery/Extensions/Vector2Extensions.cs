﻿using Microsoft.Xna.Framework;

namespace Delivery.Extensions
{
    internal static class Vector2Extensions
    {
        public static Vector2 RoundPixel(this Vector2 vec)
        {
            return new Vector2((int)vec.X, (int)vec.Y);
        }

        public static Vector2 ClampY(this Vector2 vec, float lower, float upper)
        {
            if (vec.Y < lower)
            {
                vec = new Vector2(vec.X, lower);
            }
            else if (vec.Y > upper)
            {
                vec = new Vector2(vec.X, upper);
            }

            return vec;
        }
    }
}
