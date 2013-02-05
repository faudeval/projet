#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace PacMan
{
    class MobileSprite : Sprite
    {
        #region Fields
        protected Vector2 direction;
        protected Vector2 nextDirection;
        protected bool isBlocked;
        protected bool readyToTurn;

        protected float velocity;
        protected float rotation;
        #endregion

        #region Properties
        public Vector2 Direction
        {
            get { return this.direction; }
            set { this.direction = Vector2.Normalize(value); }
        }

        public Vector2 NextDirection
        {
            get { return this.nextDirection; }
            set { this.nextDirection = Vector2.Normalize(value); }
        }
        #endregion

        #region Initialization
        public MobileSprite(Texture2D texture, Vector2 position, Level level) : base(texture, position, level)
        {
            this.readyToTurn = false;
            this.rotation = 0;
            this.velocity = 0;
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.isBlocked = false;
            if (this.direction != this.nextDirection)
            {/*
                if (readyToTurn)
                {
                    if(this.level.seePath(this.position, this.direction)
                }*/
                if (!this.level.IsOut(this.position + this.nextDirection, this is Pacman)) // Si la direction est différente de celle demandée, et qu'il est possible de tourner
                {
                    this.Direction = this.NextDirection;
                    this.rotation = (float)Math.Atan2(this.Direction.X, -this.Direction.Y) - (float)(Math.PI / 2);
                }
            }

            Vector2 nextPosition = this.position + this.velocity * this.direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!this.level.IsOut(nextPosition, this is Pacman)) // Si la position visée est possible
                this.position = nextPosition;
            else
            {
                if (nextPosition.X < this.level.Teleport[0].X) // Si la position visée est après le point de téléport 0
                    this.position = this.level.Teleport[1];
                else if (nextPosition.X > this.level.Teleport[1].X) // Si la position visée est après le point de téléport 1
                    this.position = this.level.Teleport[0];
                else // Sinon (la position demandée est dans le mur)
                {
                    if (this.direction == Vector2.UnitX) // Si on va vers la droite
                        nextPosition = new Vector2(((int)(this.position.X / 16) + 1) * 16, this.position.Y); // On se colle contre le bord droit
                    else if (this.direction == -Vector2.UnitX) // Si on va vers la gauche
                        nextPosition = new Vector2(((int)(this.position.X / 16)) * 16, this.position.Y); // On se colle contre le bord gauche
                    else if (this.direction == Vector2.UnitY) // Si on va vers le bas
                        nextPosition = new Vector2(this.position.X, ((int)(this.position.Y / 16) + 1) * 16); // On se colle contre le bord bas
                    else if (this.direction == -Vector2.UnitY) // Si on va vers le haut
                        nextPosition = new Vector2(this.position.X, ((int)(this.position.Y / 16)) * 16); // On se colle contre le bord haut
                    if (!this.level.IsOut(nextPosition, this is Pacman))
                        this.position = nextPosition;
                    this.isBlocked = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, Center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
        #endregion
    }
}
