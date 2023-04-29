using Delivery.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Delivery.States.Road
{
    internal class EnvironmentManager
    {
        private readonly int BLOCK_SIZE = 128;

        class EnvObj
        {
            public Texture2D texture;
            public Vector2 offset;
            public Rectangle hitZone;
        }

        private List<EnvObj> _objs = new List<EnvObj>();

        private float _speed;
        private float _xoff;

        private Texture2D _woods;
        private Texture2D _house;

        public List<Rectangle> GetBounds()
        {
            List<Rectangle> rects = new List<Rectangle>();
            foreach(var obj in _objs)
            {
                if (obj.hitZone != Rectangle.Empty)
                {
                    rects.Add(obj.hitZone.Add(obj.offset));
                }
            }

            return rects;
        }

        internal EnvironmentManager(DeliveryGame game, float speed = 16f)
        {
            _speed = speed;
            _woods = game.Content.Load<Texture2D>("woods");
            _house = game.Content.Load<Texture2D>("house2");

            _objs.Add(new EnvObj() { texture = _woods, offset = new Vector2(0, 12) });
            _objs.Add(new EnvObj() { texture = _woods, offset = new Vector2(BLOCK_SIZE, 12) });
            _objs.Add(new EnvObj() { texture = _woods, offset = new Vector2(BLOCK_SIZE * 2, 12) });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 firstPos = _objs[0].offset.RoundPixel();
            for (int i = 0; i < _objs.Count; i++)
            {
                spriteBatch.Draw(_objs[i].texture, firstPos, Color.White);
                firstPos.X += BLOCK_SIZE;
            }
        }

        public void Update(float deltaTime)
        {
            int i = 0;
            while (i < _objs.Count)
            {
                _objs[i].offset -= new Vector2(deltaTime * _speed, 0);
                if (_objs[i].offset.X <= -BLOCK_SIZE)
                    _objs.RemoveAt(i);
                else
                    i++;
            }

            _xoff -= deltaTime * _speed;
            if ((int)(_xoff) == -BLOCK_SIZE)
            {
                _xoff = 0;
                _objs.Add(Random.Shared.Next(0, 10) > 5 ? CreateHouse() : CreateWoods());
            }
        }

        private EnvObj CreateHouse()
        {
            return new EnvObj()
            { 
                texture = _house, 
                offset = new Vector2(BLOCK_SIZE * 2, 12), 
                hitZone = new Rectangle(55, 35, 20, 25)
            };
        }

        private EnvObj CreateWoods()
        {
            return new EnvObj() { texture = _woods, offset = new Vector2(BLOCK_SIZE * 2, 12) };
        }
    }
}
