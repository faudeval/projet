#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameStateManagement;
using SpaceSurvival;
#endregion

/* TODO
 * Transformer les DrawableGameComponents en vraies images (plus propres, ne fera pas tâche lors des transitions)
 * Gérer le menu
 * Envoi des asteroides sur un vaisseau (pas uniquement au milieu)
 * Gestion de la vie (fin de jeu)
 * Gestion des niveaux
 * Gestion de la fragmentation
 * Création de modes de jeu
 * Création de skins de vaisseau
 * Gestion du mode d'affichage (plein écran ?)
 * Implémentation du online ?
 * 
 * Projet MONO
*/

namespace GameStateManagementSample
{
    /// <summary>
    /// Cet écran gère la logique du jeu
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields
        public static SpriteBatch spriteBatch;
        public static Game game;
        public static KeyboardState oldKeyboardState;
        public static KeyboardState keyboardState = Keyboard.GetState();
        public static SpriteFont font;
        public static List<Obj> components;
        public static List<Obj> waitingComponents;
        public static Color[] teamColor;

        int level;
        List<DrawableGameComponent> dgc;

        TimeSpan _asteroidsTimer = TimeSpan.Zero;

        ContentManager content;

        float pauseAlpha;

        InputAction pauseAction;
        #endregion

        #region Initialization
        /// <summary>
        /// Constructeur.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);
        }

        /// <summary>
        /// Charge le contenu graphique pour le jeu
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                game = ScreenManager.Game;
                if (content == null)
                    content = new ContentManager(game.Services, "Content");

/*
    * Oui je mets ce qui devrait être dans le constructeur dans le Activate().
    * Et alors? Ca te pose un problème ??
    * Si tu me dis comment accéder à ce ptain de ScreenManager.Game dans le
    * constructeur sans qu'il soit null je veux bien le remettre dedans !
*/
                dgc = new List<DrawableGameComponent>();
#if DEBUG
                dgc.Add(new CompteurFPS(ScreenManager.Game));
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
                Ship s = new Ship(4, "Fleches-Espace", 0, controls, new Vector2(game.Window.ClientBounds.Width / 2 - 150, game.Window.ClientBounds.Height / 2));
                AddComponent(s);
                dgc.Add(new ShowPlayer(game, s, Vector2.Zero));

                Keys[] ctrl = new Keys[5];
                ctrl[(int)Controls.Up] = Keys.Z;
                ctrl[(int)Controls.Down] = Keys.S;
                ctrl[(int)Controls.Left] = Keys.Q;
                ctrl[(int)Controls.Right] = Keys.D;
                ctrl[(int)Controls.Fire] = Keys.A;
                Ship s2 = new Ship(4, "ZQSDA", 0, ctrl, new Vector2(game.Window.ClientBounds.Width / 2, game.Window.ClientBounds.Height / 2));
                AddComponent(s2);
                dgc.Add(new ShowPlayer(game, s2, Vector2.UnitY * 20));

                Keys[] ccc = new Keys[5];
                ccc[(int)Controls.Up] = Keys.NumPad8;
                ccc[(int)Controls.Down] = Keys.NumPad2;
                ccc[(int)Controls.Left] = Keys.NumPad4;
                ccc[(int)Controls.Right] = Keys.NumPad6;
                ccc[(int)Controls.Fire] = Keys.NumPad5;
                Ship s3 = new Ship(4, "NumPad", 0, ccc, new Vector2(game.Window.ClientBounds.Width / 2 + 150, game.Window.ClientBounds.Height / 2));
                AddComponent(s3);
                dgc.Add(new ShowPlayer(game, s3, Vector2.UnitY * 40));

                foreach(DrawableGameComponent d in dgc)
                    game.Components.Add(d);

/* "Vrai" LoadContent() */

                spriteBatch = new SpriteBatch(game.GraphicsDevice);
#if DEBUG
                font = game.Content.Load<SpriteFont>("police");
#endif

                // Une fois que le chargement est terminé, on utilise ResetElapsedTime pour dire au
                // mecanisme de timing du jeu qu'on vient de finir un état pouvant avoir un fonctionnement lent
                game.ResetElapsedTime();
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        /// <summary>
        /// Libère le contenu graphique utilisé par le jeu
        /// </summary>
        public override void Unload()
        {
            foreach (DrawableGameComponent d in dgc)
                game.Components.Remove(d);
            content.Unload();
        }
        #endregion

        #region Update and Draw
        /// <summary>
        /// Met l'état du jeu à jour. Vérifie GameScreen.IsActive pour savoir si le jeu
        /// n'est pas en pause et que l'application a le focus
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            //Apparait ou disparait graduellement selon que le jeu soit en pause ou pas
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Mise à jour des KeyboardState
                oldKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();

                // Chaque seconde, on ajoute une asteroïde
                if ((gameTime.TotalGameTime - _asteroidsTimer).Milliseconds > 750)
                {
                    _asteroidsTimer = gameTime.TotalGameTime;
                    AddComponent(new Asteroid(level));
                }

                List<Obj> done = new List<Obj>();
                foreach (Obj o in components) // Test des collisions
                {
                    if (o.IsActive)
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
            }
        }

        /// <summary>
        /// Gère l'entrée de l'utilisateur. Appelée seulement lorsque cet écran
        /// est actif (contrairement à Update)
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Répertorie les entrées pour le profil de joueur actif
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // Le jeu se met en pause si le joueur appuie sur le bouton pause, ou si
            // il déconnecte la manette active. Il faut donc garder une trace de si
            // une manette a déjà été branchée. On ne va pas mettre le jeu en pause si
            // il n'y a jamais eu de manette
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // TODO : modifier la gestion des ship pour que les commandes soient déclenchées ici (philosophie de développement)
            }
        }

        /// <summary>
        /// Dessine l'écran
        /// </summary>
        public override void Draw(GameTime gameTime)
        {

            // On dessine le fond noir
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);
            spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            // On affiche les asteroides actives
            foreach (Obj o in components)
                o.Draw(gameTime);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
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
