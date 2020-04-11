using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpGame.Engine.Base;
using CsharpGame.Engine.Physics;
using CsharpGame.Engine.Platformer;

namespace CsharpGame.Test
{
    public class Game : Core
    {
        public Game(PictureBox pictureBox) : base(pictureBox) { this.DisplayFPS = true; this.CalculeFPS = true; }
        //CustomPlatform Platfromer;
        SpriteSheet Run1;
        SpriteSheet Run2;
        GameObject Girl;
        public override bool OnCreate()
        {
            //Platfromer = new CustomPlatform(this);
            Run1 = new SpriteSheet("run", 125, 125, 3, 1, 16, Test.Properties.Resources.run_girl);
            Run2 = new SpriteSheet("stand", 125, 125, 3, 15, 16, Test.Properties.Resources.run_girl);
            Girl = new GameObject(new System.Drawing.PointF(100, 50), Run1);
            Girl.Animations.RegisterAnimation(Run1);
            Girl.Animations.RegisterAnimation(Run2);
            RegisterGameObject(Girl);
            return true;
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            //Platfromer.Run((float)ElapsedTime);
            if (KeyClicked(Keys.A))
                Girl.SetAnimation("run");
            if (KeyClicked(Keys.Z))
                Girl.SetAnimation("stand");
            return true;
        }
    }
}
