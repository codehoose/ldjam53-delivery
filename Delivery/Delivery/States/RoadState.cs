using Delivery.FX;
using Delivery.Graphics;
using Delivery.StateMachine;
using Delivery.States.Road;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Delivery.States
{
    internal class RoadState : StateBase
    {
        private RollingRoad _road;
        private Truck _truck;
        private PotholeSpawner _potholes;
        private float _durationMs;
        private bool _endRollingRoad;
        private EnvironmentManager _environmentManager;
        private float _pauseMs;
        private Spritesheet _font;
        private Color _fontColour;
        private Color _backgroundColour;
        private Texture2D _background;

        public RoadState(FSM fsm) : base(fsm)
        {
            
        }

        internal override void Enter(FSM fsm)
        {
            _road = new RollingRoad(fsm.Game, fsm.Game.RollingRoadSpeedPixels);
            _truck = new Truck(fsm.Game);
            _potholes = new PotholeSpawner(fsm.Game, fsm.Game.RollingRoadSpeedPixels, 3000);
            _environmentManager = new EnvironmentManager(fsm.Game, fsm.Game.RollingRoadSpeedPixels);
            _font = new Spritesheet(fsm.Game.Content.Load<Texture2D>("arcade-font"), 8, 8);
            _fontColour = new Color(64, 80, 16);
            _backgroundColour = new Color(208, 208, 88);
            _background = new Texture2D(fsm.Game.GraphicsDevice, 1, 1);
            _background.SetData<Color>(new Color[] { Color.White });
        }

        internal override void Exit(FSM fsm)
        {

        }

        internal override void Update(float deltaTime)
        {
            if (_endRollingRoad)
            {
                _truck.MoveToSide(deltaTime);
                if (_truck.IsAtSide)
                {
                    _pauseMs += deltaTime * 1000;
                    if (_pauseMs >= 3000)
                    {
                        FSM.ChangeState(FSM.Game.MAIN_MENU_STATE);
                    }
                }
            }
            else
            {
                _truck.Update(deltaTime);
            }

            if (!_endRollingRoad || (_endRollingRoad && !_truck.IsAtSide))
            {
                _road.Update(deltaTime);
            }


            _potholes.Update(deltaTime);
            _environmentManager.Update(deltaTime);

            Rumble.Instance.IsActive = !_endRollingRoad && _potholes.Collision(_truck.Bounds);
            Rumble.Instance.Update(deltaTime);


            List<Rectangle> hitPoints = _environmentManager.GetBounds();
            if (hitPoints.Count > 0)
            {
                int pieIndex = 0;
                while (pieIndex < _truck.ActivePizzas.Count)
                {
                    bool isHit = false;
                    foreach (var hit in hitPoints)
                    {
                        if (hit.Contains(_truck.ActivePizzas[pieIndex]))
                        {
                            isHit = true;
                            break;
                        }
                    }

                    if (isHit)
                    {
                        _truck.ActivePizzas.RemoveAt(pieIndex);
                    }
                    else
                    {
                        pieIndex++;
                    }
                }
            }

            _durationMs += deltaTime * 1000;
            if (_durationMs >= FSM.Game.RoadDurationMs)
            {
                //_endRollingRoad = true;
            }

            if (_durationMs >= FSM.Game.StopSpawningPotHolesMs)
            {
                _potholes.StopSpawningPotholes();
            }
        }

        internal override void Draw(SpriteBatch spriteBatch, float deltaTime)
        {
            _road.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);
            _environmentManager.Draw(spriteBatch);

            _potholes.Draw(spriteBatch, Rumble.Instance.Offset);
            _truck.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);

            spriteBatch.Draw(_background, new Rectangle(0, 0, 256, 12), _backgroundColour);
            _font.Message(spriteBatch, new Vector2(2, 2), "TESTING", _fontColour);
        }
    }
}
