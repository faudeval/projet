using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan
{
    class Pacman : Sprite
    {
        // Etats du clavier
        private KeyboardState keyboardState;

        public Pacman(Texture2D texture, Level level)
            : base(texture, level.StartingPosition, level)
        { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Récupération de l'état du clavier
            this.keyboardState = Keyboard.GetState();

            bool UP = this.keyboardState.IsKeyDown(Keys.Up);
            bool DOWN = this.keyboardState.IsKeyDown(Keys.Down);
            bool LEFT = this.keyboardState.IsKeyDown(Keys.Left);
            bool RIGHT = this.keyboardState.IsKeyDown(Keys.Right);

            if (UP)
            {
                Direction = -Vector2.UnitY;
                this.velocity = 100f;
                this.rotation = -(float)Math.PI / 2;
            }
            else if (DOWN)
            {
                Direction = Vector2.UnitY;
                this.velocity = 100f;
                this.rotation = (float)Math.PI / 2;
            }
            else if (LEFT)
            {
                Direction = -Vector2.UnitX;
                this.velocity = 100f;
                this.rotation = (float)Math.PI;
            }
            else if (RIGHT)
            {
                Direction = Vector2.UnitX;
                this.velocity = 100f;
                this.rotation = 0;
            }
        }
    }
}
