using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Delivery.Graphics
{
    internal class NineSlice
    {
        private Spritesheet _sheet;
        private Texture2D _tex;

        public Rectangle Size { get; set; }

        public NineSlice(Texture2D tex, int columns, int rows)
        {
            _tex = tex;
            _sheet = new Spritesheet(_tex, columns, rows);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle size = Size == Rectangle.Empty ? _tex.Bounds : Size;

            // Corners are index 0, 2
            //                   6  8

            // Slices are 012
            //            345
            //            678

            _sheet.Draw(spriteBatch, 0, new Vector2(size.X, size.Y), Color.White);
            _sheet.Draw(spriteBatch, 2, new Vector2(size.X + size.Width - _sheet.CellWidth, size.Y), Color.White);
            _sheet.Draw(spriteBatch, 6, new Vector2(size.X, size.Y + size.Height - _sheet.CellHeight), Color.White);
            _sheet.Draw(spriteBatch, 8, new Vector2(size.X + size.Width - _sheet.CellWidth, size.Y + size.Height - _sheet.CellHeight), Color.White);

            _sheet.Draw(spriteBatch, new Rectangle(size.X + _sheet.CellWidth, size.Y + _sheet.CellHeight, size.Width - _sheet.CellWidth * 2, size.Height - _sheet.CellHeight * 2), 4, Color.White);
            _sheet.Draw(spriteBatch, new Rectangle(size.X + _sheet.CellWidth, size.Y, size.Width - _sheet.CellWidth * 2, _sheet.CellHeight ), 1, Color.White);
            _sheet.Draw(spriteBatch, new Rectangle(size.X + _sheet.CellWidth, size.Height + size.Y - _sheet.CellHeight, size.Width - _sheet.CellWidth * 2, _sheet.CellHeight), 7, Color.White);

            _sheet.Draw(spriteBatch, new Rectangle(size.X, _sheet.CellHeight, _sheet.CellWidth, size.Height - _sheet.CellHeight * 2), 3, Color.White);
            _sheet.Draw(spriteBatch, new Rectangle(size.X + size.Width - _sheet.CellWidth, _sheet.CellHeight, _sheet.CellWidth, size.Height - _sheet.CellHeight * 2), 5, Color.White);
        }
    }
}
