﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.States.Road
{
    internal class PotholeSpawner
    {
        private Texture2D _pothole;
        private float _speed;
        private readonly float _spawnSpeed;
        private float _spawnCooldown;

        public List<Vector2> _positions = new List<Vector2>();

        public PotholeSpawner(DeliveryGame game, float speed, float spawnSpeedMs)
        {
            _pothole = game.Content.Load<Texture2D>("pothole");
            _speed = speed;
            _spawnSpeed = spawnSpeedMs;
            _spawnCooldown = _spawnSpeed / 2; // First one is a bit quicker than the rest
        }

        public bool Collision(Rectangle rect)
        {
            foreach(var pothole in _positions)
            {
                Rectangle r = new Rectangle((int)pothole.X, (int)pothole.Y, 32, 32);
                if (r.Intersects(rect))
                    return true;
            }

            return false;
        }

        public void Update(float deltaTime)
        {
            _spawnCooldown += deltaTime * 1000f;
            if (_spawnCooldown >= _spawnSpeed)
            {
                int[] yoffsets = new int[] { -60, 0, 60 };
                int offset = yoffsets[Random.Shared.Next(0, 3)];

                _positions.Add(new Vector2(256, offset + (192 - 32) / 2) );
                _spawnCooldown -= _spawnSpeed;
            }


            int i = 0;
            while (i < _positions.Count)
            {
                _positions[i] = _positions[i] - new Vector2(deltaTime * _speed, 0f);
                if (_positions[i].X < -32)
                {
                    _positions.RemoveAt(i);
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
                spriteBatch.Draw(_pothole, _positions[i] + offset, Color.White);
            }
        }
    }
}