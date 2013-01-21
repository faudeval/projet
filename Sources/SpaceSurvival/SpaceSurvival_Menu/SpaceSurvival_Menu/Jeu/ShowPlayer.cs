#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameStateManagementSample;
#endregion

namespace SpaceSurvival
{
    class ShowPlayer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Fields
        public SpriteBatch spriteBatch;
        public SpriteFont font;
        private Ship ship;
        private Vector2 position;
        #endregion

        #region Initialization
        public ShowPlayer(Game game, Ship ship, Vector2 position)
            : base(game)
        {
            this.ship = ship;
            this.position = position;
        }

        public override void Initialize()
        {
            this.spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("police");
            base.LoadContent();
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(
                font,                                               /* SpriteFont spriteFont */
                ship.Pseudo + "    " + ship.points + " points",     /* string text */
                position,                                           /* Vector2 position */
                GameplayScreen.teamColor[ship.Team],                                        /* Color color */
                0f,                                                 /* float rotation */
                Vector2.Zero,                                       /* Vector2 origin */
                0.5f,                                               /* float scale */
                SpriteEffects.None,                                 /* SpriteEffects effects */
                0f                                                  /* float layerDepth */
            );

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
