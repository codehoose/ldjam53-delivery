using Delivery.StateMachine;
using Delivery.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Delivery
{
    public class DeliveryGame : Game
    {
        public readonly string MAIN_MENU_STATE = nameof(MAIN_MENU_STATE);
        public readonly string CHOOSE_EQUIPMENT_STATE = nameof(CHOOSE_EQUIPMENT_STATE);
        public readonly string PLAY_GAME_STATE  = nameof(PLAY_GAME_STATE);
        public readonly string ROAD_STATE = nameof(ROAD_STATE);
        public readonly string THROW_PIES_STATE = nameof(THROW_PIES_STATE);

        private readonly Dictionary<string, StateBase> _states = new Dictionary<string, StateBase>();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private FSM _fsm;

        internal Dictionary<string, StateBase> States => _states;

        internal int StopSpawningPotHolesMs { get; set; } = 12000;
        internal int RoadDurationMs { get; set; } = 16000;


        public DeliveryGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _fsm = new FSM(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _states.Add(MAIN_MENU_STATE, new MainMenuState(_fsm, this));
            _states.Add(CHOOSE_EQUIPMENT_STATE, new ChooseEquipmentState(_fsm));
            _states.Add(ROAD_STATE, new RoadState(_fsm));
            // TODO: LOAD OTHER STATES

            _fsm.ChangeState(ROAD_STATE);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _fsm.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            using (RenderTarget2D target = new RenderTarget2D(GraphicsDevice, 256, 192))
            {
                GraphicsDevice.SetRenderTarget(target);

                _spriteBatch.Begin();
                _fsm.Draw(_spriteBatch, gameTime);
                _spriteBatch.End();


                Rectangle rect = new Rectangle(0,
                                               0,
                                               1024,
                                               768);

                GraphicsDevice.SetRenderTarget(null);
                _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                _spriteBatch.Draw(target, rect, Color.White);
                _spriteBatch.End();
            }
            
            base.Draw(gameTime);
        }
    }
}