using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Delivery.Input
{
    internal class VerticalAxis
    {
        public Vector2 Direction { get; set; }

        public void Update()
        {
            Direction = Vector2.Zero;

            bool isWPressed = Keyboard.GetState().GetPressedKeys().Contains(Keys.W);
            bool isUpPressed = GamePad.GetState(1).ThumbSticks.Left.Y < 0;

            if (isWPressed || isUpPressed)
            {
                Direction = Vector2.UnitY * -1;
                return;
            }

            bool isDPressed = Keyboard.GetState().GetPressedKeys().Contains(Keys.S);
            bool isDownPressed = GamePad.GetState(0).ThumbSticks.Left.Y > 0;

            if (isDPressed || isDownPressed)
                Direction = Vector2.UnitY;
        }
    }
}
