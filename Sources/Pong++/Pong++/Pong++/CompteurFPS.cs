using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Ne pas oublier de changer le nom du namespace et de créer la police "MyFont".
// A noter que la classe n'est pas très optimisé du fait que le recréer un spriteBatch et qu'on le begin et end
// Utilisation dans une classe dérivant de Game :
// CompteurFPS fps = new CompteurFPS(this);
// Components.Add(fps);
namespace Pong__
{
    
   public class CompteurFPS : Microsoft.Xna.Framework.DrawableGameComponent
   {
       //Composant de dessin
       public SpriteBatch spriteBatch;
       //Police de caractêres
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
           this.spriteFont = this.Game.Content.Load<SpriteFont>("SpriteFont1");
           base.LoadContent();
       }

       public override void Draw(GameTime gameTime)
       {
           this.FPS = 1000.0d / gameTime.ElapsedGameTime.TotalMilliseconds;
           
           string texte = string.Format("{0:00 }", this.FPS);
           Vector2 taille = this.spriteFont.MeasureString(texte);

           this.spriteBatch.Begin();
           this.spriteBatch.DrawString(this.spriteFont, texte, new Vector2(this.GraphicsDevice.Viewport.Width - taille.X, 5), Color.Pink);
           this.spriteBatch.End();

           base.Draw(gameTime);
       }

   }     
}
