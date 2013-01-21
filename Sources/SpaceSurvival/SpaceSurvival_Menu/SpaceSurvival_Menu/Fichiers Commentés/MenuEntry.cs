#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// Une entrée dans un MenuScreen.
    /// Affiche du texte et gère un evènement lors de sa sélection
    /// </summary>
    class MenuEntry
    {
        #region Fields
        /// <summary>
        /// Texte affiché pour cette entrée de menu
        /// </summary>
        string text;

        /// <summary>
        /// Suit l'effet de grossissement/rétrecissement lors de la sélection de l'entrée
        /// </summary>
        /// <remarks>
        /// Les entrées font une transition en sortant de l'effet de sélection lorsqu'elles ne sont plus sélectionnées
        /// </remarks>
        float selectionFade;

        /// <summary>
        /// La position où l'entrée de menu est dessinée.
        /// Elle est attribuée dans la fonction Update de MenuScreen
        /// </summary>
        Vector2 position;

        /// <summary>
        /// La taille de l'entrée du menu. Elle est actualisée à chaque passage dans Update
        /// </summary>
        Vector2 size;
        #endregion

        #region Properties
        /// <summary>
        /// Le texte de cette entrée de menu.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// La position où dessiner cette entrée de menu.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // Ajout : connaître la hauteur et la largeur du texte
        /// <summary>
        /// La hauteur et largeur du texte
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            private set { size = value; }
        }
        #endregion

        #region Events
        /// <summary>
        /// Evenement déclenché lorsque l'entrée de menu est sélectionnée
        /// </summary>
        public event EventHandler<PlayerIndexEventArgs> Selected;

        /// <summary>
        /// Methode pour déclencher l'évènement Selected
        /// </summary>
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Construit une nouvelle entrée de menu avec le texte spécifié
        /// </summary>
        public MenuEntry(string text)
        {
            this.text = text;
        }
        #endregion

        #region Update and Draw
        /// <summary>
        /// Met à jour l'entrée de menu
        /// </summary>
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // Lorsque la sélection de menu change, les entrées concernées changent
            // graduellement entre l'état sélectionné et celui de désélectionné plutôt
            // que d'apparaître instantanément dans le nouveau
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
            // Mise à jour de la taille effective du texte
            Size = screen.ScreenManager.Font.MeasureString(Text) - Vector2.UnitY * GetHeight(screen) / 2;
        }

        /// <summary>
        /// Dessine l'entrée de menu.
        /// </summary>
        public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // Si l'entrée est sélectionnée, elle sera dessinée en jaune, sinon en blanc
            Color color = isSelected ? Color.Yellow : Color.White;

            // Donne le rythme du changement de taille de l'entrée sélectionnée
            double time = gameTime.TotalGameTime.TotalSeconds;
            
            float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1 + pulsate * 0.05f * selectionFade;

            // Modifie l'alpha pour faire apparaitre/disparaitre le texte lors des transitions
            color *= screen.TransitionAlpha;

            // Dessine le texte centré sur chaque ligne
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color, 0,
                                   origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Demande la hauteur nécessaire à cette entrée de menu
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }

        /// <summary>
        /// Demande la largeur nécessaire à cette entrée de menu (utilisée pour la centrer)
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }
        #endregion
    }
}
