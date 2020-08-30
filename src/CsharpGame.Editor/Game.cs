using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpGame.Editor
{
    public class Game : Engine.Base.Engine
    {
        public Game(PictureBox drawingArea) : base(drawingArea)
        {
        }

        public override bool OnCreate()
        {
            return base.OnCreate();
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            return base.OnUpdate(ElapsedTime);
        }
    }
}
