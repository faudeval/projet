using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DamesGamesV3
{
    class Joueur
    {
        public int point
        {
            get;
            set;
        }

        public int NbJetonsRestants
        {
            get;
            set;
        }

        public int NbDames
        {
            get;
            set;
        }

        public Joueur(int p, int j)
        {
            point = p;
            NbJetonsRestants = j;
            NbDames = 0;
        }

        public Boolean TuPerdsOuBien(Joueur j)
        {
            return (j.NbJetonsRestants == 0);
        }
    }
}
