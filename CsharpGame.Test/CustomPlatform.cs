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
            Engine.Drawer.String("Use Keyborad to move", "Arial", 20, System.Drawing.Color.Black, new System.Drawing.PointF(10, 100));
            Engine.Drawer.String("Use mouse to draw lines", "Arial", 20, System.Drawing.Color.Black, new System.Drawing.PointF(10, 150));
            return true;
        }
    }
}
