using Microsoft.Xna.Framework;

namespace Delivery.Extensions
{
    internal static class RectangleExtensions
    {
        public static Rectangle Add(this Rectangle rect, Vector2 offset)
        {
            return new Rectangle((int)(rect.X + offset.X), (int)(rect.Y + offset.Y), rect.Width, rect.Height);
        }
    }
}
