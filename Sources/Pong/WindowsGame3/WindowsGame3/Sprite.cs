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

namespace Pong
{
    //Classe dont hériteront tous les sprites
    class Sprite
    {
        // Récupère ou définit l'image du sprite
        public Texture2D texture;
        public Texture2D Texture
        {
            get{return texture;}
            set { texture = value; }
        }

        // position du Sprite
        public Vector2 Position;

        //Rectangle servant à gérer les collisions
        public Rectangle Rectangle
        {
            get;
            set;
        }

        // Récupère ou définit la direction du sprite. Lorsque la direction est modifiée, elle est automatiquement normalisée.
        private Vector2 direction;
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = Vector2.Normalize(value); }
        }
        
        // Récupère ou définit la vitesse de déplacement du sprite.
        public float Speed
        {
            get;set;
        }


        /// <summary>
        /// Initialise les variables du Sprite
        /// </summary>
        public virtual void Initialize()
        {
            Position = Vector2.Zero;
            Direction = Vector2.Zero;
            Speed = 0;
        }

        /// <summary>
        /// Charge l'image voulue grâce au ContentManager donné
        /// </summary>
        /// <param name="content">Le ContentManager qui chargera l'image</param>
        /// <param name="assetName">L'asset name de l'image à charger pour ce Sprite</param>
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height);
        }

        /// <summary>
        /// Met à jour les variables du sprite
        /// </summary>
        /// <param name="gameTime">Le GameTime associé à la frame</param>
        public virtual void Update(GameTime gameTime)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height);
        }

        /// <summary>
        /// Dessine le sprite en utilisant ses attributs et le spritebatch donné
        /// </summary>
        /// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
        /// <param name="gameTime">Le GameTime de la frame</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Texture != null)   
                spriteBatch.Draw(Texture, Position, Color.White);

        }
        
    }
}
