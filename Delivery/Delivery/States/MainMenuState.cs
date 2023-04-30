using Delivery.Graphics;
using Delivery.Input;
using Delivery.StateMachine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States
{
    internal class MainMenuState : StateBase
    {
        private FullScreenClick _fsc;
        private NineSlice _border;
        private Spritesheet _font;
        private Color _fontColour;
        private Color _backgroundColour;

        internal MainMenuState(FSM fsm, DeliveryGame game)
            : base(fsm)
        {
            _border = new NineSlice(game.Content.Load<Texture2D>("border"), 3, 3);
            _border.Size = new Rectangle(0, 0, 256, 192);
            _fontColour = new Color(64, 80, 16);
            _backgroundColour = new Color(208, 208, 88);
            _font = new Spritesheet(game.Content.Load<Texture2D>("arcade-font"), 8, 8);
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
            {
                FSM.Game.Score = 0;
                FSM.Game.Damage = 0;

                FSM.ChangeState(FSM.Game.ROAD_STATE);
            }
        }

        internal override void Draw(SpriteBatch spriteBatch, float deltaTime)
        {
            _border.Draw(spriteBatch);
            _font.Message(spriteBatch, new Vector2(24, 16), "APOCALYPTIC PIZZA DELIVERY", _fontColour);
            _font.Message(spriteBatch, new Vector2(80, 32), "FOR LDJAM 53", _fontColour);
            _font.Message(spriteBatch, new Vector2(8 * 3, 8 * 10), "DELIVER PIZZA TO THE HOUSES", _fontColour);
            _font.Message(spriteBatch, new Vector2(8 , 8 * 11), "AVOID POTHOLES - WATCH DAMAGE!", _fontColour);
            _font.Message(spriteBatch, new Vector2(8 * 2, 8 * 14), "WASD MOVES TRUCK SPACE FIRES", _fontColour);
            _font.Message(spriteBatch, new Vector2(48, 22 * 8), "CLICK SCREEN TO PLAY", _fontColour);
        }
    }
}
