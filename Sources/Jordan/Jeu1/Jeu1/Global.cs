using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Jeu1
{
    class Global
    {
        public static KeyboardState clavier { get; set; }
        public static GamePadState pad { get; set; }
        public static int largeurFenetre { get; set; }
        public static int hauteurFenetre { get; set; }
    }
}