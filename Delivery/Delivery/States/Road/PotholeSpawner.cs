using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Delivery.States.Road
{
    internal class PotholeSpawner
    {
        private readonly int POTHOLE_SIZE = 24;
        private Texture2D[] _potholes = new Texture2D[3];
        private float _speed;
        private readonly float _spawnSpeed;
        private float _spawnCooldown;
        private bool _stopSpawningPotholes;

        private List<Vector2> _positions = new List<Vector2>();
        private List<int> _indexes = new List<int>();

        public PotholeSpawner(DeliveryGame game, float speed, float spawnSpeedMs)
        {
            _potholes[0] = game.Content.Load<Texture2D>("pothole");
            _potholes[1] = game.Content.Load<Texture2D>("pothole-2");
            _potholes[2] = game.Content.Load<Texture2D>("pothole-3");
            _speed = speed;
            _spawnSpeed = spawnSpeedMs;
            _spawnCooldown = _spawnSpeed / 2; // First one is a bit quicker than the rest
        }

        public void StopSpawningPotholes()
        {
            _stopSpawningPotholes = true;
        }

        public bool Collision(Rectangle rect)
        {
            foreach(var pothole in _positions)
            {
                Rectangle r = new Rectangle((int)pothole.X, (int)pothole.Y, POTHOLE_SIZE, POTHOLE_SIZE);
                if (r.Intersects(rect))
                    return true;
            }

            return false;
        }

        public void Update(float deltaTime)
        {
            _spawnCooldown += deltaTime * 1000f;
            if (_spawnCooldown >= _spawnSpeed && !_stopSpawningPotholes)
            {
                int[] yoffsets = new int[] { 0, 15, 30, 45, 60 };
                int offset = yoffsets[Random.Shared.Next(0, yoffsets.Length)];

                _positions.Add(new Vector2(256, offset + (192 - POTHOLE_SIZE) / 2) );
                _indexes.Add(Random.Shared.Next(0, 3));
                _spawnCooldown -= _spawnSpeed;
            }

            int i = 0;
            while (i < _positions.Count)
            {
                _positions[i] = _positions[i] - new Vector2(deltaTime * _speed, 0f);
                if (_positions[i].X < -POTHOLE_SIZE)
                {
                    _positions.RemoveAt(i);
                    _indexes.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            for (int i = 0; i < _positions.Count; i++)
            {
                spriteBatch.Draw(_potholes[_indexes[i]], _positions[i] + offset, Color.White);
            }
        }
    }
}
