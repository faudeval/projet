using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pong__
{
    //Classe contenant les variables globales
    class Globals
    {
        public static int largeurFenetre { get; set; }

        public static int hauteurFenetre { get; set; }

        public static SpriteFont police { get; set; }

        public static KeyboardState keyboardState { get; set; }

        public static Random Random { get; set; }

        public static bool IsAgainstIA { get; set; }
    }
}
