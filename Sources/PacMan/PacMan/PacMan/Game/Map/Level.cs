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

        int[,] map = new int[,]{{1,1,1,1,1,1,1,1,1,1,1,1},
                                {0,0,0,0,0,0,0,0,0,0,0,1},
                                {1,1,1,1,1,1,1,1,1,1,0,1},
                                {1,1,1,1,1,1,1,1,1,1,0,1},
                                {1,1,1,1,1,1,1,1,1,1,0,1},
                                {1,1,1,0,0,0,0,0,0,0,0,1},
                                {1,1,1,0,1,1,1,1,1,1,1,1},
                                {1,1,1,0,1,1,0,0,0,0,0,0},
                                {1,1,1,0,0,0,0,1,1,1,1,1},
                                {1,1,1,1,1,1,1,1,1,1,1,1},

        };

        public int Width { get { return map.GetLength(1); } }
        public int Height { get { return map.GetLength(0); } }

        public int[,] Map { get { return this.map; } }

        private List<Texture2D> tileTextures = new List<Texture2D>();
        public List<Texture2D> TileTextures { get; set; }

        public Level()
        {
            this.startingPosition = new Vector2(0, 16);
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
    }
}
