using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Platformer
{
    public class PlatformCharacter : PlatformerNode
    {
        public PlatformCharacter(PointF position, Sprite sprite) : base(position, sprite)
        {
        }
    }
}
