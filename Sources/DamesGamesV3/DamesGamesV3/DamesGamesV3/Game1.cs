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


        // Cr�ation des variables d�stin�es � contenir les images des jetons
        public Texture2D JBlanc;
        public Texture2D JNoir;

        // Texture pour cases
        public Texture2D CMarron;
        public Texture2D CBlanche;
        public Texture2D CGrise;
        public Texture2D CRougeB;

        // Largeur de la fen�tre
        private int WindowWidth;
        // Hauteur de la fen�tre
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
        public int NewPosXMouse;
        public int NewPosYMouse;

        // Bool�en indiquant si un jeton est selectionn�
        Boolean IsSelected = new Boolean();
        // Num�ro du jeton selectionn�
        int NumJetonSelect;

        // Variable permettant de num�roter chaques jetons ind�pendemment
        int Num = 1;

        // Cr�ation du tableau repr�sentatif de la grille
        public Jeton[,] Grille = new Jeton[10, 10]; 

        // Tableau permettant l'affichage des cases de couleurs diff�rentes
        public int[,] CoulGrille = new int[10, 10];

        // Nos 2 Joueurs
        Joueur J1 = new Joueur(0, 20);
        Joueur J2 = new Joueur(0, 20);

        // Variables de test
        public Vector2 testAff;
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

            // Texture cases
            CMarron = Content.Load<Texture2D>("CaseMarron");
            CBlanche = Content.Load<Texture2D>("CaseBlanche");
            CGrise = Content.Load<Texture2D>("CaseGrise");
            CRougeB = Content.Load<Texture2D>("CaseRougeBois");

            // On initialise les dimensions des jetons
            JBlancHeight = JBlanc.Height;
            JBlancWidth = JBlanc.Width;
            JNoirHeight = JNoir.Height;
            JNoirWidth = JNoir.Width;

            // On affiche la souris
            this.IsMouseVisible = true;       

            // Aucun jeton n'est selectionn�
            IsSelected = false;

            // On initialise les dimensions de la fen�tres     
            this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 10 * JBlancWidth;
            this.graphics.PreferredBackBufferHeight = 10 * JBlancHeight;
            this.graphics.ApplyChanges();
            WindowWidth = Window.ClientBounds.Width;
            WindowHeight = Window.ClientBounds.Height;

            // Titre de la fen�tre
            this.Window.Title = "Jeu de Dames ";

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

            // Remplissage du tableau permettant l'affichage du damier
            // La couleur des cases pouvant varier au gr�s des envies
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Grille[i, j] = null;
                    CoulGrille[i, j] = 0;
                    int a = (i % 2);
                    int b = (j % 2);
                    if (a == 0 && b == 0 || a == 1 && b == 1)
                        CoulGrille[i, j] = 1;
                }
            }

            // Remplissage du tableau de Jetons
            // Ces derniers �tant diff�renci�s par une couleur, 
            // une position et un num�ro
            for (int i = 0; i < 5; i++) 
            {
                Grille[0, i * 2 + 1] =  new Jeton("noir", JBlancWidth * (2 * i + 1), 0, Num++, this);
                Grille[1, i * 2]     =  new Jeton("noir", JBlancWidth * (i * 2), JBlancHeight, Num++, this);
                Grille[2, i * 2 + 1] =  new Jeton("noir", JBlancWidth * (2 * i + 1), 2 * JBlancHeight, Num++, this);
                Grille[3, i * 2]     =  new Jeton("noir", JBlancWidth * (i * 2), 3 * JBlancHeight, Num++, this);
                Grille[6, i * 2 + 1] =  new Jeton("blanc", (2 * i * JBlancWidth), (7 * JBlancHeight), Num++, this);
                Grille[7, i * 2]     =  new Jeton("blanc", ((2 * i + 1) * JBlancWidth), (6 * JBlancHeight), Num++, this);
                Grille[8, i * 2 + 1] =  new Jeton("blanc", (2 * i * JBlancWidth), (9 * JBlancHeight), Num++, this);
                Grille[9, i * 2]     =  new Jeton("blanc", ((2 * i + 1) * JBlancWidth), (8 * JBlancHeight), Num++, this);
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
            // Jeton 'bidon' permettant l'utilisation des m�thodes de la classe Jeton
            Jeton j = new Jeton("blanc", 150, 150, 9000, this);

            // Variable permettant de r�cup�rer la position du curseur
            mouseState = Mouse.GetState();

            // Bool�en permettant de savoir si un jeton est s�lectionn�
            IsSelected = false;

            // Condition principale pour le d�placement d'un jeton
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                // On stocke la position actuelle du curseur
                PosXMouse = mouseState.X;
                PosYMouse = mouseState.Y;

                if (j.IsJetonPresent(PosXMouse, PosYMouse) != -1)
                {
                    // Un jeton est selectionn�
                    IsSelected = true;
                    // On r�cup�re son num�ro
                    NumJetonSelect = j.IsJetonPresent(PosXMouse, PosYMouse);

                    // mouseState.RightButton == ButtonState.Pressed
                    if (IsSelected == true)
                    {
                        // On met � jour la position du curseur,
                        // et on stocke ainsi la position de la case 
                        // o� le joueur souhaite d�placer son jeton
                        NewPosXMouse = mouseState.X;
                        NewPosYMouse = mouseState.Y;

                        while (Math.Abs(NewPosXMouse - PosXMouse) <= 2 && Math.Abs(NewPosYMouse - PosYMouse) <= 2 && oldMouseState.LeftButton == ButtonState.Released)
                        {
                            // Solution interm�diaire permettant 'de gagner du temps'
                            // afin de r�cup�rer la position du curseur une seconde fois
                            System.Threading.Thread.Sleep(1500);
                            mouseState = Mouse.GetState();

                            NewPosXMouse = mouseState.X;
                            NewPosYMouse = mouseState.Y;

                            oldMouseState = mouseState;
                        }

                        // Valeurs comprises entre 0 et 9
                        // repr�sentant la case cibl�e par  le joueur
                        // et l'ancienne case 
                        int NewX = j.RenvoiePosX(NewPosXMouse);
                        int NewY = j.RenvoiePosY(NewPosYMouse);
                        int OldX = j.RenvoiePosX(PosXMouse);
                        int OldY = j.RenvoiePosY(PosYMouse);

                        // On v�rifit qu'il n'y a pas de jeton l� o� le joueur souhaite d�placer son jeton
                        // Et si le d�placement est correct (en diagonale et � une case de distance) 
                        if (j.IsJetonPresent(NewPosXMouse, NewPosYMouse) == -1 
                            && (Math.Abs(NewX - OldX) == JBlancWidth) 
                            && (Math.Abs(NewY - OldY) == JBlancHeight))
                        { 
                            // Le cas �ch�ant, on d�place le jeton aux coordonn�es choisies
                            if(j.DeplaceJeton(NewPosXMouse, NewPosYMouse, NumJetonSelect))
                            {
                                
                            }

                        }
                            
                        // Sinon, on v�rifie toujours qu'il n'y a pas de jeton,
                        // Que le d�placement est correct (en diagonale)
                        // Et qu'il y a bien un jeton de la couleur oppos�e entre les deux positions (son ancienne et sa nouvelle)
                        else if (j.IsJetonPresent(NewPosXMouse, NewPosYMouse) == -1 
                            && (Math.Abs(NewX - OldX) == 2 * JBlancWidth) 
                            && (Math.Abs(NewY - OldY) == 2 * JBlancHeight))
                        {
                            // Le cas �ch�ant, on d�place le jeton aux coordonn�es choisies
                            if (j.DeplaceJeton(NewPosXMouse, NewPosYMouse, NumJetonSelect))
                            {
                                // Si le jeton d�plac� �tait blanc,
                                // On supprime le jeton noir en question,
                                // On retire un jeton au compteur du joueur des jetons noirs
                                // Et on rajoute un point au joueur des jetons blancs
                                if (j.CoulJeton(NumJetonSelect) == "blanc")
                                {
                                    if (((NewX / JBlancWidth) - (OldX / JBlancWidth)) == -2)
                                    {
                                        Grille[((OldX / JBlancWidth) - 1), ((OldY / JBlancHeight) - 1)] = null;
                                    }
                                    else 
                                    {
                                        Grille[((OldX / JBlancWidth) + 1), ((OldY / JBlancHeight) - 1)] = null;
                                    }
                                    
                                    J1.NbJetonsRestants--;
                                    J2.point++;
                                }

                                // Si le jeton d�plac� �tait noir,
                                // On supprime le jeton blanc en question,
                                // On retire un jeton au compteur du joueur des jetons blancs
                                // Et on rajoute un point au joueur des jetons noirs
                                else
                                {
                                    if (((NewX / JBlancWidth) - (OldX / JBlancWidth)) == -2)
                                    {
                                        Grille[((OldX / JBlancWidth) - 1), ((NewY / JBlancHeight) - 1)] = null;
                                    }
                                    else
                                    {
                                        Grille[((OldX / JBlancWidth) + 1),  ((NewY / JBlancHeight) - 1)] = null;
                                    }

                                    J2.NbJetonsRestants--;
                                    J1.point++;
                                }
                            }

                        }
                        
                        // Le jeton a soit �t� d�plac�, soit  le joueur souhaite d�plac� un autre jeton
                        // soit le joueur a proc�d� � une mauvaise s�lection
                        IsSelected = false;
                    }
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
            //GraphicsDevice.Clear(Color.Chocolate);

            spriteBatch.Begin();
            
            // Tant qu'aucun joueur ne perds la partie
            if (!J1.TuPerdsOuBien() && !J2.TuPerdsOuBien())
            {
                // On parcours le tableau d'entiers
                // Pour afficher le damier
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (CoulGrille[i, j] == 0)
                            spriteBatch.Draw(CGrise, new Rectangle(i * JBlancWidth, j * JBlancHeight, JBlancWidth, JBlancHeight), Color.White);
                        else
                            spriteBatch.Draw(CRougeB, new Rectangle(i * JBlancWidth, j * JBlancHeight, JBlancWidth, JBlancHeight), Color.White);
                    }

                }

                // On parcours le tableau de jetons 
                // Pour afficher les jetons restants de chaque joueur
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (Grille[i, j] != null)
                        {
                            if (Grille[i, j].couleur == "noir")
                                spriteBatch.Draw(JNoir, Grille[i, j].position, Color.White);
                            else
                                spriteBatch.Draw(JBlanc, Grille[i, j].position, Color.White);

                            // Boucle de test qui affiche le num�ro de chaque jeton sur le-dit jeton
                            /*testAff.X = Grille[i, j].position.X;
                            testAff.Y = Grille[i, j].position.Y;
                            spriteBatch.DrawString(TestFont, "" + Grille[i, j].num + "", testAff, Color.Cyan);
                            Console.WriteLine(Grille[i, j].num); */
                        }
                    }
                }

                // Srites permettant d'afficher en temps r�el le score de chaque joueurs
                spriteBatch.DrawString(TestFont, "J1 : ", new Vector2(5, 0), Color.LightCyan);
                spriteBatch.DrawString(TestFont, "" + J1.point + " pts", new Vector2(5, 25), Color.LightCyan);
                spriteBatch.DrawString(TestFont, "J2 : ", new Vector2(WindowWidth - JBlancWidth + 5, WindowHeight - JBlancHeight), Color.LightCyan);
                spriteBatch.DrawString(TestFont, "" + J2.point + " pts", new Vector2(WindowWidth - JBlancWidth + 5, WindowHeight - 40), Color.LightCyan);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
