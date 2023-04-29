using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Delivery.Input
{
    internal class ButtonWithCooldown
    {
        private bool _downLastFrame;
        private Keys _key;
        private int _ms;
        private readonly int _cooldownMs;

        internal ButtonWithCooldown(Keys key, int cooldownMs)
        {
            _key = key;
            _cooldownMs = cooldownMs;
        }

        public bool Update(float deltaTime)
        {
            bool isPressed = Keyboard.GetState().GetPressedKeys().Contains(_key);
            if (isPressed && !_downLastFrame)
                _downLastFrame = true;

            if (_ms > 0)
            {
                _ms -= (int)(deltaTime * 1000);
                if (_ms < 0)
                {
                    _ms = 0;
                }

                return false;
            }

            if (!isPressed)
            {
                return false;
            }

            _ms = _cooldownMs;
            return true;
        }
    }
}
