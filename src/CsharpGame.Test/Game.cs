using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpGame.Engine.Base;
using CsharpGame.Engine.MapTile.Simple;
using CsharpGame.Engine.Physics;
using CsharpGame.Engine.Platformer;

namespace CsharpGame.Test
{
    public class Game : Engine.Base.Engine
    {
        public Game(PictureBox pictureBox) : base(pictureBox) { this.DisplayFPS = true; this.CalculeFPS = true; }
        //CustomPlatform Platfromer;
        TileGame TileGame;
        MenuScene Menu;
        public override bool OnCreate()
        {
            //Platfromer = new CustomPlatform(this);
            //TileGame = new TileGame(this);
            Menu = new MenuScene(this);
            RegisterScene(Menu);
            return true;
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            //Platfromer.Run((float)ElapsedTime);
            //TileGame.Run((float)ElapsedTime);
            return true;
        }
    }
}
