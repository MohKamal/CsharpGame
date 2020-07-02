using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class Layout
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public Size Size { get; set; }
        public Size TilesSize { get; set; }
        public Tile[,] Grid { get; set; }

        public bool LogicLayout { get; set; }

        public LogicTile[,] LogicGrid { get; set; }
        
        public Layout(string name, Size size, Size tilesSize)
        {
            Name = name;
            Size = size;
            TilesSize = tilesSize;
            Grid = new Tile[Size.Width, Size.Height];
            LogicGrid = new LogicTile[Size.Width, Size.Height];
            LogicLayout = false;
            GenerateTileGrid();
        }

        // Instantiate random number generator.  
        private readonly Random _random = new Random();
        /// <summary>
        /// Generate a map using only code
        /// </summary>
        public virtual void GenerateTileGrid()
        {
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    Tile tile = new Tile(TilesSize) { Position = new Point(x, y), Texture = new Base.Sprite(TilesSize.Width, TilesSize.Height) };
                    tile.Texture.LoadFromFile(CsharpGame.Engine.Resource.sky);
                    //if (_random.Next(0, 20) < 11)
                    //    tile.Texture.LoadFromFile(CsharpGame.Engine.Resource.sky);
                    //else
                    //    tile.Texture.LoadFromFile(CsharpGame.Engine.Resource.tile);
                    Grid[x, y] = tile;
                    
                }
            }

            foreach (Tile node in Grid)
                node.ConnectClosestNodes(Grid);
        }

        /// <summary>
        /// Generate a map using only code
        /// </summary>
        public virtual void GenerateLogicTileGrid()
        {
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    LogicTile tile = new LogicTile(TilesSize) { Position = new Point(x, y) };
                    LogicGrid[x, y] = tile;
                }
            }
        }
    }
}
