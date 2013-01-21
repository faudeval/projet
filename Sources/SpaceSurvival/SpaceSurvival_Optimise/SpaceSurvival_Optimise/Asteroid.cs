#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SpaceSurvival_Optimise
{
    class Asteroid:Obj
    {
        #region Fields
        private int _value;                             // valeur de l'asteroide
        private Random rand = new Random();             // Variable pour générer des nombres aléatoires

        public int Value                                // Généré dans le constructeur
        {
            get { return _value; }
            private set { _value = value; HP = value; Image = generateImage(value); Speed = (float)((float)rand.Next(1, 4) / (float)value); }
        }
        #endregion

        #region Initialize
        public Asteroid():base(new Object[] {1, Vector2.Zero} )
        { }
        public Asteroid(int level):base(new Object[] {level, Vector2.Zero} )
        { }
        public Asteroid(Vector2 position):base(new Object[] {1, position} )
        { }
        public Asteroid(float x, float y):base(new Object[] {1, new Vector2(x, y)} )
        { }
        public Asteroid(int level, Vector2 position):base(new Object[] {level, position} )
        { }
        public Asteroid(int level, float x, float y):base(new Object[] {level, new Vector2(x, y)} )
        { }

        protected override void  init(params object[] param)
        {
            Value = (int)param[0];
            Position = ((Vector2)param[1] == Vector2.Zero)?(generatePosition()):((Vector2)param[1]);
            Team = -1;
        }
        #endregion

        #region Methods
        // Génère une image d'asteroïde et la HitBox correspondante à partir d'une valeur
        Texture2D generateImage(int value)
        {
            Texture2D texture = new Texture2D(SpaceSurvival.graphics.GraphicsDevice, (value * 20), (value * 20), false, SurfaceFormat.Color);
            Color[] colorTable = new Color[value * value * 20 * 20];
            for (int i = 0; i < value * value * 20 * 20; i++)
            {
                colorTable[i] = Color.Transparent;
            }
            int effectiveSurface = rand.Next(value * value * 20 * 5, value * value * 20 * 20);

            HitBox = new Vector2[effectiveSurface];

            int j = rand.Next(colorTable.Length);
            for (int i = 0; i < effectiveSurface; i++)
            {
                HitBox[i] = new Vector2(j % (20 * value), j / (20 * value));
                colorTable[j] = Color.Gray;
                int k = rand.Next(4);
                switch (k)
                {
                    case 0: // On tente de placer le prochain pixel au dessus du précédent
                        if (j - value * 20 > 0)
                            j -= value * 20;
                        break;
                    case 1: // On tente de placer le prochain pixel à gauche du précédent
                        if (j - 1 > 0 && j % (value * 20) != 0) // Si j % (value*20) == 0, alors on est sur le bord gauche. Si on fait -1 on se retrouve sur le bord droit...
                            j -= 1;
                        break;
                    case 2: // On tente de placer le prochain pixel au dessous du précédent
                        if (j + value * 20 < colorTable.Length)
                            j += value * 20;
                        break;
                    case 3: // On tente de placer le prochain pixel à droite du précédent
                        if (j + 1 < colorTable.Length && (j + 1) % (value * 20) != 0) // Si j + 1 % (value*20) == 0, alors on est sur le bord droit. Si on fait +1 on se retrouve sur le bord gauche...
                            j += 1;
                        break;
                }
            }
            texture.SetData<Color>(colorTable);

            return texture;
        }

        // Génère une position aléatoire sur un bord du cadre ainsi qu'une direction 
        Vector2 generatePosition(int x = 0, int y = 0, int width = -1, int height = -1)
        {
            Vector2 position;
            if (width <= 0 || height <= 0)
            {
                width = SpaceSurvival.game.Window.ClientBounds.Width;
                height = SpaceSurvival.game.Window.ClientBounds.Height;
            }

            int side = rand.Next(4), _x = -1, _y = -1;
            switch (side)
            {
                case 0: // haut
                    _y = y;
                    break;
                case 1: // droite
                    _x = width + x - (Value * 20);
                    break;
                case 2: // bas
                    _y = height + y - (Value * 20);
                    break;
                case 3: // gauche
                    _x = y;
                    break;
            }
            if (_x == -1)
                _x = rand.Next(x, x + width - (Value * 20));
            else
                _y = rand.Next(y, y + height - (Value * 20));

            position = new Vector2(_x, _y);

            Direction = new Vector2((float)(x + width / 2 - _x), (float)(y + height / 2 - _y));

            return position;
        }
        #endregion
    }
}
