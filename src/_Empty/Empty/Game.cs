using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empty
{
    public class Game : Engine
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
