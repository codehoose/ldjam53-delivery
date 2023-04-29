using Delivery.StateMachine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States
{
    internal class ChooseEquipmentState : StateBase
    {
        private Texture2D _texture;

        public ChooseEquipmentState(FSM fsm) : base(fsm)
        {
            _texture = fsm.Game.Content.Load<Texture2D>("equip-truck");
        }

        internal override void Enter(FSM fsm)
        {
            
        }

        internal override void Exit(FSM fsm)
        {
            
        }

        internal override void Draw(SpriteBatch spriteBatch, float deltaTime)
        {
            spriteBatch.Draw(_texture, new Rectangle(0, 0, 256, 192), Color.White);
        }
    }
}
