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
        private HorizontalAxis _horizontal;
        private float _pizzaSpeed;
        private ButtonWithCooldown _fireButton;

        List<Vector2> _activePizzas = new List<Vector2>();

        public Vector2 Position => _pos;

        public Rectangle Bounds => new Rectangle((int) _pos.X, (int) _pos.Y, _truck.Width, _truck.Height);

        public bool IsAtSide { get; set; }

        public List<Vector2> ActivePizzas => _activePizzas;

        internal Truck(DeliveryGame game, float speed = 32, float pizzaSpeed = 64)
        {
            _truck = game.Content.Load<Texture2D>("truck");
            _pizza= game.Content.Load<Texture2D>("pizza");
            _speed = speed;
            _pos = new Vector2(32, (192 - 32) / 2);
            _vertical = new VerticalAxis();
            _horizontal = new HorizontalAxis();
            _pizzaSpeed = pizzaSpeed;
            _fireButton = new ButtonWithCooldown(Keys.Space, 1000);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset, float deltaTime)
        {
            foreach (var pos in _activePizzas)
            {
                spriteBatch.Draw(_pizza, pos.RoundPixel(), Color.White);
            }

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
            _horizontal.Update();
            _pos += _vertical.Direction * deltaTime * _speed;
            _pos += _horizontal.Direction * deltaTime * _speed;
            _pos = _pos.ClampY(72, 156);
            _pos = _pos.ClampX(32, 96);

            int i = 0;
            while (i < _activePizzas.Count)
            {
                _activePizzas[i] += Vector2.UnitY * -1f * deltaTime * _pizzaSpeed;
                if (_activePizzas[i].Y < -16)
                    _activePizzas.RemoveAt(i);
                else
                    i++;
            }

            if (_fireButton.Update(deltaTime))
            {
                _activePizzas.Add(_pos.RoundPixel());
            }
        }
    }
}
