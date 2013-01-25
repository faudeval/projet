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
    class Brique : Sprite
    {

        public Boolean isAlive;

        public Boolean IsInList
        {
            get;
            set;
        }

        public int type;

        public int vie;

        private Texture2D briqueBrisée;

        public int RefBonus
        {
            get;
            private set;
        }


        public Brique()
        {
            isAlive = true;
            IsInList = false;
            vie = 2;
            this.type = Globals.Random.Next(1, 6);
            switch (this.type)
            {
                case 1:
                    color = MyColors.color1;
                    break;
                case 2:
                    color = MyColors.bleuBalle;
                    break;
                case 3:
                    color = MyColors.color2;
                    break;
                case 4:
                    color = MyColors.color3;
                    break;
                case 5:
                    color = MyColors.color4;
                    break;
            }
            if (type > 3)
                RefBonus = Globals.Random.Next(2);
        }

        private int x;
        private int y;

        // Initialisation de la raquette
        public override void Initialize()
        {
            base.Initialize();
        }

        public void LoadContent(ContentManager content, string assetName, List<Brique> briques)
        {

            briqueBrisée = content.Load<Texture2D>("brokenBrick");
            base.LoadContent(content, assetName);
            Position = new Vector2(Globals.Random.Next(150, 651-Texture.Width), Globals.Random.Next(600 - Texture.Height));
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height);
            while (!VerifPlacement(briques))
            {
                x = Globals.Random.Next(150, 651);
                y = Globals.Random.Next(600 - Texture.Height);
                Position = new Vector2(x, y);
                Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (vie == 1)
                Texture = briqueBrisée;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isAlive)
                base.Draw(spriteBatch, gameTime);
            
        }

        public void VerifCollision(Balle balle)
        {
            if (isAlive && Rectangle.Intersects(balle.Rectangle))
            {
                vie--;
                if((balle.Rectangle.Y - balle.Rectangle.Height <= Rectangle.Y || balle.Rectangle.Y >= Rectangle.Y + Rectangle.Height-3)
                    && (balle.Rectangle.X >= Rectangle.X && balle.Rectangle.X <= Rectangle.X+Rectangle.Width))
                    balle.InverserY();
                else
                    balle.InverserX();
                if (vie == 0)
                    isAlive = false;
 
            }
        }

        public bool VerifPlacement(List<Brique> briques)
        {
            bool res = true;
            foreach(Brique brique in briques){
                if (Rectangle != brique.Rectangle && Rectangle.Intersects(brique.Rectangle))
                    res = false;
            }
            return res;    
        }
    }
}
