using Delivery.FX;
using Delivery.Graphics;
using Delivery.SoundFx;
using Delivery.StateMachine;
using Delivery.States.Road;
using Delivery.Timed;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Delivery.States
{
    internal class RoadState : StateBase
    {
        static readonly float DAMAGE_FROM_POTHOLE = 0.001f;

        private RollingRoad _road;
        private Truck _truck;
        private PotholeSpawner _potholes;
        private bool _endRollingRoad;
        private EnvironmentManager _environmentManager;
        private float _pauseMs;
        private Spritesheet _font;
        private Color _fontColour;
        private Color _backgroundColour;
        private Texture2D _background;
        private OneTimeClip _potholeSfx;
        private OneTimeClip _explosionSfx;

        private TimedAction _scoreUpdater;

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
            _scoreUpdater = new TimedAction(2000, () => FSM.Game.Score += 5);
            _potholeSfx = new OneTimeClip(fsm.Game.Content.Load<SoundEffect>("potholefx"));
            _explosionSfx = new OneTimeClip(fsm.Game.Content.Load<SoundEffect>("explosion"));
        }

        internal override void Exit(FSM fsm)
        {

        }

        internal override void Update(float deltaTime)
        {
            _potholeSfx.Update(deltaTime);
            _explosionSfx.Update(deltaTime);

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

            if (!_truck.IsAtSide)
            {
                _road.Update(deltaTime);
                _scoreUpdater.Update(deltaTime);
                _potholes.Update(deltaTime);
                _environmentManager.Update(deltaTime);
            }

            Rumble.Instance.IsActive = !_endRollingRoad && _potholes.Collision(_truck.Bounds);
            if (Rumble.Instance.IsActive)
            {
                FSM.Game.Damage += DAMAGE_FROM_POTHOLE;
                if (FSM.Game.Damage >= 1)
                {
                    _endRollingRoad = true;
                    _explosionSfx.Play();
                }
                else
                {
                    _potholeSfx.Play();
                }

            }
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
                        FSM.Game.Score += 100;
                    }
                    else
                    {
                        pieIndex++;
                    }
                }
            }
        }

        internal override void Draw(SpriteBatch spriteBatch, float deltaTime)
        {
            _road.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);
            _environmentManager.Draw(spriteBatch, Rumble.Instance.Offset);

            _potholes.Draw(spriteBatch, Rumble.Instance.Offset);
            _truck.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);

            spriteBatch.Draw(_background, new Rectangle(0, 0, 256, 12), _backgroundColour);
            _font.Message(spriteBatch, new Vector2(2, 2), $"SCORE: {FSM.Game.Score:000000}     DAMAGE:", _fontColour);

            spriteBatch.Draw(_background, new Rectangle(202, 2, 52, 8), _fontColour);

            float damage = Math.Clamp(FSM.Game.Damage, 0, 1);
            Rectangle bar = new Rectangle(203, 3, (int)(damage* 50), 6);
            spriteBatch.Draw(_background, bar, _backgroundColour);

            if (_truck.IsAtSide)
            {
                spriteBatch.Draw(_background, new Rectangle(88, 104, "GAME OVER!".Length * 8, 8), _backgroundColour);
                _font.Message(spriteBatch, new Vector2(88, 104), "GAME OVER!", _fontColour);
            }
        }
    }
}
