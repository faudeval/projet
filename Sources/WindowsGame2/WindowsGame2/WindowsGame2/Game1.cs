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

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private static readonly int ROUNDS = 15;    // Nombre de balles / cibles

        GraphicsDeviceManager graphics;             // GraphicsDeviceManager (fonctionnement encore obscur)
        SpriteBatch spriteBatch;                    // SpriteBatch (là où est stocké l'affichage)

        private Texture2D _target;                  // Image de la cible
        private Texture2D _sight;                   // Image du réticule

        private List<Vector2> _targets;             // Liste contenant les positions des cibles
        private Random _rnd;                        // Variable permettant le positionnement aléatoire

        private int _windowWidth;                   // Largeur de la fenêtre
        private int _windowHeight;                  // Hauteur de la fenêtre
        private int _targetWidth;                   // Largeur d'une cible
        private int _targetHeight;                  // Hauteur d'une cible
        private int _sightWidth;                    // Largeur du réticule
        private int _sightHeight;                   // Hauteur du réticule

        private int _lastUpdate;                    // Instant de la dernière apparition d'une cible

        private MouseState _mouseState;             // Etat de la souris (utile entre autres pour récupérer la position du curseur)
        private MouseState _oldMouseState;          // Ancien état de la souris (utile pour savoir si l'utilisateur vient de cliquer ou pas)

        private int _bulletsLeft;                   // Nombre de balles restantes
        private int _score;                         // Score
        private int _targetsAppeared;               // Nombre de cibles apparues

        private SpriteFont _police;                 // Police utilisée pour écrire

        private bool _isPlaying;                    // Booléen tout simple pour savoir si l'utilisateur est encore en train de jouer ou a fini

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
            _targets = new List<Vector2>();
            _rnd = new Random();
            _windowWidth = Window.ClientBounds.Width;
            _windowHeight = Window.ClientBounds.Height;
            _lastUpdate = -1;
            _mouseState = Mouse.GetState();
            _bulletsLeft = ROUNDS;
            _score = 0;
            _targetsAppeared = 0;
            _isPlaying = true;

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

            _target = Content.Load<Texture2D>("cible");
            _targetWidth = _target.Width;
            _targetHeight = _target.Height;
            _sight = Content.Load<Texture2D>("reticule_final");
            _sightWidth = _sight.Width;
            _sightHeight = _sight.Height;
            _police = Content.Load<SpriteFont>("police");   // Police de taille < 40
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
            if (_isPlaying) // On joue tant qu'il nous reste des balles
            {
                if (_bulletsLeft <= 0)
                    _isPlaying = false;

                // Toutes les secondes, on ajoute un Vector2 dans _targets afin d'ajouter une cible à l'écran (on ajoute la cible dans l'espace en dessous des 50px réservés à l'affichage)
                int sec = gameTime.TotalGameTime.Seconds;
                if (sec < ROUNDS && sec > _lastUpdate)
                {
                    _lastUpdate = sec;
                    _targets.Add(new Vector2(_rnd.Next(_windowWidth - _targetWidth), _rnd.Next(50, _windowHeight - _targetHeight)));
                    _targetsAppeared++;
                }

                // Si l'utilisateur clique, alors on lui retire une balle et si le clic est sur une cible, on retire la cible et on lui ajoute des points
// TODO : Gérer les points
                /* Gestion des points :
                 * +10 si touché dans le centre
                 * Sinon on réduit jusqu'à ce que la distance avec le centre soit d'1/2 taille de la cible
                 * Attention au mode de calcul :
                 * la distance entre le centre c et le point p est :
                 * racine ((cx - px)² + (cy-py)²)
                 * on a donc points = 10 - (arrondi( racine((cx - px)² + (cy-py)²) ))/tailleCible)*10
                 * cx = (_targets[_targets.Count - 1 - cpt].X + _targetWidth) / 2
                 * px = _mouseState.X
                 * cy = (_targets[_targets.Count - 1 - cpt].Y + _targetHeight) / 2
                 * py = _mouseState.Y
                 * ce qui donne points = 10 - Math.Round(Math.Sqrt(((_targets[_targets.Count - 1 - cpt].X + _targetWidth) / 2)
                 * _score += 10 - (int)(Math.Round(Math.Sqrt(Math.Pow((_targets[_targets.Count - 1 - cpt].X + _targetWidth) / 2 - _mouseState.X, 2) + Math.Pow((_targets[_targets.Count - 1 - cpt].Y + _targetHeight) / 2 - _mouseState.Y, 2))) / (_targetWidth/2));
                 * ^FAUX
                 */
                if (_mouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released)
                {
                    _bulletsLeft--;
                    bool found = false;
                    int cpt = 0;
                    while (cpt < _targets.Count && !found)
                    {
                        if ((_targets[_targets.Count - 1 - cpt].X + _targetWidth > _mouseState.X) && (_targets[_targets.Count - 1 - cpt].X < _mouseState.X) && (_targets[_targets.Count - 1 - cpt].Y + _targetHeight > _mouseState.Y) && (_targets[_targets.Count - 1 - cpt].Y < _mouseState.Y))
                        {
                            _score++;
                            _targets.Remove(_targets[_targets.Count - 1 - cpt]);
                            found = true;
                        }
                        cpt++;
                    }
                }
            }
            else // Si on a fini de jouer, alors on attend un clic avant de quitter le jeu
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released)
                    this.Exit();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            // On affiche toutes les cibles
            foreach (Vector2 v in _targets)
                spriteBatch.Draw(_target, v, Color.White);


            if(_isPlaying) // Si on joue encore, on affiche les balles restantes et le score
            {
                String str = "Balles restantes : "+_bulletsLeft.ToString()+" Score : "+_score.ToString()+"/"+_targetsAppeared;
                spriteBatch.DrawString(_police, str,  new Vector2((_windowWidth - _police.MeasureString(str).X)/2 , 5f), Color.Black);
            }
            else // Sinon on affiche un message de victoire / défaite
            {
                String str = (_targets.Count == 0 && _targetsAppeared == ROUNDS) ? ("Bravo, vous avez gagné") : ("Dommage, vous avez manqué " + (ROUNDS + _targets.Count - _targetsAppeared).ToString() + " cibles");
                spriteBatch.DrawString(_police, str, new Vector2((_windowWidth - _police.MeasureString(str).X) / 2, (_windowHeight - _police.MeasureString(str).Y) / 2), Color.Black);
            }
            // On affiche le réticule là où devrait se trouver le pointeur de la souris
            spriteBatch.Draw(_sight, new Vector2(_mouseState.X - _sightWidth/2, _mouseState.Y - _sightHeight/2), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
