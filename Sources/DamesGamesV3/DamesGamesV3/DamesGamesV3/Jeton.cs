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

        // Couleur du jeton
        public string couleur
        {
            get;
            set;
        }

        // Rang du jeton, 2 si reine, 1 sinon
        public int rang
        {
            get;
            set;
        }

        // Numéro permettant de différencier les jetons
        public int num
        {
            get;
            set;
        }

        // Position des jetons
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

            // Méthode qui parcours la grille où se trouvent les jetons
            // Dès que la position (X, Y) ciblée est trouvée
            // On renvoie le numéro du jeton correspondant
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

        // Renvoie la postion en X de l'angle supérieure gauche d'une case
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

        // Renvoie la postion en Y de l'angle supérieure gauche d'une case
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

        // Méthode qui permet le déplacement d'un jeton
        // Renvoie true qi le jeton a été déplacé, false sinon
        public Boolean DeplaceJeton(int X, int Y, int numJS) 
        {
            Boolean res = false;
            
            // variable de 'sécurité', 
            // permet d'être sur que l'on déplace 
            // et surtout si l'on supprime un unique jeton
            int hey = 0;

            // Jeton 'bidon' permettant l'appel des méthodes de la classe Jeton
            Jeton j = new Jeton("ROUGE", 0, 0, 99, game);

            // Valeurs des positions X et Y où le joueur souhaite déplacer son jeton
            int x = j.RenvoiePosX(X) / game.JBlancWidth;
            int y = j.RenvoiePosY(Y) / game.JBlancHeight;

            // On supprime le jeton 'NumJetonSelect'
            // On le redessine dans la case sélectionnée
            // On va donc mettre à jour la position du jeton sélectionné,
            // puis on va le redessiné, sans oublier de supprimer 'son ancienne image'
            for (int i = 0; i < 10; i++)
            {
                for (int u = 0; u < 10; u++)
                {
                    if (game.Grille[i, u] != null && hey == 0)
                    {
                        if (game.Grille[i, u].num == numJS && game.Grille[i, u].couleur == j.CoulJeton(numJS) && hey == 0)
                        {
                            for (int k = 0; k < 10; k++)
                            {
                                for (int l = 0; l < 10; l++)
                                {                                  
                                   if (k == x && l == y && hey == 0)
                                    {
                                        game.Grille[k, l] = new Jeton(game.Grille[i, u].couleur, j.RenvoiePosX(X), j.RenvoiePosY(Y), numJS, game);
                                        game.Grille[i, u] = null;
                                        hey++;

                                        res = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (hey != 0)
                    i = 10;
            }
            return res;
        }
    }
}
