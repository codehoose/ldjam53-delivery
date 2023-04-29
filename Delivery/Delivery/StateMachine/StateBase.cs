using Microsoft.Xna.Framework.Graphics;

namespace Delivery.StateMachine
{
    internal abstract class StateBase
    {
        private FSM _fsm;

        protected FSM FSM => _fsm;

        internal StateBase(FSM fsm)
        {
            _fsm = fsm;
        }

        internal abstract void Enter(FSM fsm);

        internal abstract void Exit(FSM fsm);

        internal virtual void Update(float deltaTime) { }

        internal virtual void Draw(SpriteBatch spriteBatch, float deltaTime) { }
    }
}
