using Microsoft.Xna.Framework.Audio;

namespace Delivery.SoundFx
{
    internal class OneTimeClip
    {
        float _ms;
        float _clipMs;
        private SoundEffect _effect;

        public OneTimeClip(SoundEffect effect)
        {
            _effect = effect;
            _clipMs = (float)_effect.Duration.TotalMilliseconds;
        }

        public void Play()
        {
            if (_ms == 0)
            {
                _effect.Play();
                _ms = _clipMs;
            }
        }

        public void Update(float deltaTime)
        {
            _ms -= (deltaTime * 1000);
            if (_ms < 0)
                _ms = 0;
        }
    }
}
