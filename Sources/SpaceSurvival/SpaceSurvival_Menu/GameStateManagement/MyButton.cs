using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    public class MyButton
    {

        /*
         * TODO :
         * Dessiner un bouton extensible à partir d'une icône et un texte (et éventuellement d'une couleur principale)
         */
        /// <summary>
        /// Rectangle définissant la surface clicable
        /// </summary>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Image du bouton
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Texte du bouton
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Position du texte par rapport au bouton
        /// </summary>
        public Vector2 TextPosition
        {
            get { return textPosition + Position; }
            set { textPosition = value; }
        }

        /// <summary>
        /// Position du bouton
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(Bounds.X, Bounds.Y); }
            set { Bounds = new Rectangle((int)value.X, (int)value.Y, Texture.Width, Texture.Height); }
        }
        Vector2 textPosition;

        public MyButton()
        { }

        public MyButton(string text, Texture2D texture)
        {
            Text = text;
            Texture = texture;
            Bounds = Texture.Bounds;
            TextPosition = new Vector2(100, 20);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.Draw(Texture, Bounds, color);
            spriteBatch.DrawString(font, Text, TextPosition, color);
        }

        public bool IsClicked(MouseState ms)
        {
            return ms.LeftButton == ButtonState.Pressed && Bounds.Contains(new Point(ms.X, ms.Y));
        }
    }
}
