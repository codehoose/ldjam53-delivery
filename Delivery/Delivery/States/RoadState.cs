using Delivery.FX;
using Delivery.StateMachine;
using Delivery.States.Road;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States
{
    internal class RoadState : StateBase
    {
        private RollingRoad _road;
        private Truck _truck;
        private PotholeSpawner _potholes;
        private float _durationMs;
        private bool _endRollingRoad;
        private float _pauseMs;
        private Texture2D[] _houses = new Texture2D[3];


        public RoadState(FSM fsm) : base(fsm)
        {
            
        }

        internal override void Enter(FSM fsm)
        {
            _road = new RollingRoad(fsm.Game, 48);
            _truck = new Truck(fsm.Game);
            _potholes = new PotholeSpawner(fsm.Game, 48, 3000);

            _houses[0] = FSM.Game.Content.Load<Texture2D>("house");
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

            Rumble.Instance.IsActive = !_endRollingRoad && _potholes.Collision(_truck.Bounds);
            Rumble.Instance.Update(deltaTime);

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
            spriteBatch.Draw(_houses[0], new Vector2(0, 12) + Rumble.Instance.Offset, Color.White);

            _potholes.Draw(spriteBatch, Rumble.Instance.Offset);
            _truck.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);
        }
    }
}
