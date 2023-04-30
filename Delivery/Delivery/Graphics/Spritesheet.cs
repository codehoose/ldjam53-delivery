using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.Graphics
{
    internal class Spritesheet
    {
        private Texture2D _texture;
        private int _columns;
        private int _rows;
        private int _cellWidth;
        private int _cellHeight;

        public Spritesheet(Texture2D fontTexture, int columns, int rows)
        {
            _texture = fontTexture;
            _columns = columns;
            _rows = rows;
            _cellWidth = _texture.Width / columns;
            _cellHeight = _texture.Height / rows;
        }

        public void Message(SpriteBatch spriteBatch, Vector2 pos, string msg, Color color)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                Vector2 loc = pos + new Vector2(i * _cellWidth, 0);
                int index = msg[i] - ' ';
                int px = index % _columns;
                int py = index / _columns;
                Rectangle src = new Rectangle(px * _cellWidth, py * _cellHeight, _cellWidth, _cellHeight);
                spriteBatch.Draw(_texture, loc, src, color);
            }
        }
    }
}
