#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SpaceSurvival_Optimise
{
    public class Obj
    {
        #region Fields
        protected Vector2 direction;
        protected int HP { get; set; }
        public int Team { get; set; }

        public Texture2D Image { get; protected set; }
        public float Speed { get; protected set; }
        public Vector2[] HitBox { get; protected set; }
        public bool IsReady { get; protected set; }
        public bool IsActive { get; set; }
        public Vector2 Position { get; protected set; }
        public Vector2 IntPosition
        {
            get { return new Vector2((int)Position.X, (int)Position.Y); }
        }
        public Vector2 Direction
        {
            get { return direction; }
            protected set { direction = Vector2.Normalize(value); }
        }
        public virtual Rectangle bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height); }
        }
        #endregion

        #region Initialization
        public Obj(params Object[] param)
        {
            IsReady = false;
            HP = 1;
            init(param);
            IsActive = true;
            IsReady = true;
        }

        protected virtual void init(params Object[] param)
        { }
        #endregion

        #region Methods
        public virtual void Hit()
        {
            HP -= 1;
            if (HP <= 0)
                IsActive = false;
        }
        #endregion

        #region Update & Draw
        public virtual void Update(GameTime gameTime)
        {
            if (IsActive && IsReady)
            {
                Position += Direction * Speed;
                if (Position.X > SpaceSurvival.game.Window.ClientBounds.Width || Position.Y > SpaceSurvival.game.Window.ClientBounds.Height || Position.X < 0 || Position.Y < 0)
                    IsActive = false;
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (IsActive && IsReady)
                SpaceSurvival.spriteBatch.Draw(Image, Position, Color.White);
        }
        #endregion
    }
}
