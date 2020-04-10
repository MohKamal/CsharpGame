using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Platformer
{
    public class PlatformerGridCell
    {
        public bool Wall { get; set; }
        public bool  Celling { get; set; }

        public PlatformerGridCell()
        {
            Wall = false;
            Celling = false;
        }
    }
}
