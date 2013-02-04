using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;

namespace PacMan
{
    class Ghost : MobileSprite
    {
        Texture2D vulnerable;
        Texture2D usual;
        Vector2[] directions = new Vector2[4]{Vector2.UnitX, Vector2.UnitY, -Vector2.UnitX, -Vector2.UnitY};
        Random rand;

        public Ghost(Texture2D[] textures, Vector2 position, Level level)
            : base(textures[0], position, level)
        {
            this.nextDirection = -Vector2.UnitY;
            this.velocity = 100;
            this.usual = textures[0];
            this.vulnerable = textures[1];
            this.rand = new Random(unchecked((int)DateTime.Now.Ticks));
            Thread.Sleep(1); // Pour s'assurer que le rand soit aléatoire
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.rotation = 0;
            if (this.isBlocked)
            {
                int backwards = Array.IndexOf(directions, -direction);
                bool[] availableDirections = LookAround();
                if (backwards >= 0)
                    availableDirections[backwards] = false;
                if (Array.IndexOf(availableDirections, true) >= 0) // Si il y a au moins une direction possible
                {
                    do
                        backwards = rand.Next(4);
                    while (!availableDirections[backwards]);
                }
                nextDirection = directions[backwards];
            }
            else if (nextDirection == direction)
            {
                int initialDirection = Array.IndexOf(directions, direction);
                int backwards = Array.IndexOf(directions, -direction);
                bool[] availableDirections = new bool[4] { true, true, true, true };
                if (initialDirection >= 0)
                {
                    availableDirections[initialDirection] = false;
                    availableDirections[backwards] = false;
                }
                do
                    initialDirection = rand.Next(4);
                while (!availableDirections[initialDirection]);
                nextDirection = directions[initialDirection];
            }
        }

        private bool[] LookAround()
        {
            bool[] res = new bool[4];
            for(int i = 0; i < 4 ; i ++)
            {
                Vector2 nextPosition = this.position + this.directions[i];
                res[i] = (!this.level.IsOut(nextPosition, false));
            }
            return res;
        }
    }
}
