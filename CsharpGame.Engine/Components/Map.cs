using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Components
{
    public class Map
    {
        private int _Width;
        private int _Height;
        private Tile[,] _Grid;
        
        public Size TilesSize { get; set; }

        public int Width { get => _Width; set => _Width = value; }
        public int Height { get => _Height; set => _Height = value; }
        public Tile[,] Grid { get => _Grid; }

        public Map(int width, int height)
        {
            _Width = width;
            _Height = height;
            TilesSize = new Size(16, 16);
        }

        /// <summary>
        /// Generate default map from a string
        /// </summary>
        public void GenerateDefaultMap()
        {
            string[,] map = new string[Width, Height];
            for (int x = 0; x < Width ; x++)
            {
                for (int y = 0; y < Height; y++)
                {

                    if (y == Height - 1)
                        map[x,y] = "G";
                    else
                        map[x,y] = ".";
                }
            }
            GenerateFromStringArray(map);
        }

        /// <summary>
        /// Generate map tiles from string
        /// . for empty space
        /// G for ground tile
        /// </summary>
        /// <param name="map"></param>
        public virtual void GenerateFromStringArray(string[,] map)
        {
            _Grid = new Tile[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Sprite sprite = new Sprite(TilesSize.Width, TilesSize.Height);
                    bool Ground = false;
                    switch (map[x,y])
                    {
                        case ".":
                            sprite.LoadFromFile(Engine.Resource.sky);
                            break;
                        case "G":
                            sprite.LoadFromFile(Engine.Resource.tile_1);
                            Ground = true;
                            break;
                        default:
                            sprite.LoadFromFile(Engine.Resource.sky);
                            break;
                    }
                    _Grid[x, y] = new Tile(new System.Drawing.PointF(x * sprite.Width, y * sprite.Height), sprite) { Ground = Ground };
                }
            }
        }

        /// <summary>
        /// Get Tile from grid with screnn position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTile(float x, float y)
        {
            int X = (int)x / TilesSize.Width;
            int Y = (int)y / TilesSize.Height;
            if (X >= 0 && X < Width && Y >= 0 && Y < Height)
                return Grid[X, Y];

            return null;
        }

        /// <summary>
        /// Get Tile from grid with grid positions
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return Grid[x, y];

            return null;
        }

    }
}
