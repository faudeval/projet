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

namespace Global
{
    class Balle : Sprite
    {
        public override void Initialize()
        {
            base.Initialize();
            //Direction de départ de la balle aléatoire
            Direction = new Vector2(Globals.Random.Next(2) == 0 ? Globals.Random.Next(1, 4) : -Globals.Random.Next(1, 4), Globals.Random.Next(2) == 0 ? -1 : 1);
            Speed = 0.2f;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            //Positionnement e la balle au centre de la fenêtre
            Position = new Vector2((Globals.largeurFenetre / 2) - Texture.Width, (Globals.hauteurFenetre / 2) - Texture.Height);
            
        }

        public void Update(GameTime gameTime, Rectangle rj1, Rectangle rj2)
        {
            base.Update(gameTime);
            // Si la balle tend à sortir de l'écran par le haut ou le bas
            if (((Position.Y + Direction.Y) <= 0) || ((Position.Y + Direction.Y) + Texture.Height >= Globals.hauteurFenetre))
                // On inverse l'ordonnée du vecteur Direction
                Direction = new Vector2(Direction.X, -Direction.Y);
            //Sinon si la balle touche une raquette
            else if (Rectangle.Intersects(rj1) || Rectangle.Intersects(rj2))
            {
                //On inverse l'abcisse du vecteur Direction
                Direction = new Vector2(-Direction.X, Direction.Y);
                //On augmente la vitesse
                Speed += 0.05f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
             base.Draw(spriteBatch, gameTime);
        }
    }
}
