using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.StateMachine
{
    internal class FSM
    {
        private readonly DeliveryGame _game;
        private StateBase _currentState;

        internal DeliveryGame Game => _game;

        internal FSM(DeliveryGame game)
        {
            _game = game;
        }

        public void ChangeState(string stateName)
        {
            ChangeState(_game.States[stateName]);
        }

        public void ChangeState(StateBase state)
        {
            if (_currentState != null)
            {
                _currentState.Exit(this);
            }

            _currentState = state;
            _currentState.Enter(this);
        }

        public void Update(GameTime gameTime)
        {
            _currentState?.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _currentState?.Draw(spriteBatch, gameTime.ElapsedGameTime.Milliseconds / 1000f);
        }
    }
}
