using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace T2D
{
    class Sprite
    {
        protected Texture2D texture;

        protected Vector2 position;
        protected Vector2 velocity;

        protected Vector2 center;
        protected Vector2 origin;

        protected float rotation;

        public Vector2 Center
        {
            get { return this.center; }
        }
        public Vector2 Position
        {
            get { return this.position; }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = Vector2.Zero;

            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
