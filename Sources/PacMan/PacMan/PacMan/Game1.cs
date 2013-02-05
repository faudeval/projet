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

namespace PacMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<MobileSprite> mobileSprites;
        Level level;

        // Conteur de "points de vie" du pacman
        int LP = 3;

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
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mobileSprites = new List<MobileSprite>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Chargement de la MAP
            this.level = new Level();
            this.level.AddTexture(Content.Load<Texture2D>("mur")); //0
            this.level.AddTexture(Content.Load<Texture2D>("map")); //1
            this.level.AddTexture(Content.Load<Texture2D>("porte")); //2

            // Chargement du PACMAN
            this.mobileSprites.Add(new Pacman(Content.Load<Texture2D>("pacman"), level));
            this.mobileSprites.Add(new Ghost(new Texture2D[2] { Content.Load<Texture2D>("ghost1"), Content.Load<Texture2D>("fear") }, level.StartingPosition - Vector2.UnitY * 6 * Level.TILE_HEIGHT, level));
            this.mobileSprites.Add(new Ghost(new Texture2D[2] { Content.Load<Texture2D>("ghost2"), Content.Load<Texture2D>("fear") }, level.StartingPosition - Vector2.UnitY * 6 * Level.TILE_HEIGHT, level));
            this.mobileSprites.Add(new Ghost(new Texture2D[2] { Content.Load<Texture2D>("ghost3"), Content.Load<Texture2D>("fear") }, level.StartingPosition - Vector2.UnitY * 6 * Level.TILE_HEIGHT, level));
            this.mobileSprites.Add(new Ghost(new Texture2D[2] { Content.Load<Texture2D>("ghost4"), Content.Load<Texture2D>("fear") }, level.StartingPosition - Vector2.UnitY * 6 * Level.TILE_HEIGHT, level));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && LP >= 0)
                this.Exit();

            // TODO: Add your update logic here
            foreach(MobileSprite m in mobileSprites)
            {
                if (m.ToString() == "PacMan.Pacman")
                {
                    foreach(MobileSprite m2 in mobileSprites)
                    {
                        if (m2.ToString() == "PacMan.Ghost")
                            if (m2.HitBox.Intersects(m.HitBox)) ;
                                //m.Position = level.StartingPosition;
                    }
                }
            }

            Console.WriteLine(LP);
            //this.pacMan.CheckDecorCollision(this.level);
            foreach (MobileSprite ms in this.mobileSprites)
                ms.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            this.spriteBatch.Begin();
            this.level.Draw(this.spriteBatch);
            foreach(MobileSprite ms in this.mobileSprites)
                ms.Draw(this.spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
