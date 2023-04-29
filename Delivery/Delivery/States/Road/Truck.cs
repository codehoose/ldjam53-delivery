using Delivery.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.States.Road
{
    internal class Truck
    {
        Texture2D _truck;
        float _speed;
        Vector2 _pos;

        public Vector2 Position => _pos;

        public Rectangle Bounds => new Rectangle((int) _pos.X, (int) _pos.Y, _truck.Width, _truck.Height);


        internal Truck(DeliveryGame game, float speed = 32)
        {
            _truck = game.Content.Load<Texture2D>("truck");
            _speed = speed;
            _pos = new Vector2(32, (192 - 32) / 2);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset, float deltaTime)
        {
            
            spriteBatch.Draw(_truck, (_pos + offset).RoundPixel(), Color.White);
        }

        public void Update(float deltaTime)
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] keys = state.GetPressedKeys();
            if (keys.Contains(Keys.W))
            {
                _pos -= Vector2.UnitY * deltaTime * _speed;
                if (_pos.Y < 16)
                {
                    _pos = new Vector2(32, 16);
                }
            } else if (keys.Contains(Keys.S))
            {
                _pos += Vector2.UnitY * deltaTime * _speed;
                if (_pos.Y > 156)
                {
                    _pos = new Vector2(32, 156);
                }
            }
        }
    }
}
