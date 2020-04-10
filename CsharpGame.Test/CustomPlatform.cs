using CsharpGame.Engine.Base;
using CsharpGame.Engine.Platformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Test
{
    public class CustomPlatform : PlatformGame
    {
        public CustomPlatform(Core engine) : base(engine)
        {
        }

        public override bool Update(float ElapsedTime)
        {
            Engine.Drawer.String("This is an overrided function", "Arial", 20, System.Drawing.Color.Black, new System.Drawing.PointF(10, 100));
            return true;
        }
    }
}
