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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private bool isPaused;
        private Balle balle;
        private Raquette j1;
        private Raquette j2;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Window.Title = "Pong";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Globals.largeurFenetre = Window.ClientBounds.Width;
            Globals.hauteurFenetre = Window.ClientBounds.Height;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.police = Content.Load<SpriteFont>("SpriteFont1");
            balle.LoadContent(Content, "balle");
            j1.LoadContent(Content, "raquette");
            j2.LoadContent(Content, "raquette");
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
    }
}
