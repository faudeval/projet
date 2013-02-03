using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan
{
    class Pacman : MobileSprite
    {
        // Etats du clavier
        private KeyboardState keyboardState;

        public Pacman(Texture2D texture, Level level)
            : base(texture, level.StartingPosition, level)
        { velocity = 100; }

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
                NextDirection = -Vector2.UnitY;
            else if (DOWN)
                NextDirection = Vector2.UnitY;
            else if (LEFT)
                NextDirection = -Vector2.UnitX;
            else if (RIGHT)
                NextDirection = Vector2.UnitX;
        }
    }
}
