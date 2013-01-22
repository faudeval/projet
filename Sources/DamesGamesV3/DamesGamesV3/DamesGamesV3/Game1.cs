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

namespace DamesGamesV3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Game game;

         // Création des variables déstinées à contenir les images des jetons
        public Texture2D JBlanc;
        public Texture2D JNoir;

        //Création des listes contenant respectivement les jetons blancs et les jetons noirs
        //public static List<Jeton> ListeJetonsBlanc = new List<Jeton>();
        // public static List<Jeton> ListeJetonsNoir = new List<Jeton>();

        // Largeur de la fenêtre
        private int WindowWidth;
        // Hauteur de la fenêtre
        private int WindowHeight;

        // Police
        private SpriteFont TestFont;

        //Hauteur et Largeur des jetons
        public int JBlancWidth;
        public int JBlancHeight;
        public int JNoirWidth;
        public int JNoirHeight;

        //Gestion de la souris
        public MouseState mouseState;
        public MouseState oldMouseState;
        public int PosXMouse;
        public int PosYMouse;

        // Booléen indiquant si un jeton est selectionné
        Boolean IsSelected = new Boolean();
        // Numéro du jeton selectionné
        int NumJetonSelect;

        // Variable permettant de numéroter chaques jetons indépendemment
        int Num = 0;

        // Création du tableau représentatif de la grille
        public Jeton[,] Grille = new Jeton[10,10]; 

        // Variables de test
        public Vector2 afficheMoi;
        public string test;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = this;
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
            // On initialise le rendu graphique de nos jetons
            JBlanc = Content.Load<Texture2D>("JetonBlanc");
            JNoir = Content.Load<Texture2D>("JetonNoir");

            // On initialise les dimensions des jetons
            JBlancHeight = JBlanc.Height;
            JBlancWidth = JBlanc.Width;
            JNoirHeight = JNoir.Height;
            JNoirWidth = JNoir.Width;

            // On affiche la souris
            this.IsMouseVisible = true;       

            // Aucun jeton n'est selectionné
            IsSelected = false;

            // On initialise les dimensions de la fenêtres     
            this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 10 * JBlancWidth;
            this.graphics.PreferredBackBufferHeight = 10 * JBlancHeight;
            this.graphics.ApplyChanges();
            WindowWidth = Window.ClientBounds.Width;
            WindowHeight = Window.ClientBounds.Height;

            // Titre de la fenêtre
            this.Window.Title = "Mon premier jeu de Dames !";

            // Variables de test
            test = "";

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
            TestFont = Content.Load<SpriteFont>("MyFont");

            // Remplissage du tableau de 100 cases (damier) 
            // avec des Jetons différenciés par une couleur, 
            // un numéro et une position
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Grille[i, j] = null;
                }
            }

            for (int i = 0; i < 5; i++) 
            {
                Grille[0, i * 2 + 1] = new Jeton("noir", JBlancWidth * (2 * i + 1), 0, Num, this);
                Num++;
                Grille[1, i * 2] = new Jeton("noir", JBlancWidth * (i * 2), JBlancHeight, Num, this);
                Num++;
                Grille[2, i * 2 + 1] = new Jeton("noir", JBlancWidth * (2 * i + 1), 2 * JBlancHeight, Num, this);
                Num++;
                Grille[3, i * 2] = new Jeton("noir", JBlancWidth * (i * 2), 3 * JBlancHeight, Num, this);
                Num++;

                Grille[6, i * 2 + 1] = new Jeton("blanc", (2 * i * JBlancWidth), (WindowHeight - JBlancHeight), Num, this);
                Num++;
                Grille[7, i * 2] = new Jeton("blanc", ((2 * i + 1) * JBlancWidth), (WindowHeight - 2 * JBlancHeight), Num, this);
                Num++;
                Grille[8, i * 2 + 1] = new Jeton("blanc", (2 * i * JBlancWidth), (WindowHeight - 3 * JBlancHeight), Num, this);
                Num++;
                Grille[9, i * 2] = new Jeton("blanc", ((2 * i + 1) * JBlancWidth), (WindowHeight - 4 * JBlancHeight), Num, this);
                Num++;


            }
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
            // On quitte le jeu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Jeton j = new Jeton("blanc", 150, 150, 9000, this);
            mouseState = Mouse.GetState();  

            // TODO: Add your update logic here
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                // On stocke la position actuelle du curseur
                PosXMouse = mouseState.X;
                PosYMouse = mouseState.Y;

                // Tests
                test = "Un";

                if (j.IsJetonPresent(PosXMouse, PosYMouse) != -1)
                {
                    // Un jeton est selectionné
                    IsSelected = true;
                    // On récupère son numéro
                    NumJetonSelect = j.IsJetonPresent(PosXMouse, PosYMouse);

                    // Tests
                    test = "Deux";

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        // On met à jour la position du curseur,
                        // et on stocke ainsi la position de la case 
                        // où le joueur souhaite déplacer son jeton
                        PosXMouse = mouseState.X;
                        PosYMouse = mouseState.Y;

                        // Tests
                        test = "Trois";

                        if (IsSelected && j.IsJetonPresent(PosXMouse, PosYMouse) == -1)
                        {
                            // Tests
                            test = "Quatre";

                            // On supprime le jeton 'NumJetonSelect'
                            // On le redessine dans la case sélectionnée
                            // On va donc mettre à jour la position du jeton sélectionné,
                            // puis on va le redessiné, sans oublier de supprimer 'son ancienne image'

                            //Jeton jeton = new Jeton("blanc", PosXMouse, PosYMouse, NumJetonSelect);
                            //ListeJetonsBlanc.Add(jeton);

                            /*for (int l = 0; l < 10; l++ ) {
                                if (PosXMouse > l * JBlancWidth && PosXMouse < (l + 1) * JBlancWidth)
                                    if (m.CoulJeton(NumJetonSelect) == "blanc")
                                        ListeJetonsBlanc.Find(Jeton("blanc", 0, 0, NumJetonSelect));
                                    else
                                        ListeJetonsNoir.Find(Jeton("noir", 0, 0, NumJetonSelect));
                            }*/
                        }
                    }
                    // Le jeton a été déplacé et n'est donc plus séléctionné
                    IsSelected = false;
                }
            }
            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin();

            /*foreach(Jeton j in ListeJetonsBlanc){
                spriteBatch.Draw(JBlanc, j.position, Color.White);
            }

            foreach (Jeton j in ListeJetonsNoir)
            {
                spriteBatch.Draw(JNoir, j.position, Color.White);
            }*/

            for(int i=0; i<10; i++) 
            {
                for(int j=0; j<10; j++){

                    if(Grille[i,j] != null){
                        if (Grille[i,j].couleur == "noir")
                           spriteBatch.Draw(JNoir, Grille[i,j].position, Color.White);
                        else
                           spriteBatch.Draw(JBlanc,Grille[i,j].position , Color.White);
                    }

                }
            }

            if (afficheMoi != null)
                spriteBatch.DrawString(TestFont, test + " : " + NumJetonSelect + "", afficheMoi, Color.DarkOliveGreen);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
