using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan
{
    class Pacman : Sprite
    {   
        // Etats du clavier
        private KeyboardState keyboardState;

        public Pacman(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

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
                this.rotation = -(float)Math.PI/2;
            }
            else if (DOWN)
            {
                Direction = Vector2.UnitY;
                this.velocity = 100f;
                this.rotation = (float)Math.PI/2;
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
            //else if (!UP || !DOWN || !LEFT || !RIGHT) { this.velocity = 0; }
        }

        /*public void CheckDecorCollision(Level level)
        {
            Console.WriteLine(line.ToString() + ", " + column.ToString());

            /// Problème avec les index
            // Si UP et prochaine case = 1
            if (Direction == -Vector2.UnitY && this.velocity > 0 && level.Map[line - 1, column]  == 1)
            {
                Console.WriteLine(level.Map[line + 1, column].ToString());
                this.velocity = 0;
            }
            // Si DOWN et prochaine case = 1
            if (Direction == Vector2.UnitY && this.velocity > 0 && level.Map[line + 1, column] == 1)
            {
                Console.WriteLine(level.Map[line + 1, column].ToString());
                this.velocity = 0;
            }
            // Si LEFT et prochaine case = 1
            if (Direction == -Vector2.UnitX && this.velocity > 0 && level.Map[line, column - 1] == 1)
            {
                Console.WriteLine(level.Map[line - 1, column].ToString());
                this.velocity = 0;
            }
            // Si RIGHT et prochaine case = 1
            if (Direction == Vector2.UnitX && this.velocity > 0 && level.Map[line, column + 1] == 1)
            {
                Console.WriteLine(level.Map[line + 1, column].ToString());
                this.velocity = 0;
            }
        }*/

        public void CheckDecorCollision(Level level)
        {
                if (level.CollisionTile((int)this.position.X , (int)this.position.Y ))
                {
                    
                }
        }
    }
}
