using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jeu1
{
    class Life : Sprite
    {
        public int nbVies;
        public Texture2D heart2;
        public Texture2D heart1;
        public Life()
        {
            nbVies=3;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            heart2 = content.Load<Texture2D>("heartv2");
            heart1 = content.Load<Texture2D>("heartv1");
            Position = new Microsoft.Xna.Framework.Vector2(0,0);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        { 
            if (nbVies == 3)
                base.Draw(spriteBatch, gameTime);
            if (nbVies == 2)
                spriteBatch.Draw(heart2, Position, Color.White);
            if (nbVies == 1)
                spriteBatch.Draw(heart1, Position, Color.White);
        }
    }
}
