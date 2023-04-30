using System;

namespace Delivery.Timed
{
    internal class TimedAction
    {
        private int _ms;
        private int _callAfterMs;
        private Action _action;

        public TimedAction(int callAfterMs, Action action)
        {
            _callAfterMs = callAfterMs;
            _action = action;
            _ms = _callAfterMs;
        }

        public void Update(float deltaTime)
        {
            _ms -= (int)(deltaTime * 1000);
            if (_ms < 0)
            {
                _ms += _callAfterMs;
                _action?.Invoke();
            }
        }
    }
}
