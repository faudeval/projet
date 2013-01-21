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

namespace Global
{
    public enum GameState { Menu, DuckHunt, Pong, Pause }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Jeu : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        DuckHunt duckHunt;
        Pong pong;

        GameState state;

        public MouseState _mouseState;             // Etat de la souris (utile entre autres pour récupérer la position du curseur)
        public MouseState _oldMouseState;          // Ancien état de la souris (utile pour savoir si l'utilisateur vient de cliquer ou pas)

        public Jeu()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Menu";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            state = GameState.Menu;
            _mouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {}

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _oldMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            switch (state)
            {
                case GameState.DuckHunt:
                    duckHunt.update(gameTime);
                    break;
                case GameState.Pong:
                    pong.update(gameTime);
                    break;
                case GameState.Menu:
                case GameState.Pause:
                    if (Keyboard.GetState().GetPressedKeys().Contains(Keys.D))
                    {
                        state = GameState.DuckHunt;
                        duckHunt = new DuckHunt(this);
                        duckHunt.init(gameTime);
                        duckHunt.load();
                    }
                    if (Keyboard.GetState().GetPressedKeys().Contains(Keys.P))
                    {
                        state = GameState.Pong;
                        pong = new Pong(this);
                        pong.init(gameTime);
                        pong.load();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (state)
            {
                case GameState.DuckHunt:
                    duckHunt.draw(gameTime);
                    break;
                case GameState.Pong:
                    pong.draw(gameTime);
                    break;
                case GameState.Menu:
                    break;
                case GameState.Pause:
                    break;
            }

            base.Draw(gameTime);
        }

        public void setState(GameState state)
        {
            this.state = state;
        }
    }
}
