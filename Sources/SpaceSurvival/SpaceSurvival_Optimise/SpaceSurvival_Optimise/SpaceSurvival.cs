#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion
/* TODO :
 * Gestion de la vie (fin de jeu)
 * Gestion des menus
 * Gestion des niveaux
 * Gestion de la fragmentation
 * Création de modes de jeu
 * Création de skins de vaisseau
 * Gestion du mode d'affichage (plein écran ?)
 * Implémentation du online ?
 * 
 * Projet MONO
*/

namespace SpaceSurvival_Optimise
{
    public class SpaceSurvival : Microsoft.Xna.Framework.Game
    {
        #region Fields
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Game game;
        public static KeyboardState oldKeyboardState;
        public static KeyboardState keyboardState = Keyboard.GetState();
        public static SpriteFont font;
        public static List<Obj> components;
        public static List<Obj> waitingComponents;
        public static Color[] teamColor;

        int level;

        TimeSpan _asteroidsTimer = TimeSpan.Zero;
        #endregion

        #region Initialize
        public SpaceSurvival()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = this;
        }

        protected override void Initialize()
        {
#if DEBUG
            Components.Add(new CompteurFPS(this));
#endif
            components = new List<Obj>();
            waitingComponents = new List<Obj>();
            level = 1;
            teamColor = new Color[] { Color.Blue, Color.Red, Color.Green, Color.Yellow, Color.Pink, Color.Orange, Color.Violet, Color.Silver };

            Keys[] controls = new Keys[5];
            controls[(int)Controls.Up] = Keys.Up;
            controls[(int)Controls.Down] = Keys.Down;
            controls[(int)Controls.Left] = Keys.Left;
            controls[(int)Controls.Right] = Keys.Right;
            controls[(int)Controls.Fire] = Keys.Space;
            Ship s = new Ship(4, "Fleches-Espace", 0, controls, new Vector2(Window.ClientBounds.Width / 2 - 150, Window.ClientBounds.Height / 2));
            AddComponent(s);
            Components.Add(new ShowPlayer(this, s, Vector2.Zero));

            Keys[] ctrl = new Keys[5];
            ctrl[(int)Controls.Up] = Keys.Z;
            ctrl[(int)Controls.Down] = Keys.S;
            ctrl[(int)Controls.Left] = Keys.Q;
            ctrl[(int)Controls.Right] = Keys.D;
            ctrl[(int)Controls.Fire] = Keys.A;
            Ship s2 = new Ship(4, "ZQSDA", 0, ctrl, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));
            AddComponent(s2);
            Components.Add(new ShowPlayer(this, s2, Vector2.UnitY * 20));

            Keys[] ccc = new Keys[5];
            ccc[(int)Controls.Up] = Keys.NumPad8;
            ccc[(int)Controls.Down] = Keys.NumPad2;
            ccc[(int)Controls.Left] = Keys.NumPad4;
            ccc[(int)Controls.Right] = Keys.NumPad6;
            ccc[(int)Controls.Fire] = Keys.NumPad5;
            Ship s3 = new Ship(4, "NumPad", 0, ccc, new Vector2(Window.ClientBounds.Width / 2 + 150, Window.ClientBounds.Height / 2));
            AddComponent(s3);
            Components.Add(new ShowPlayer(this, s3, Vector2.UnitY * 40));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
#if DEBUG
            font = Content.Load<SpriteFont>("police");
#endif
        }

        protected override void UnloadContent()
        {
            // TODO ?
        }
        #endregion

        #region Update & Draw
        protected override void Update(GameTime gameTime)
        {
            // Mise à jour des KeyboardState
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            // On quitte si on appuie sur Echap
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            // Chaque seconde, on ajoute une asteroïde
            if ((gameTime.TotalGameTime - _asteroidsTimer).Milliseconds > 750)
            {
                _asteroidsTimer = gameTime.TotalGameTime;
                AddComponent(new Asteroid(level));
            }

            List<Obj> done = new List<Obj>();
            foreach (Obj o in components) // Test des collisions
            {
                if(o.IsActive)
                foreach (Obj c in components)
                {
                    if (c.IsActive && c != o && c.bounds.Intersects(o.bounds) && c.Team != o.Team) // Les deux rectangles s'intersectionnent. On vérifie donc avec les hitbox
                    {
                        int i = 0;
                        bool b = true;
                        do
                        {
                            if (o.HitBox.Contains(c.HitBox[i] + c.IntPosition - o.IntPosition))
                            {
                                if (!done.Contains(c))
                                {
                                    c.Hit();
                                    done.Add(c);
                                }
                                if (!done.Contains(o))
                                {
                                    o.Hit();
                                    done.Add(o);
                                }
                                b = false;
#if DEBUG
                                Console.WriteLine("{0} ({2}) touche {1} ({3})", c.GetType().Name, o.GetType().Name, components.IndexOf(c), components.IndexOf(o));
#endif
                            }
                            i++;
                        }
                        while (b && i < c.HitBox.Length);
                    }
                }
            }

            // On Update tous les composants
            foreach (Obj o in components)
                o.Update(gameTime);
            if (waitingComponents.Count > 0)
            {
                foreach (Obj o in waitingComponents)
                    components.Add(o);
                waitingComponents.Clear();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // On dessine le fond noir
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // On affiche les asteroides actives
            foreach (Obj o in components)
                o.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Methods
        public static void AddComponent(Obj o)
        {
            int i = 0;
            bool filled = false;
            while (i < components.Count && !filled)
            {
                if (!components[i].IsActive)
                {
                    components[i] = o;
                    filled = true;
                }
                i++;
            }
            if (!filled)
                components.Add(o);
        }
        #endregion
    }
}
