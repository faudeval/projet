using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan
{
    class Level
    {
        public const int TILE_HEIGHT = 16;
        public const int TILE_WIDTH = 16;

        protected Vector2 startingPosition;
        public Vector2 StartingPosition { get { return this.startingPosition; } set { this.startingPosition = value; } }

        public Vector2[] Teleport = new Vector2[2] {new Vector2(0, 9*TILE_HEIGHT), new Vector2(18*TILE_WIDTH, 9*TILE_HEIGHT)};

        int[,] map = new int[,]{{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
                                {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                {1,0,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,0,1},
                                {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
                                {1,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1},
                                {1,1,1,1,0,1,0,0,0,0,0,0,0,1,0,1,1,1,1},
                                {1,1,1,1,0,1,0,1,1,2,1,1,0,1,0,1,1,1,1},
                                {0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},
                                {1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1},
                                {1,1,1,1,0,1,0,0,0,0,0,0,0,1,0,1,1,1,1},
                                {1,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1},
                                {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
                                {1,0,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,0,1},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
                                {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
                                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        public int Width { get { return map.GetLength(1); } }
        public int Height { get { return map.GetLength(0); } }

        public int[,] Map { get { return this.map; } }

        private List<Texture2D> tileTextures = new List<Texture2D>();
        public List<Texture2D> TileTextures { get; set; }

        public Level()
        {
            this.startingPosition = new Vector2(9*TILE_WIDTH, 15*TILE_HEIGHT);
        }

        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];
                    batch.Draw(texture, new Rectangle(x * TILE_WIDTH, y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT), Color.White);
                }
            }
        }


        /// <summary>
        /// Méthode vérifiant si une position est dans le décor
        /// </summary>
        /// <param name="position">Vector2 représentant la position à vérifier</param>
        /// <returns>Faux si la position (et la surface associée) est totalement dans le chemin, Vrai si elle sort du niveau ou du chemin</returns>
        public bool IsOut(Vector2 position, bool isPacman)
        {
            int top = (int)((int)position.Y / TILE_HEIGHT);
            int left = (int)((int)position.X / TILE_WIDTH);
            int bot = (int)(((int)position.Y + TILE_HEIGHT - 1) / TILE_HEIGHT);
            int right = (int)(((int)position.X + TILE_WIDTH - 1) / TILE_WIDTH);
            
            if (top < 0 || left < 0 || right >= Width || bot >= Height) // si un des bords est hors du niveau
                return true;

            bool isPorte = false;
            if (isPacman) // Si c'est Pacman, il ne peut pas passer par la porte
                isPorte = (Map[top, left] == 2 ||
                    Map[top, right] == 2 ||
                    Map[bot, left] == 2 ||
                    Map[bot, right] == 2);

            return (isPorte ||
                    Map[top, left] == 1 ||
                    Map[top, right] == 1 ||
                    Map[bot, left] == 1 ||
                    Map[bot, right] == 1); // True si l'un des côtés est dans le décor, false sinon
        }

        /*
        public bool seePath(Vector2 position, Vector2 direction, bool front = true)
        {
            bool ret = false;

            int top = (int)((int)position.Y / TILE_HEIGHT);
            int left = (int)((int)position.X / TILE_WIDTH);
            int bot = (int)(((int)position.Y + TILE_HEIGHT - 1) / TILE_HEIGHT);
            int right = (int)(((int)position.X + TILE_WIDTH - 1) / TILE_WIDTH);

            return ret;
        }*/
    }
}
