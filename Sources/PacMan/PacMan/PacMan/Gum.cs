using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan
{
    class Gum : Sprite
    {
        public Gum(Texture2D texture, Vector2 position, Level level)
            : base(texture, position, level)
        { }
    }
}
