using Microsoft.Xna.Framework;
using System;

namespace Delivery.FX
{
    internal class Rumble
    {
        private static Rumble _instance;

        public static Rumble Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Rumble();
                }

                return _instance;
            }
        }

        private Vector2 _offset;

        public Vector2 Offset => IsActive ? _offset : Vector2.Zero;

        public bool IsActive { get; set; }

        public float Distance { get; set; } = 2f;

        public void Update(float deltaTime)
        {
            if (IsActive)
            {
                _offset = Vector2.UnitX * Random.Shared.Next(0, (int)Distance);
                _offset += Vector2.UnitY * Random.Shared.Next(0, (int)Distance);
            }
        }

        private Rumble()
        {

        }
    }
}
