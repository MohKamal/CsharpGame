using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class Tile
    {
        public Point Position { get; set; }
        public Sprite Texture { get; set; }
        public Size Size { get; }

        public Tile(Size size) {
            Size = size;
        }
    }
}
