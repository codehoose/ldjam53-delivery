using Delivery.Input;
using Delivery.StateMachine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States
{
    internal class MainMenuState : StateBase
    {
        private readonly Texture2D _banner;
        private FullScreenClick _fsc;

        internal MainMenuState(FSM fsm, DeliveryGame game)
            : base(fsm)
        {
            _banner = game.Content.Load<Texture2D>("main-menu");
        }

        internal override void Enter(FSM fsm)
        {
            _fsc = null;
            _fsc = new FullScreenClick();
        }

        internal override void Exit(FSM fsm)
        {

        }

        internal override void Update(float deltaTime)
        {
            _fsc.Update();
            if (_fsc.Done)
                FSM.ChangeState(FSM.Game.CHOOSE_EQUIPMENT_STATE);
        }

        internal override void Draw(SpriteBatch spriteBatch, float deltaTime)
        {
            spriteBatch.Draw(_banner, new Rectangle(0, 0, 256, 192), Color.White);
        }
    }
}
