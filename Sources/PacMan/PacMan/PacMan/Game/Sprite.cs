using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan
{
    class Sprite
    {
        protected Texture2D texture;

        protected Vector2 position;
        protected Vector2 origin;
        protected Level level;

        protected float scale;

        public Vector2 Center
        {
            get { return new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2); }
        }
        public Vector2 Position
        {
            get { return this.position; }
        }
        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }
        public bool IsActive { get; set; }

        public Sprite(Texture2D texture, Vector2 position, Level level)
        {
            this.texture = texture;
            this.position = position;
            this.level = level;
            this.IsActive = true;

            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Center, null, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, Center, null, color);
        }
    }
}
