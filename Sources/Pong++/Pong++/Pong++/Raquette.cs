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

namespace Pong__
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
            Placement();       
        }

        public void Update(GameTime gameTime, Balle b1, Balle b2)
        {
            Deplacements(b1, b2);
            base.Update(gameTime);   
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }


        // Permet de replacer les raquettes
        public void Placement()
        {
            switch (Joueur)
            {
                case 1:
                    Position = new Vector2(5, (Globals.hauteurFenetre / 2) - Texture.Height);
                    break;
                case 2:
                    Position = new Vector2(Globals.largeurFenetre - Texture.Width - 5, (Globals.hauteurFenetre / 2) - Texture.Height);
                    break;
            }
        }

        //MàJ de la position de la raquette en fonction des entrées claviers
        public void Deplacements(Balle b1, Balle b2)
        {
            Globals.keyboardState = Keyboard.GetState();
            switch (Joueur)
            {
                case 1:
                    if (!Globals.IsAgainstIA)
                    {
                        if (Globals.keyboardState.IsKeyDown(Keys.Z) && Position.Y > 0)
                        {
                            Position.Y -= 7;
                        }
                        else if (Globals.keyboardState.IsKeyDown(Keys.S) && Position.Y + Texture.Height < Globals.hauteurFenetre)
                        {
                            Position.Y += 7;
                        }
                    }
                    else
                    {
                        IA(b1);
                        IA(b2);
                    }
                    break;
                case 2: if (Globals.keyboardState.IsKeyDown(Keys.Up) && Position.Y > 0)
                    {
                        Position.Y -= 7;
                    }
                    else if (Globals.keyboardState.IsKeyDown(Keys.Down) && Position.Y + Texture.Height < Globals.hauteurFenetre)
                    {
                        Position.Y += 7;
                    }
                    break;
            }
            
        }

        public void VerifCollision(Balle balle)
        {
            if (Rectangle.Intersects(balle.Rectangle))
            {
                if ((balle.Rectangle.Y - balle.Rectangle.Height <= Rectangle.Y || balle.Rectangle.Y >= Rectangle.Y + Rectangle.Height - 3)
                    && (balle.Rectangle.X >= Rectangle.X && balle.Rectangle.X <= Rectangle.X + Rectangle.Width))
                    balle.InverserY();
                else
                    balle.InverserX();
                balle.Speed += 0.05f;
            }
        }

        public void IA(Balle balle)
        {
            //foreach(Balle balle in balles){
            if (balle.Position.X < Globals.largeurFenetre / 2 && balle.Direction.X < 0)
            {
                Speed = 0.5f;
                if (balle.Position.Y < Position.Y)
                    Direction = new Vector2(0, -1);
                else if (balle.Position.Y > Position.Y + Texture.Height)
                    Direction = new Vector2(0, 1);

                if (Position.Y > 0 && Position.Y + Texture.Height < Globals.hauteurFenetre)
                    Position += Direction * Speed;
            }
            else
                Speed = 0;
               // }
    
        }
    }
}
