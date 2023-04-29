using Delivery.Extensions;
using Delivery.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Delivery.States.Road
{
    internal class Truck
    {
        Texture2D _truck;
        Texture2D _pizza;
        float _speed;
        Vector2 _pos;
        private VerticalAxis _vertical;
        private ButtonWithCooldown _fireButton;

        List<Vector2> _activePizzas = new List<Vector2>();

        public Vector2 Position => _pos;

        public Rectangle Bounds => new Rectangle((int) _pos.X, (int) _pos.Y, _truck.Width, _truck.Height);

        public bool IsAtSide { get; set; }

        internal Truck(DeliveryGame game, float speed = 32)
        {
            _truck = game.Content.Load<Texture2D>("truck");
            _pizza= game.Content.Load<Texture2D>("pizza");
            _speed = speed;
            _pos = new Vector2(32, (192 - 32) / 2);
            _vertical = new VerticalAxis();
            _fireButton = new ButtonWithCooldown(Keys.Space, 1000);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset, float deltaTime)
        {
            spriteBatch.Draw(_truck, (_pos + offset).RoundPixel(), Color.White);

            foreach (var pos in _activePizzas)
            {
                spriteBatch.Draw(_pizza, pos.RoundPixel(), Color.White);
            }
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
            _pos = _pos.ClampY(16, 156);

            for (int i = 0; i < _activePizzas.Count; i++)
            {
                _activePizzas[i] += Vector2.UnitY * -1f * deltaTime * _speed;
            }

            if (_fireButton.Update(deltaTime))
            {
                _activePizzas.Add(_pos.RoundPixel());
            }
        }
    }
}
