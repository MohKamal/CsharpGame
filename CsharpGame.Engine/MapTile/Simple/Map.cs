using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class Map
    {
        public Size Size { get; set; }
        public string Name { get; set; }
        public Tile[,] Grid { get; set; }
        public Size TilesSize { get; set; }

        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        public Map(string name, Size size, Size tilesSize)
        {
            Name = name;
            Size = size;
            Grid = new Tile[size.Width, size.Height];
            TilesSize = tilesSize;
            GenerateMap();
        }

        /// <summary>
        /// Generate a map using only code
        /// </summary>
        public virtual void GenerateMap()
        {
            for(int x=0; x < Size.Width; x++)
            {
                for(int y=0; y < Size.Height; y++)
                {
                    Tile tile = new Tile(TilesSize) { Position = new Point(x, y), Texture = new Base.Sprite(TilesSize.Width, TilesSize.Height) };
                    if(_random.Next(0, 20) < 11)
                        tile.Texture.LoadFromFile(CsharpGame.Engine.Resource.sky);
                    else
                        tile.Texture.LoadFromFile(CsharpGame.Engine.Resource.tile);
                    Grid[x, y] = tile;
                }
            }
        }
    }
}
