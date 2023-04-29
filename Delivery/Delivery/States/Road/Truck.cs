using Delivery.Extensions;
using Delivery.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States.Road
{
    internal class Truck
    {
        Texture2D _truck;
        float _speed;
        Vector2 _pos;
        private VerticalAxis _vertical;

        public Vector2 Position => _pos;

        public Rectangle Bounds => new Rectangle((int) _pos.X, (int) _pos.Y, _truck.Width, _truck.Height);

        public bool IsAtSide { get; set; }

        internal Truck(DeliveryGame game, float speed = 32)
        {
            _truck = game.Content.Load<Texture2D>("truck");
            _speed = speed;
            _pos = new Vector2(32, (192 - 32) / 2);
            _vertical = new VerticalAxis();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset, float deltaTime)
        {
            spriteBatch.Draw(_truck, (_pos + offset).RoundPixel(), Color.White);
        }

        public void MoveToSide(float deltaTime)
        {
            _pos += Vector2.UnitY * deltaTime * _speed;
            if (_pos.Y > 156)
            {
                _pos = new Vector2(32, 156);
                IsAtSide = true;
            }
        }

        public void Update(float deltaTime)
        {
            _vertical.Update();
            _pos += _vertical.Direction * deltaTime * _speed;
        }
    }
}
