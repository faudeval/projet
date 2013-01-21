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
using System.Diagnostics;

//Ne pas oublier de changer le nom du namespace et de cr�er la police "MyFont".
// A noter que la classe n'est pas tr�s optimis� du fait que le recr�er un spriteBatch et qu'on le begin et end
// Utilisation dans une classe d�rivant de Game :
// CompteurFPS fps = new CompteurFPS(this);
// Components.Add(fps);
namespace T2D
{
    
   public class CompteurFPS : Microsoft.Xna.Framework.DrawableGameComponent
   {
       //Composant de dessin
       public SpriteBatch spriteBatch;
       //Police de caract�res
       public SpriteFont spriteFont;
       //Nombre d'images par secondes
       public double FPS = 0.0f;

       public CompteurFPS(Game game) : base(game)
       {
       
       }

       public override void Initialize()
       {
           this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
           base.Initialize();
       }

       protected override void LoadContent()
       {
           this.spriteFont = this.Game.Content.Load<SpriteFont>("fpsfont");
           base.LoadContent();
       }

       public override void Draw(GameTime gameTime)
       {
           this.FPS = 1000.0d / gameTime.ElapsedGameTime.TotalMilliseconds;
           
           string texte = string.Format("{0:00 }", this.FPS);
           Vector2 taille = this.spriteFont.MeasureString(texte);

           this.spriteBatch.Begin();
           this.spriteBatch.DrawString(this.spriteFont, texte, new Vector2(this.GraphicsDevice.Viewport.Width - taille.X, 5), Color.White);
           this.spriteBatch.End();

           base.Draw(gameTime);
       }

   }     
}
