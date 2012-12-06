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
 * Essai du menu
 * */
namespace WindowsGame3
{
    public class Button
    {
        private static Texture2D _buttonLeft;           // Bord gauche du bouton
        private static Texture2D _buttonRight;          // Bord droit du bouton
        private static Texture2D _buttonMotif;          // Milieu du bouton
        private static Texture2D _buttonHoverLeft;      // Bord gauche du bouton ( survol )
        private static Texture2D _buttonHoverRight;     // Bord droit du bouton ( survol )
        private static Texture2D _buttonHoverMotif;     // Milieu du bouton ( survol )
        private static SpriteFont _font;                // Police d'écriture
        private static ContentManager _contentManager;  // ContentManager (utile pour charger le contenu)

        public static void Load(ContentManager cm)
        {
            _contentManager = cm;
            _font = cm.Load<SpriteFont>("police");
            _buttonLeft = cm.Load<Texture2D>("boutonLeft");
            _buttonRight = cm.Load<Texture2D>("boutonRight");
            _buttonMotif = cm.Load<Texture2D>("boutonMotif");
            _buttonHoverLeft = cm.Load<Texture2D>("bouton2Left");
            _buttonHoverMotif = cm.Load<Texture2D>("bouton2Right");
            _buttonHoverRight = cm.Load<Texture2D>("bouton2Motif");
        }

        public Button(string l, Vector2 p, Boolean h)
        {
            Label = l;
            Position = p;
            Hover = h;
            Size = Vector2.Zero;
        }
        public string Label { get; set; }       // Texte du bouton
        public Vector2 Position { get; set; }   // Position du bouton
        public Boolean Hover { get; set; }      // Le bouton est survolé ?
        public Vector2 Size { get; set; }       // Taille du bouton

        public void Show(SpriteBatch sb)
        {
/*
 * On dessine le bouton :
 * on affiche d'abord le bord gauche,
 * ensuite on regarde quelle taille fera le texte et on répète le motif du milieu sur toute cette taille,
 * ensuite on place le bord droit. 
 * Pour finir, on écrit le texte.
 * */
            Vector2 size = _font.MeasureString(Label);
            sb.Draw((Hover) ? (_buttonHoverLeft) : (_buttonLeft), Position, Color.White);
            for(int i=0; i<size.X; i++)
                sb.Draw((Hover) ? (_buttonHoverMotif) : (_buttonMotif), Position + new Vector2(((Hover) ? (_buttonHoverLeft) : (_buttonLeft)).Width + i, 0), Color.White);
            sb.Draw((Hover) ? (_buttonHoverRight) : (_buttonRight), Position + new Vector2(((Hover) ? (_buttonHoverLeft) : (_buttonLeft)).Width + size.X, 0), Color.White);
            sb.DrawString(_font, Label, Position + new Vector2(((Hover) ? (_buttonHoverLeft) : (_buttonLeft)).Width, (((Hover) ? (_buttonHoverLeft) : (_buttonLeft)).Height - size.Y) / 2), Color.Black);
            Size = new Vector2((((Hover) ? (_buttonHoverMotif) : (_buttonMotif)).Width + size.X + ((Hover) ? (_buttonHoverRight) : (_buttonRight)).Width), ((Hover) ? (_buttonHoverLeft) : (_buttonLeft)).Height);
        }

        public void Update(MouseState ms)
        {
            if (ms.X > Position.X && ms.X < Position.X + Size.X && ms.Y > Position.Y && ms.Y < Position.Y + Size.Y)
                Hover = true;
            else
                Hover = false;
        }
    }

    enum State { Begin, Game, End };
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        State state;    // Etat du jeu (Menu, Jeu ou fin)

        Texture2D _arrierePlan;
        List<Button> _boutons;

        MouseState _mouseState;
        MouseState _oldMouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;    // On fixe la fenêtre à 800*600 pour que l'arrière plan aille bien 
            graphics.PreferredBackBufferHeight = 600;
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
            state = State.Begin;
            Button.Load(Content);   // Chargement des variables statiques des Boutons
            _boutons = new List<Button>();
            _mouseState = Mouse.GetState();
            _boutons.Add(new Button("Créer un profil", new Vector2(100, 100), false));
            _boutons.Add(new Button("Charger un profil", new Vector2(100, 200), false));
            _boutons.Add(new Button("Portnawak", new Vector2(100, 300), false));

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
            Console.WriteLine(Window.ClientBounds.Height.ToString());
            _arrierePlan = Content.Load<Texture2D>("arriere_plan_800_600");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            _oldMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            foreach(Button b in _boutons)   // On vérifie si tous les boutons sont survolés
                b.Update(_mouseState);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(_arrierePlan, Vector2.Zero, Color.White);

            foreach (Button b in _boutons)
                b.Show(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
