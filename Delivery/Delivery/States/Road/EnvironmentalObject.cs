using Delivery.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States.Road
{
    internal class EnvironmentalObject
    {
        private Rectangle[] _bounds;
        private Texture2D _texture;
        private Vector2 _position;

        public Rectangle[] Bounds
        {
            get
            {
                return _bounds;
            }
        }

        public Vector2 Position => _position;

        internal EnvironmentalObject(Texture2D texture, Vector2 position, params Rectangle[] bounds)
        {
            _bounds = bounds;
            _position = position;
        }

        public void Update(Vector2 offset)
        {
            _position += offset;

            for (int i = 0; i < _bounds.Length; i++)
            {
                _bounds[i] = _bounds[i].Add(offset);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
