#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// Un "popup" demandant une confirmation
    /// </summary>
    class MessageBoxScreen : GameScreen
    {
        #region Fields
        string message;
        Texture2D gradientTexture;
        InputAction menuSelect;
        InputAction menuCancel;
/* Ajout */
        MyButton yesButton;
        MyButton noButton;
/* Fin d'ajout */
        #endregion

        #region Events
        public event EventHandler<PlayerIndexEventArgs> Accepted; // Evenement lancé lors de la confirmation
        public event EventHandler<PlayerIndexEventArgs> Cancelled; // Evenement lancé lors de l'annulation
        #endregion

        #region Initialization
        /// <summary>
        /// Constructeur par défaut ajoutant automatiquement Y/N à la fin
        /// </summary>
        public MessageBoxScreen(string message)
            : this(message, true)
        { }

/* Ajouts : 
 * modification du message par défaut (Y/N)
 * modification des touches déclencheuses d'évènement
 */
        /// <summary>
        /// Constructeur permettant à l'utilisateur de choisir d'écrire Y/N ou pas à la fin
        /// </summary>
        public MessageBoxScreen(string message, bool includeUsageText)
        {
            const string usageText = "\nY/N"; 
            
            if (includeUsageText)
                this.message = message + usageText;
            else
                this.message = message;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);

            menuSelect = new InputAction(
                new Buttons[] { Buttons.A, Buttons.Start },     // Boutons déclanchant cet évènement
                new Keys[] { Keys.Y, Keys.Enter },              // Touches déclanchant cet évènement
                true);                                          // La touche doit être relâchée avant d'être appuyée
            menuCancel = new InputAction(
                new Buttons[] { Buttons.B, Buttons.Back },
                new Keys[] { Keys.N, Keys.Escape },
                true);
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                ContentManager content = ScreenManager.Game.Content;
                gradientTexture = content.Load<Texture2D>("gradient");
                yesButton = new MyButton("Oui", content.Load<Texture2D>("accept_button"));
                noButton = new MyButton("Non", content.Load<Texture2D>("refuse_button"));
            }
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            PlayerIndex playerIndex;

            // We pass in our ControllingPlayer, which may either be null (to
            // accept input from any player) or a specific index. If we pass a null
            // controlling player, the InputState helper returns to us which player
            // actually provided the input. We pass that through to our Accepted and
            // Cancelled events, so they can tell which player triggered them.
            
            if (menuSelect.Evaluate(input, ControllingPlayer, out playerIndex) || yesButton.IsClicked(Mouse.GetState()))
            {
                // Raise the accepted event, then exit the message box.
                if (Accepted != null)
                    Accepted(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
            else if (menuCancel.Evaluate(input, ControllingPlayer, out playerIndex) || noButton.IsClicked(Mouse.GetState()))
            {
                // Raise the cancelled event, then exit the message box.
                if (Cancelled != null)
                    Cancelled(this, new PlayerIndexEventArgs(playerIndex));

                ExitScreen();
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Dessine le popup
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Assombrit l'image en dessous du popup
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Centre le texte dans le Viewport
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // Marge autour du texte
            const int hPad = 32;
            const int vPad = 16;

            // Marge autour des boutons
            const int buttonH = 20;

            // Centrage des boutons sous le texte
            yesButton.Position = new Vector2(
                (viewportSize.X - buttonH) / 2 - yesButton.Bounds.Width,
                textPosition.Y + textSize.Y + 2 * vPad);
            noButton.Position = new Vector2(
                (viewportSize.X + buttonH) / 2,
                textPosition.Y + textSize.Y + 2 * vPad);

            // Rectangle englobant le texte
            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, color);

            yesButton.Draw(spriteBatch, font, color);
            noButton.Draw(spriteBatch, font, color);

            spriteBatch.End();
        }


        #endregion
    }
}
