using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class LogicTile : Tile
    {
     
        public bool Movable { get; set; }
        public LogicTile(Size size) : base(size)
        {
            Movable = true;
        }
    }
}
