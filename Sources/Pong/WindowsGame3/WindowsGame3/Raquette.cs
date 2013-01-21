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

namespace Pong
{
    class Raquette : Sprite
    {
        // Récupère ou définit le nombre de points du joueur associé à la raquette
        public int NbPoints
        {
            get;
            set;
        }

        // Récupère ou définit le joueur associé à la raquette
        public int Joueur
        {
            get;
            set;
        }

        public Raquette(int joueur)
        {
            Joueur = joueur;
            NbPoints = 0;
        }

        // Initialisation de la raquette
        public override void Initialize()
        {
            base.Initialize();
            Direction = new Vector2(1,1);
 
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            // positionnement de la raquette
            if (Joueur == 1)
                Position = new Vector2(5, (Globals.hauteurFenetre / 2) - Texture.Height);
            else if (Joueur == 2)
                Position = new Vector2(Globals.largeurFenetre - Texture.Width - 5, (Globals.hauteurFenetre / 2) - Texture.Height);
            
        }

        public override void Update(GameTime gameTime)
        {
            //MàJ de la position de la raquette en fonction des entrées claviers
            Globals.keyboardState = Keyboard.GetState();
            if (Joueur == 2)
            {
                if (Globals.keyboardState.IsKeyDown(Keys.Up) && Position.Y > 0)
                {
                    Position.Y -=5;
                }
                else if (Globals.keyboardState.IsKeyDown(Keys.Down) && Position.Y + Texture.Height < Globals.hauteurFenetre)
                {
                    Position.Y +=5;
                }
            }
            else if (Joueur == 1)
            {
                if (Globals.keyboardState.IsKeyDown(Keys.Z) && Position.Y > 0)
                {
                    Position.Y -= 5;
                }
                else if (Globals.keyboardState.IsKeyDown(Keys.S) && Position.Y + Texture.Height < Globals.hauteurFenetre)
                {
                    Position.Y += 5;
                }
            }
            base.Update(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
    }
}
