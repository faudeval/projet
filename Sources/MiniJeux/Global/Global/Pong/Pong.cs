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

/*
 * Changements par rapport à avant :
 * - Renommage du namespace
 * - Ajout d'une variable de type Jeu
 * - Changements dans le constructeur : le pong prend l'environnement de jeu du parent
 * - Initialize() : la taille de la fenêtre passe maintenant par le parent, donc la récupération de la taille a été modifiée
 * - LoadContent() : l'emplacement du contenu a changé pour faire du "rangement", donc les chemins ont été modifiés
 * - ajout des méthodes pour pouvoir accéder aux méthodes "protected" depuis le parent
*/

namespace Global
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Jeu parent;

        private bool isPaused;
        private Balle balle;
        private Raquette j1;
        private Raquette j2;

        public Pong(Jeu j)
        {
            this.parent = j;
            this.graphics = j.graphics;
            this.spriteBatch = j.spriteBatch;
            this.Content = j.Content;

            j.Window.Title = "Pong";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Globals.largeurFenetre = parent.Window.ClientBounds.Width;
            Globals.hauteurFenetre = parent.Window.ClientBounds.Height;
            Globals.Random = new Random();
            balle = new Balle();
            j1 = new Raquette(1);
            j2 = new Raquette(2);
            isPaused = true;
            //Initialisation des sprites
            balle.Initialize();
            j1.Initialize();
            j2.Initialize();

            //Initialisation du jeu
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Globals.police = Content.Load<SpriteFont>("Pong/SpriteFont1");
            balle.LoadContent(Content, "Pong/balle");
            j1.LoadContent(Content, "Pong/raquette");
            j2.LoadContent(Content, "Pong/raquette");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Globals.keyboardState = Keyboard.GetState();
            if (Globals.keyboardState.IsKeyDown(Keys.Escape)) /* Si on appuie sur Echap, le parent passe à l'état "Pause" */
            {
                parent.setState(GameState.Pause);
                parent.Window.Title = "Menu";
                return;
            }
            if (!isPaused)
            {
                balle.Update(gameTime, j1.Rectangle, j2.Rectangle);
                j1.Update(gameTime);
                j2.Update(gameTime);
                PointMarqué();
            }
            else if(Globals.keyboardState.IsKeyDown(Keys.Space))
                isPaused = false;

            base.Update(gameTime);
        }

        //Vérifie si un point a été marqué et modifie la direction de la balle pour la manche suivante
        public void PointMarqué()
        {
            if (balle.Position.X >= Globals.largeurFenetre)
            {
                j1.NbPoints += 1;
                balle.Initialize();
                balle.Position = new Vector2(Globals.largeurFenetre / 2 - balle.Texture.Width / 2, Globals.hauteurFenetre / 2 - balle.Texture.Height / 2);
                balle.Direction = new Vector2(-Globals.Random.Next(1, 4), Globals.Random.Next(2) == 0 ? -1 : 1);
                isPaused = true;
            }
            else if (balle.Position.X < 0  - balle.Texture.Width)
            {
                j2.NbPoints += 1;
                balle.Initialize();
                balle.Position = new Vector2(Globals.largeurFenetre / 2 - balle.Texture.Width / 2, Globals.hauteurFenetre / 2 - balle.Texture.Height / 2);
                balle.Direction = new Vector2(Globals.Random.Next(1, 4), Globals.Random.Next(2) == 0 ? -1 : 1);
                isPaused = true;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Honeydew);
            spriteBatch.Begin();
                balle.Draw(spriteBatch, gameTime);
                j1.Draw(spriteBatch, gameTime);
                j2.Draw(spriteBatch, gameTime);
                spriteBatch.DrawString(Globals.police, j1.NbPoints.ToString() + " | " + j2.NbPoints.ToString(), Vector2.UnitX, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        /* Méthodes pour passer à travers le "protected" */
        public void init(GameTime gt)
        {
            this.Initialize();
        }
        public void load()
        {
            this.LoadContent();
        }
        public void unload()
        {
            this.UnloadContent();
        }
        public void update(GameTime gt)
        {
            this.Update(gt);
        }
        public void draw(GameTime gt)
        {
            this.Draw(gt);
        }
    }
}
