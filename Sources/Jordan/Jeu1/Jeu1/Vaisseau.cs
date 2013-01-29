using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Jeu1
{
    class Vaisseau : Sprite
    {
        public Texture2D vaisseaugauche;
        public Texture2D vaisseaudroite;
        public Texture2D vaisseauTouche;
        public bool vaisseauHit;

        public Vaisseau()
        {
            Speed = 9;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            vaisseaugauche = content.Load<Texture2D>("vaisseaugauche");
            vaisseaudroite = content.Load<Texture2D>("vaisseaudroite");
            vaisseauTouche = content.Load<Texture2D>("vaisseauTouche");
            Position.X = Global.largeurFenetre / 2 - texture.Width / 2;
            Position.Y = Global.hauteurFenetre - texture.Height;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Global.clavier = Keyboard.GetState();
            Global.pad = GamePad.GetState(PlayerIndex.One);
            if (Global.clavier.IsKeyDown(Keys.Left) && Global.pad.IsConnected == false)
            {   Position.X -= Speed;
                if (Position.X <= -Texture.Width)
                    Position.X = Global.largeurFenetre - Texture.Width;
            }

            if (Global.clavier.IsKeyDown(Keys.Right)  && Global.pad.IsConnected == false)
            {
                Position.X += Speed;
                if (Position.X >=Global.largeurFenetre)
                    Position.X = 0;
            }

            if (Global.pad.ThumbSticks.Left.X != 0)
            {
                Position.X += Convert.ToInt32(Global.pad.ThumbSticks.Left.X) * Speed;
                if (Position.X <= -Texture.Width)
                    Position.X = Global.largeurFenetre - Texture.Width;
                if (Position.X >= Global.largeurFenetre)
                    Position.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            //Gestion de l'affichage en fonction du clavier si le pas est déconnecté
            if (Global.pad.IsConnected == false)
            {
                if (Global.clavier.IsKeyDown(Keys.Left) && Global.clavier.IsKeyUp(Keys.Right) && !vaisseauHit)
                    spriteBatch.Draw(vaisseaugauche, Position, Color.White);
                if (Global.clavier.IsKeyDown(Keys.Right) && Global.clavier.IsKeyUp(Keys.Left) && !vaisseauHit)
                    spriteBatch.Draw(vaisseaudroite, Position, Color.White);
                if (Global.clavier.IsKeyUp(Keys.Right) && Global.clavier.IsKeyUp(Keys.Left) && !vaisseauHit
                    || Global.clavier.IsKeyDown(Keys.Right) && !Global.clavier.IsKeyUp(Keys.Left))
                    base.Draw(spriteBatch, gameTime);
                if (vaisseauHit)
                {
                    spriteBatch.Draw(vaisseauTouche, Position, Color.White);
                }
            }

            //Gestion de l'affichage des sprite si le pad est connecté
            if (Global.pad.IsConnected == true)
            {
                if (Global.pad.ThumbSticks.Left.X > 0 && !vaisseauHit)
                    spriteBatch.Draw(vaisseaudroite, Position, Color.White);
                if (Global.pad.ThumbSticks.Left.X < 0 && !vaisseauHit)
                    spriteBatch.Draw(vaisseaugauche, Position, Color.White);
                if(Global.pad.ThumbSticks.Left.X ==0 && !vaisseauHit)
                    base.Draw(spriteBatch, gameTime);
                if (vaisseauHit)
                {
                    spriteBatch.Draw(vaisseauTouche, Position, Color.White);
                }

            }
        }
    }
}
