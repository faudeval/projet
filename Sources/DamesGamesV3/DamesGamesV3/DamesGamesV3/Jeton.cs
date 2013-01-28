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
    public class Jeton
    {
        private Game1 game;

        // Créations des joueurs
        Joueur J1 = new Joueur(0, 20);
        Joueur J2 = new Joueur(0, 20);

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
        public Jeton(String coul, int posX, int posY, int n, Game1 game)
        {
            couleur = coul;
            position.X = posX;
            position.Y = posY;
            rang = 1;
            num = n;
            this.game = game;
        }
        
        // Methode permettant de savoir si un jeton est présent.
        // Renvoie le numéro du jeton selectionné,
        // -1 si il n'y a pas de jeton
        public int IsJetonPresent(int X, int Y)
        {
            int numero = -1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (game.Grille[i, j] != null)
                    {
                        if (X >= game.Grille[i, j].position.X
                            && Y >= game.Grille[i, j].position.Y
                            && X <= (game.Grille[i, j].position.X + game.JBlancWidth)
                            && Y <= (game.Grille[i, j].position.Y + game.JBlancHeight))
                            numero = game.Grille[i, j].num;
                    }
                }
            }
            return numero;
        }

        // Renvoie la couleur du jeton séletionné,
        // Prend le numéro du jeton en parametre
        public string CoulJeton(int n)
        {
            string c = null;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (game.Grille[i, j] != null)
                    {
                        if (game.Grille[i, j].num == n)
                            c = game.Grille[i, j].couleur;
                    }

                }
            }

            return c;
        }

        // Renvoie la postion en X du côté supérieure gauche d'une case
        // En fonction de la case cliquée
        public int RenvoiePosX(int PosXActu) 
        {
            int NewPosX = 0;
            for (int i = 0; i < 9; i++)
            {
                if (PosXActu >= i * game.JBlancWidth && PosXActu <= (i+1) * game.JBlancWidth)
                    NewPosX = i * game.JBlancWidth;
            }
            return NewPosX;
        }

        // Renvoie la postion en Y du côté supérieure gauche d'une case
        // En fonction de la case cliquée
        public int RenvoiePosY(int PosYActu)
        {
            int NewPosY = 0;

            for (int i = 0; i < 9; i++)
            {
                if (PosYActu >= i * game.JBlancHeight && PosYActu <= (i+1) * game.JBlancHeight)
                    NewPosY = i * game.JBlancHeight;
            }
            
            return NewPosY;
        }
    }
}
