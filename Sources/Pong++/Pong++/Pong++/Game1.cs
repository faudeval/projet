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

namespace Pong__
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pong__ : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Balle b1;
        private Balle b2;
        private Raquette j1;
        private Raquette j2;
        private List<Brique> briques;
        private int nbBriques=20;
        private int nbRespawn = 2;
        private Texture2D wall;
        private String brick = "brick";
        private String brickbroken = "brokenBrick";
        private String ball = "ball";
        private String bat1 = "bat";
        private String bat2 = "bat2";

        public Pong__()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Window.Title = "Pong";
            this.Window.AllowUserResizing = false;
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            //this.graphics.SynchronizeWithVerticalRetrace = false;
            //this.IsFixedTimeStep = false;
            Components.Add(new CompteurFPS(this));
            //this.graphics.IsFullScreen = true;
            this.graphics.ApplyChanges();
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
            Globals.IsAgainstIA = false;
            //Construction des sprites
            b1 = new Balle(1);
            b2 = new Balle(2);
            j1 = new Raquette(1);
            j2 = new Raquette(2);
            briques = new List<Brique>();
            for (int i = 0; i < nbBriques; i++)
            {
                briques.Add(new Brique());
            }
            //Initialisation des sprites
            b1.Initialize();
            b2.Initialize();
            j1.Initialize();
            j2.Initialize();
            foreach (Brique brique in briques)
                brique.Initialize();

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
            wall = Content.Load<Texture2D>("wall");
            b1.LoadContent(Content, ball);
            b2.LoadContent(Content, ball);
            j1.LoadContent(Content, bat1);
            j2.LoadContent(Content, bat2);
            foreach (Brique brique in briques)
            {
                brique.LoadContent(Content, brick, briques);
            }
            b1.Placement(j1);
            b2.Placement(j2);
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
            if(Globals.keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();
            //VerifFenetre();
            
            b1.Update(gameTime, j1, j2);
            b2.Update(gameTime, j1, j2);
            j1.Update(gameTime, b1,b2);
            j2.Update(gameTime, b1, b2);
            
            foreach (Brique brique in briques)
            {
                brique.VerifCollision(b1);
                brique.VerifCollision(b2);
                brique.Update(gameTime);
            }

            PointMarqué();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Honeydew);

            spriteBatch.Begin();
              spriteBatch.Draw(wall, Vector2.Zero, Color.White);
                b1.Draw(spriteBatch, gameTime);
                b2.Draw(spriteBatch, gameTime);
                j1.Draw(spriteBatch, gameTime);
                j2.Draw(spriteBatch, gameTime);
                foreach (Brique brique in briques)
                {
                    brique.Draw(spriteBatch, gameTime);
                }
                spriteBatch.DrawString(Globals.police, j1.NbPoints.ToString() + " | " + j2.NbPoints.ToString(), Vector2.UnitX, MyColors.bleuBalle);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        //Vérifie si un point a été marqué et modifie la direction de la balle pour la manche suivante
        public void PointMarqué()
        {
            Random rnd = new Random();
            if (b1.Position.X >= Globals.largeurFenetre)
            {
                j1.NbPoints += 1;
                b1.Speed = 0;
                b1.EnMouvement = false;
                b1.Placement(j2);
                b1.LastTouched = 2;
                b1.Direction = new Vector2(-rnd.Next(1, 4), rnd.Next(2) == 0 ? -1 : 1);
                MoreBricks();
            }
            else if (b1.Position.X < 0 - b1.Texture.Width)
            {
                j2.NbPoints += 1;
                b1.Speed = 0;
                b1.EnMouvement = false;
                b1.Placement(j1);
                b1.LastTouched = 1;
                b1.Direction = new Vector2(rnd.Next(1, 4), rnd.Next(2) == 0 ? -1 : 1);
                MoreBricks();
            }

            if (b2.Position.X >= Globals.largeurFenetre)
            {
                j1.NbPoints += 1;
                b2.Speed = 0;
                b2.EnMouvement = false;
                b2.Placement(j2);
                b2.LastTouched = 2;
                b2.Direction = new Vector2(-Globals.Random.Next(1, 4), Globals.Random.Next(2) == 0 ? -1 : 1);
                MoreBricks();
            }
            else if (b2.Position.X < 0 - b2.Texture.Width)
            {
                j2.NbPoints += 1;
                b2.Speed = 0;
                b2.EnMouvement = false;
                b2.Placement(j1);
                b2.LastTouched = 1;
                b2.Direction = new Vector2(Globals.Random.Next(1, 4), Globals.Random.Next(2) == 0 ? -1 : 1);
                MoreBricks();
            }
        }

        // Vérifie si la taille de la fenêtre a été modifiée et replace les raquettes en conséquence
        public void VerifFenetre()
        {
            //Si la taille de la fenetre a été modifiée
            if (Globals.hauteurFenetre != Window.ClientBounds.Height || Globals.largeurFenetre != Window.ClientBounds.Width)
            {
                //On modifie la valeur des variables globales
                Globals.hauteurFenetre = Window.ClientBounds.Height;
                Globals.largeurFenetre = Window.ClientBounds.Width;
                //On replace les raquettes
                j1.Placement();
                j2.Placement();
            }
        }

        public void MoreBricks()
        {
            for (int i = 0; i < nbRespawn; i++)
            {
                int j = 0;
                Brique brique = new Brique();
                brique.Initialize();
                brique.LoadContent(Content, "brick", briques);
                while(j<briques.Count && !brique.IsInList)
                {
                    if(!briques[j].isAlive)
                    {
                        briques[j] = brique;
                        brique.IsInList = true;
                    }
                    j++;
                }
                if (!brique.IsInList)
                {
                    briques.Add(brique);
                    brique.IsInList = true;
                }
            }
        }
    }
}
