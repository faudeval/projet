using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame3
{
    class Sprite
    {
        private Vector2 _direction;
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = Vector2.Normalize(value); }
        }
        public float Vitesse { get; set; }

        public virtual void Initialize()
        {
            Position = Vector2.Zero;
            Direction = Vector2.Zero;
            Vitesse = 0f;
        }

        public virtual void LoadContent(ContentManager cm, String name)
        {
            Texture = cm.Load<Texture2D>(name);
        }

        public virtual void Update(GameTime gt)
        {
            Position += Direction * Vitesse * (float)gt.ElapsedGameTime.TotalMilliseconds;
        }

        public virtual void HandleInput(KeyboardState ks, MouseState ms)
        {
        }

        public virtual void Draw(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(Texture, Position, Color.White);
        }
    }
}
