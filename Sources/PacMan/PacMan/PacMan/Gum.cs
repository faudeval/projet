using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan
{
    class Gum
    {
        public bool Alive { get; private set; }
        public bool Super { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 MapPosition { get { return new Vector2((int)(Position.X / Level.TILE_WIDTH), (int)(Position.Y / Level.TILE_HEIGHT)); } }
        public Gum(Vector2 position, bool isSuper)
        {
            Position = position;
            this.Alive = true;
            this.Super = isSuper;
        }

        public int Activate()
        {
            Alive = false;
            if (Super)
                return 50;
            else
                return 10;
        }
    }
}
