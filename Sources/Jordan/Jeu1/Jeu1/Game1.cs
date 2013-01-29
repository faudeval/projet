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

namespace Jeu1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
         Vaisseau vaisseau;
         Laser laser;
         Laser laser2;
         Laser laser3;
         Laser laser4;
         Life vie;
         double time;
         double tpsScore;
         SpriteFont Scored;
         Texture2D background;
         Rectangle fenetre;
         bool god = false;
         public bool draw = false;
         bool endGame = false;
         double tpsInv = 0;

         int score;
        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        #region Initialisation
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Global.largeurFenetre = Window.ClientBounds.Width;
            Global.hauteurFenetre = Window.ClientBounds.Height;
            laser = new Laser();
            laser2 = new Laser();
            laser3 = new Laser();
            laser4 = new Laser();
            vie = new Life();
            Global.pad = GamePad.GetState(PlayerIndex.One);

            vaisseau = new Vaisseau();
           // vaisseau.Initialize();
            base.Initialize();
             }
        #endregion


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        #region Chargement contenu, déchargement
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("back");
            vaisseau.LoadContent(Content,"vaisseau");
            laser.LoadContent(Content,"laser1");
            laser2.LoadContent(Content, "laser1");
            laser3.LoadContent(Content, "laser1");
            laser4.LoadContent(Content, "laser1");
            vie.LoadContent(Content, "heart");
            Scored = Content.Load<SpriteFont>("Score");
            fenetre = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            // TODO: use this.Content to load your game content here
        }
        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
#endregion

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        #region Update & Draw
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
             Global.clavier = Keyboard.GetState();
             if (Global.clavier.IsKeyDown(Keys.Escape))
                this.Exit();
            if (!endGame)
            {
                time += gameTime.ElapsedGameTime.TotalSeconds;
                placementLasers(laser, laser2);
                laser.Update(gameTime);
                laser2.Update(gameTime);

                placementLasers(laser3, laser4);
                // Délai entre les différents Laser

                if (time >= 1.3)
                {
                    laser3.Update(gameTime);
                    laser4.Update(gameTime);
                }
                vaisseau.Update(gameTime);
                vaisseauTouche();
                Invincibilite(gameTime);
                Score(gameTime);
                base.Update(gameTime);
            }
        }
           
               

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //Appel à la méthode Draw des composants(vaisseau et lasers)
            base.Draw(gameTime);
           
                spriteBatch.Begin();
                spriteBatch.Draw(background, fenetre, Color.White);
                if (!endGame)
                {
                    vaisseau.Draw(spriteBatch, gameTime);
                    laser.Draw(spriteBatch, gameTime);
                    laser2.Draw(spriteBatch, gameTime);
                    laser3.Draw(spriteBatch, gameTime);
                    laser4.Draw(spriteBatch, gameTime);
                    vie.Draw(spriteBatch, gameTime);
                    spriteBatch.DrawString(Scored, "Score : " + score.ToString(), new Vector2(550, 0), Color.Wheat);
                }
                else 
                {
                    if (!draw)
                    {
                        spriteBatch.DrawString(Scored, "GAME OVER ", new Vector2(Global.largeurFenetre / 2 - 200, Global.hauteurFenetre / 2), Color.White);
                        spriteBatch.DrawString(Scored, "Press SPACEBAR to continue ... ", new Vector2(200, Global.hauteurFenetre / 2 + 200), Color.White);
                    }
                    if (Global.clavier.IsKeyDown(Keys.Space))
                        draw = true;
                    if(draw)
                        spriteBatch.DrawString(Scored, "Votre score est de : " + score.ToString(), new Vector2(200, Global.hauteurFenetre / 2 - 5), Color.White);
                }
                graphics.GraphicsDevice.Clear(Color.DodgerBlue);
                spriteBatch.End();
            
        }

        #endregion

        #region Méthodes pour les instances

        private void placementLasers(Laser premierLaser, Laser secondLaser)
        {
            Random r = new Random();
            int nbr = r.Next(Global.largeurFenetre - vaisseau.Texture.Width*2)+vaisseau.Texture.Width*2;
            
            if (premierLaser.first)
            {
                premierLaser.Position.X -= nbr;
                premierLaser.first = false;
            }

            if (premierLaser.Position.Y >= Global.hauteurFenetre)
            {
                    premierLaser.Position.X = 0;
                    premierLaser.Position.X -= nbr;
            }
            secondLaser.Position.X= premierLaser.Position.X+premierLaser.Texture.Width+(int)((vaisseau.Texture.Width)*2.7);
        }

        public void vaisseauTouche()
        {
            // On test si les hitbox se touchent
            if (vaisseau.Rectangle.Intersects(laser.Rectangle)
                || vaisseau.Rectangle.Intersects(laser2.Rectangle)
                || vaisseau.Rectangle.Intersects(laser3.Rectangle)
                || vaisseau.Rectangle.Intersects(laser4.Rectangle))
            {
                /* SI l'invicibilité temporaire n'est pas activée:
                 * On retire une vie
                 * On active l'invincibilité temporaire
                 * Si le gamePad est connectée alors on met en route une vibration
                 * On indique à la classe vaisseau qu'il a été touché( changement de sprite)
                 * Sinon on annule cette dernière
                     */
                if (god == false && vie.nbVies !=0)
                {
                    vie.nbVies -= 1;
                    tpsScore = -3;
                    god = true;
                    if(Global.pad.IsConnected == true)
                        GamePad.SetVibration(PlayerIndex.One, 1.0f,1.0f);
                    vaisseau.vaisseauHit = true;
                }
            }
            else
            {
                if (vie.nbVies == 0)
                    endGame = true;
                vaisseau.vaisseauHit = false;
                GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            }
            
        }

        public void Invincibilite(GameTime gametime)
        {
            /*Si invincibilité temporaire activée:
                 * On fixe un temps pour la désactivée
                */
            if (god == true)
            {
                tpsInv += gametime.ElapsedGameTime.TotalSeconds;
                if (tpsInv > 0.50)
                {
                    god = false;
                    tpsInv = 0;
                }
            }
        }

        public void Score(GameTime gametime){
            tpsScore += gametime.ElapsedGameTime.TotalSeconds;
            if (tpsScore >= 1)
            {
                score += 1; 
                tpsScore = 0;
            }
            
        }

        #endregion
    }
}