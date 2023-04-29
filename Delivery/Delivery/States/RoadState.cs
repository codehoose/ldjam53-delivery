using Delivery.FX;
using Delivery.StateMachine;
using Delivery.States.Road;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.States
{
    internal class RoadState : StateBase
    {
        private RollingRoad _road;
        private Truck _truck;
        private PotholeSpawner _potholes;

        public RoadState(FSM fsm) : base(fsm)
        {
            
        }

        internal override void Enter(FSM fsm)
        {
            _road = new RollingRoad(fsm.Game, 48);
            _truck = new Truck(fsm.Game);
            _potholes = new PotholeSpawner(fsm.Game, 48, 3000);
        }

        internal override void Exit(FSM fsm)
        {

        }

        internal override void Update(float deltaTime)
        {
            _truck.Update(deltaTime);
            _potholes.Update(deltaTime);
            Rumble.Instance.IsActive = _potholes.Collision(_truck.Bounds);
            Rumble.Instance.Update(deltaTime);
        }

        internal override void Draw(SpriteBatch spriteBatch, float deltaTime)
        {
            _road.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);
            _potholes.Draw(spriteBatch, Rumble.Instance.Offset);
            _truck.Draw(spriteBatch, Rumble.Instance.Offset, deltaTime);
        }
    }
}
