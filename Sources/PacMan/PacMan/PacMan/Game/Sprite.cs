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
        protected Vector2 direction;
        protected Vector2 center;
        protected Vector2 origin;
        protected Level level;

        protected float velocity;
        protected float rotation;
        protected float scale;

        protected int Line
        {
            get { return (int)this.position.Y / Level.TILE_HEIGHT; }
        }
        protected int Column
        {
            get { return (int)this.position.X / Level.TILE_WIDTH; }
        }
        public Vector2 Center
        {
            get { return this.center; }
        }
        public Vector2 Position
        {
            get { return this.position; }
        }
        public Vector2 Direction
        {
            get { return this.direction; }
            set { this.direction = Vector2.Normalize(value); }
        }
        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        public Sprite(Texture2D texture, Vector2 position, Level level)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = 0;
            this.level = level;

            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            Vector2 nextPosition = this.position + this.velocity * this.direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!Out(nextPosition))
                this.position = nextPosition;
            else
            {
                if (this.direction == Vector2.UnitX) // Si on va vers la droite
                    nextPosition = new Vector2(((int)(this.position.X / 16) + 1) * 16, this.position.Y); // On se colle contre le bord droit
                else if (this.direction == -Vector2.UnitX) // Si on va vers la gauche
                    nextPosition = new Vector2(((int)(this.position.X / 16)) * 16, this.position.Y); // On se colle contre le bord gauche
                else if (this.direction == Vector2.UnitY) // Si on va vers le bas
                    nextPosition = new Vector2(this.position.X, ((int)(this.position.Y / 16) + 1) * 16); // On se colle contre le bord bas
                else if (this.direction == -Vector2.UnitY) // Si on va vers le haut
                    nextPosition = new Vector2(this.position.X, ((int)(this.position.Y / 16)) * 16); // On se colle contre le bord haut
                if (!Out(nextPosition))
                    this.position = nextPosition;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Méthode vérifiant si une position est dans le décor
        /// </summary>
        /// <param name="position">Vector2 représentant la position à vérifier</param>
        /// <returns>Faux si la position (et la surface associée) est totalement dans le chemin, Vrai si elle sort du niveau ou du chemin</returns>
        private bool Out(Vector2 position)
        {
            int top = (int)(position.Y / Level.TILE_HEIGHT);
            int left = (int)(position.X / Level.TILE_WIDTH);
            int bot = (int)((position.Y + Level.TILE_HEIGHT - 1) / Level.TILE_HEIGHT);
            int right = (int)((position.X + Level.TILE_WIDTH - 1) / Level.TILE_WIDTH);
            if (top < 0 || left < 0 || right >= level.Width || bot >= level.Height) // true si un des bords est hors du niveau
                return true;
            return (level.Map[top, left] == 1 ||
                    level.Map[top, right] == 1 ||
                    level.Map[bot, left] == 1 ||
                    level.Map[bot, right] == 1); // True si l'un des côtés est dans le décor, false sinon
        }
    }
}
