using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DamesGamesV3
{
    class Joueur
    {
        // Nombre de points d'un joueur
        public int point
        {
            get;
            set;
        }

        // Nombre de jetons restants à un joueur 
        public int NbJetonsRestants
        {
            get;
            set;
        }

        // Nombre de dames possédées par un joueur
        public int NbDames
        {
            get;
            set;
        }

        // Nombre de déplacements effectués par un joueur
        // Permettra de créer des 'high score' 
        // Avec le moins de coups possibles pour gagner
        public int NbCoupJoue
        {
            get;
            set;
        }

        // Le joueur qui possède un nombre de points,
        // un nombre de jetons
        // un nombre de dames
        // un nombre de déplacements effectués
        public Joueur(int p, int j)
        {
            point = p;
            NbJetonsRestants = j;
            NbDames = 0;
            NbCoupJoue = 0;
        }

        // Méthode renvoyant true si le joueur a perdu, false sinon.
        // La condition portant sur le nombre de jetons restants.
        public Boolean TuPerdsOuBien()
        {
            return (this.NbJetonsRestants == 0);
        }
    }
}
