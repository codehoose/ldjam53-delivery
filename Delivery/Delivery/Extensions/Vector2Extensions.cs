using Microsoft.Xna.Framework;

namespace Delivery.Extensions
{
    internal static class Vector2Extensions
    {
        public static Vector2 RoundPixel(this Vector2 vec)
        {
            return new Vector2((int)vec.X, (int)vec.Y);
        }
    }
}
