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
    class Balle : Sprite
    {
        public int numero;

        public Boolean EnMouvement
        {
            get;
            set;
        }

        public int LastTouched
        {
            get;
            set;
        }

        public Balle(int numero)
        {
            this.numero = numero;
            LastTouched = numero == 1 ? 1 : 2;
            Speed = 0;
            EnMouvement = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            //Direction de départ de la balle
            Direction = new Vector2(numero == 1 ? Globals.Random.Next(1, 4) : -Globals.Random.Next(1, 4),
                                    Globals.Random.Next(2) == 0 ? Globals.Random.Next(1, 4) : -Globals.Random.Next(1, 4));
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);            
        }

        public void Update(GameTime gameTime, Raquette j1, Raquette j2)
        {
            base.Update(gameTime);
            Globals.keyboardState = Keyboard.GetState();
            if (!EnMouvement && LastTouched == 1)
            {
                if (Globals.keyboardState.IsKeyDown(Keys.Z))
                {
                    Direction = new Vector2(Direction.X, -1);
                    Demarrer();
                }
                    
                else if (Globals.keyboardState.IsKeyDown(Keys.S))
                {
                    Direction = new Vector2(Direction.X, 1);
                    Demarrer();
                }
                
            }
            if (!EnMouvement && LastTouched == 2)
            {
                if (Globals.keyboardState.IsKeyDown(Keys.Up))
                {
                    Direction = new Vector2(Direction.X, -1);
                    Demarrer();
                }
                else if (Globals.keyboardState.IsKeyDown(Keys.Down))
                {
                    Direction = new Vector2(Direction.X, 1);
                    Demarrer();
                }
            }
            j1.VerifCollision(this);
            j2.VerifCollision(this);
            RebondMur();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }


        //Positionnement de la balle au centre de la fenêtre
        public void Placement(Raquette raquette)
        {
            switch (raquette.Joueur)
            {
                case 1: Position = new Vector2(raquette.Position.X + raquette.Texture.Width, raquette.Position.Y + (raquette.Texture.Height)/2 - (Texture.Height)/2);
                    break;
                case 2: Position = new Vector2(raquette.Position.X - Texture.Width, raquette.Position.Y + (raquette.Texture.Height) / 2 - (Texture.Height)/2);
                    break;
            }
        }

        public void RebondMur()
        {
            // Si la balle tend à sortir de l'écran par le haut ou le bas
            if (((Position.Y + Direction.Y) <= 0) || ((Position.Y + Direction.Y) + Texture.Height >= Globals.hauteurFenetre))
                // On inverse l'ordonnée du vecteur Direction
                Direction = new Vector2(Direction.X, -Direction.Y);
        }

        public void Demarrer()
        {
            EnMouvement = true;
            Speed = 0.25f;
        }

        public void InverserX()
        {
            Direction = new Vector2(-Direction.X, Direction.Y);
        }

        public void InverserY()
        {
            Direction = new Vector2(Direction.X, -Direction.Y);
        }

    }
}
