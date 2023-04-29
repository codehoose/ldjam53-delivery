using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Delivery.Input
{
    internal class HorizontalAxis
    {
        public Vector2 Direction { get; set; }

        public void Update()
        {
            Direction = Vector2.Zero;

            bool isWPressed = Keyboard.GetState().GetPressedKeys().Contains(Keys.A);
            bool isUpPressed = GamePad.GetState(1).ThumbSticks.Left.X < 0;

            if (isWPressed || isUpPressed)
            {
                Direction = Vector2.UnitX * -1;
                return;
            }

            bool isDPressed = Keyboard.GetState().GetPressedKeys().Contains(Keys.D);
            bool isDownPressed = GamePad.GetState(0).ThumbSticks.Left.X > 0;

            if (isDPressed || isDownPressed)
                Direction = Vector2.UnitX;
        }
    }
}
