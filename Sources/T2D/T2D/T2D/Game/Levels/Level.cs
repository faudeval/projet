﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace T2D
{
    class Level
    {

        private Queue<Vector2> waypoints = new Queue<Vector2>();

        int[,] map = new int[,]{{0,0,1,0,0,0,0,0,0,1,0},
                                {0,0,1,1,0,0,0,0,0,1,0},
                                {0,0,0,1,1,0,0,0,0,1,0},
                                {0,0,0,0,1,0,0,0,0,1,0},
                                {0,0,0,1,1,0,0,0,0,1,0},
                                {0,0,1,1,0,0,0,0,0,1,0},
                                {0,0,1,0,0,0,0,0,0,1,0},
                                {0,0,1,1,1,1,1,1,1,1,0},
                                {0,0,0,0,0,0,0,0,0,0,0},
        };

        public int Width { get { return map.GetLength(1); } }
        public int Height { get { return map.GetLength(0); } }
        public Queue<Vector2> Waypoints { get { return this.waypoints; } }


        public List<Texture2D> tileTextures = new List<Texture2D>();

        public Level()
        {
            waypoints.Enqueue(new Vector2(2, 0) * 32);
            waypoints.Enqueue(new Vector2(2, 1) * 32);
            waypoints.Enqueue(new Vector2(3, 1) * 32);
            waypoints.Enqueue(new Vector2(3, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 4) * 32);
            waypoints.Enqueue(new Vector2(3, 4) * 32);
            waypoints.Enqueue(new Vector2(3, 5) * 32);
            waypoints.Enqueue(new Vector2(2, 5) * 32);
            waypoints.Enqueue(new Vector2(2, 7) * 32);
            waypoints.Enqueue(new Vector2(9, 7) * 32);
            waypoints.Enqueue(new Vector2(9, 0) * 32);
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
                    batch.Draw(texture, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                }
            }
        }

        
        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width || cellY < 0 || cellY > Height)
                return 0;

            return map[cellY, cellX];
        }
    }
}
