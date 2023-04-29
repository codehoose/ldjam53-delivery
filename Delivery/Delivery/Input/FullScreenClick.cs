using Microsoft.Xna.Framework.Input;

namespace Delivery.Input
{
    internal class FullScreenClick
    {
        private bool _wasPressed;

        public bool Done { get; private set; }

        public void Update()
        {
            MouseState state = Mouse.GetState();

            if (state.X >= 0 && state.X < 1024 && state.Y >= 0 && state.Y < 768)
            {
                if (state.LeftButton == ButtonState.Pressed && !_wasPressed)
                    _wasPressed = true;

                if (_wasPressed && state.LeftButton == ButtonState.Released)
                    Done = true;
            }
        }
    }
}
