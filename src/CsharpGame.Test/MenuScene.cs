using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Test
{
    public class MenuScene : Scene
    {
        public MenuScene(Engine.Base.Engine engine) : base("Menu", engine)
        {
        }

        public bool  Done { get; set; }

        public override bool OnCreate()
        {
            Done = true;
            return base.OnCreate();
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            Engine.Drawer.String("This is a menu", "Arial", 48, Color.Red, new PointF(100, 100));
            Engine.Drawer.String("Clique Space to go out of menu", "Arial", 18, Color.Red, new PointF(100, 160));

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Space))
            {
                Ended();
            }
            return Done;
        }
    }
}
