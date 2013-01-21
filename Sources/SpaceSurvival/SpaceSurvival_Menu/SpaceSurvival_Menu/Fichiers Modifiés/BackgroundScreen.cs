#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;

using Microsoft.Xna.Framework.Input;

using System.Collections.Generic; // List<>
#endregion

namespace GameStateManagementSample
{
    #region Description
/*
 * Cette classe permet d'afficher des images de fond lorsqu'on est par exemple dans le menu.
 * Pour l'utiliser il suffit de modifier le contenu de loadTexture()
*/
    #endregion
    class BackgroundScreen : GameScreen
    {
        #region Fields
        ContentManager content;
        List<Texture2D> backgroundTexture; // Liste de Texture2D qui se succèdent pour faire un fond animé
        TimeSpan begin = TimeSpan.MinValue;
        int FPS = 1; // Nombre de Texture2D différentes affichées par seconde
        int currentImage;
        #endregion

        #region Initialization
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                loadTexture();
            }
        }

        public void loadTexture() // Chargement des images de fond (en mettre plusieurs pour un fond animé)
        {
            backgroundTexture = new List<Texture2D>();
            backgroundTexture.Add(content.Load<Texture2D>("background"));
            //backgroundTexture.Add(content.Load<Texture2D>("background2"));
            FPS = 30;
        }

        public override void Unload()
        {
            content.Unload();
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (begin == TimeSpan.MinValue)
                begin = gameTime.TotalGameTime;
            currentImage = (int)((gameTime.TotalGameTime - begin).TotalSeconds * FPS) % backgroundTexture.Count;
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture[currentImage], fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

            spriteBatch.End();
        }


        #endregion
    }
}
