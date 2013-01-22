using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DamesGamesV3
{
    public class Jeton : Game1
    {
        // Couleur du jeton
        public string couleur
        {
            get;
            set;
        }

        // rang du jeton, 2 si reine, 1 sinon
        public int rang
        {
            get;
            set;
        }

        public int num
        {
            get;
            set;
        }

        public Vector2 position = new Vector2(0, 0);

        // Constructeur d'un jeton
        public Jeton(String coul, int posX, int posY, int n)
        {
            couleur = coul;
            position.X = posX;
            position.Y = posY;
            rang = 1;
            num = n;
        }
        
        public int IsJetonPresent(int X, int Y)
        {
            int numero = -1;
            test += "1";

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Grille[i, j] != null)
                    {
                        test += "a";

                        if (X >= Grille[i, j].position.X
                            && Y >= Grille[i, j].position.Y
                            && X <= (Grille[i, j].position.X + JBlancWidth)
                            && Y <= (Grille[i, j].position.Y + JBlancHeight))
                                numero = Grille[i, j].num;
                    }
                }
            }

            return numero;
        }

        public string CoulJeton(int n)
        {
            string c = null;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (Grille[i, j] != null)
                    {
                        if (Grille[i, j].num == n)
                            c = Grille[i,j].couleur;
                    }

                }
            }

            return c;
        }
    }
}
