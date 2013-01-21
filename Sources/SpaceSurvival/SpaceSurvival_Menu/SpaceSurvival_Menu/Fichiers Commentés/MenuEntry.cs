#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// Une entr�e dans un MenuScreen.
    /// Affiche du texte et g�re un ev�nement lors de sa s�lection
    /// </summary>
    class MenuEntry
    {
        #region Fields
        /// <summary>
        /// Texte affich� pour cette entr�e de menu
        /// </summary>
        string text;

        /// <summary>
        /// Suit l'effet de grossissement/r�trecissement lors de la s�lection de l'entr�e
        /// </summary>
        /// <remarks>
        /// Les entr�es font une transition en sortant de l'effet de s�lection lorsqu'elles ne sont plus s�lectionn�es
        /// </remarks>
        float selectionFade;

        /// <summary>
        /// La position o� l'entr�e de menu est dessin�e.
        /// Elle est attribu�e dans la fonction Update de MenuScreen
        /// </summary>
        Vector2 position;

        /// <summary>
        /// La taille de l'entr�e du menu. Elle est actualis�e � chaque passage dans Update
        /// </summary>
        Vector2 size;
        #endregion

        #region Properties
        /// <summary>
        /// Le texte de cette entr�e de menu.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// La position o� dessiner cette entr�e de menu.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // Ajout : conna�tre la hauteur et la largeur du texte
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
        /// Evenement d�clench� lorsque l'entr�e de menu est s�lectionn�e
        /// </summary>
        public event EventHandler<PlayerIndexEventArgs> Selected;

        /// <summary>
        /// Methode pour d�clencher l'�v�nement Selected
        /// </summary>
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Construit une nouvelle entr�e de menu avec le texte sp�cifi�
        /// </summary>
        public MenuEntry(string text)
        {
            this.text = text;
        }
        #endregion

        #region Update and Draw
        /// <summary>
        /// Met � jour l'entr�e de menu
        /// </summary>
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // Lorsque la s�lection de menu change, les entr�es concern�es changent
            // graduellement entre l'�tat s�lectionn� et celui de d�s�lectionn� plut�t
            // que d'appara�tre instantan�ment dans le nouveau
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
            // Mise � jour de la taille effective du texte
            Size = screen.ScreenManager.Font.MeasureString(Text) - Vector2.UnitY * GetHeight(screen) / 2;
        }

        /// <summary>
        /// Dessine l'entr�e de menu.
        /// </summary>
        public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // Si l'entr�e est s�lectionn�e, elle sera dessin�e en jaune, sinon en blanc
            Color color = isSelected ? Color.Yellow : Color.White;

            // Donne le rythme du changement de taille de l'entr�e s�lectionn�e
            double time = gameTime.TotalGameTime.TotalSeconds;
            
            float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1 + pulsate * 0.05f * selectionFade;

            // Modifie l'alpha pour faire apparaitre/disparaitre le texte lors des transitions
            color *= screen.TransitionAlpha;

            // Dessine le texte centr� sur chaque ligne
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color, 0,
                                   origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Demande la hauteur n�cessaire � cette entr�e de menu
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }

        /// <summary>
        /// Demande la largeur n�cessaire � cette entr�e de menu (utilis�e pour la centrer)
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }
        #endregion
    }
}
