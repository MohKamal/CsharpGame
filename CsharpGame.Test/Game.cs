using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpGame.Engine.Base;
using CsharpGame.Engine.Components;
using CsharpGame.Engine.Physics;
using CsharpGame.Engine.Platformer;

namespace CsharpGame.Test
{
    public class Game : Core
    {
        public Game(PictureBox pictureBox) : base(pictureBox) { this.DisplayFPS = false; this.CalculeFPS = false; }

        CustomPlatform Platfromer;

        public override bool OnCreate()
        {
            Platfromer = new CustomPlatform(this);
            return true;
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            Platfromer.Run((float)ElapsedTime);

            return true;
        }
    }
}
