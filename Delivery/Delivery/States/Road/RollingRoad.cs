using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States.Road
{
    internal class RollingRoad
    {
        private readonly Texture2D[] _blocks = new Texture2D[3];
        private float _xoff;
        private float _speed;
        internal RollingRoad(DeliveryGame game, float speed = 16f)
        {
            _blocks[0] = game.Content.Load<Texture2D>("road-block-bottom");
            _blocks[1] = game.Content.Load<Texture2D>("road-block");
            _blocks[2] = game.Content.Load<Texture2D>("road-block-bottom");

            _speed = speed;
        }

        internal void Update(float deltaTime)
        {
            _xoff -= deltaTime * _speed;
            if (_xoff < -64)
            {
                _xoff += 64;
            }
        }

        internal void Draw(SpriteBatch spriteBatch, Vector2 offset, float deltaTime)
        {
            int xoff = (int)_xoff;

            for (int y = 1; y < 3; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Vector2 pos = new Vector2(xoff , 12) + offset;
                    pos += new Vector2(x * 64, y * 60);
                    spriteBatch.Draw(_blocks[y], pos, Color.White);
                }
            }
        }
    }
}
