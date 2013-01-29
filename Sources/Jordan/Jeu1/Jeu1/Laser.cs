using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Jeu1
{
    class Laser : Sprite
    {
         public bool first = true;
         public Texture2D laser2;
         public Texture2D laser3;
         public Texture2D laser4;
         public Texture2D laser5;
         public Texture2D laser6;
         public Texture2D laser7;
         public Texture2D laser8;
         public Texture2D laser9;
         public Texture2D laser10;
         public double tpspawn;
         public int spawn;
         double temps;

        public Laser()
        {
            Speed =3;
            tpspawn = 1.2;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            laser2 = content.Load<Texture2D>("laser2");
            laser3 = content.Load<Texture2D>("laser3");
            laser4 = content.Load<Texture2D>("laser4");
            laser5 = content.Load<Texture2D>("laser5");
            laser6 = content.Load<Texture2D>("laser6");
            laser7 = content.Load<Texture2D>("laser7");
            laser8 = content.Load<Texture2D>("laser8");
            laser9 = content.Load<Texture2D>("laser9");
            laser10 = content.Load<Texture2D>("laser10");

            Random r = new Random();
            int nbr = r.Next(Global.largeurFenetre - texture.Width + 1);
            Position.Y = - Texture.Height;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            Random r = new Random();
            int nbr = r.Next(Global.largeurFenetre - texture.Width + 1);

            if (Position.Y < Global.hauteurFenetre)
                Position.Y += Speed;
            else
            {
                temps+= gameTime.ElapsedGameTime.TotalSeconds;
                if(spawn <3)
                    Speed = 4; this.tpspawn = .2;
                if (spawn >= 3)
                    Speed = 4; this.tpspawn = .4;
                if (spawn >= 6)
                    Speed = 5; this.tpspawn = .6;
                if (spawn >= 9)
                    Speed = 6; this.tpspawn = .8;
                if (spawn >= 12)
                    Speed = 7; this.tpspawn = 1.0;

                if (spawn >=20)
                    Speed = 8; this.tpspawn = 1.3;

                if (temps > this.tpspawn)
                {
                    Position.Y = 0;
                    spawn += 1;
                    temps = 0;
                }
                
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            if (spawn >= 3)
                spriteBatch.Draw(laser2, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 6)
                    spriteBatch.Draw(laser3, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 9)
                    spriteBatch.Draw(laser4, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 12)
                    spriteBatch.Draw(laser5, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 15)
                    spriteBatch.Draw(laser6, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 18)
                    spriteBatch.Draw(laser7, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 21)
                    spriteBatch.Draw(laser8, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 24)
                    spriteBatch.Draw(laser9, Position, Microsoft.Xna.Framework.Color.White);
                if (spawn >= 27)
                    spriteBatch.Draw(laser10, Position, Microsoft.Xna.Framework.Color.White);
        }
    }
}
