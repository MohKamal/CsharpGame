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
        Sprite player;
        public CustomPlatform(Core engine) : base(engine)
        {
            RegisterGrid(new PlatformerGrid(engine.ScreenWith() / 16, engine.ScreenHeight() / 16, 16));
            player = new Sprite(Grid.Resolution, Grid.Resolution);
            player.LoadFromFile(Test.Properties.Resources.men_1);
            Character = new PlatformCharacter(new System.Drawing.PointF(PLAYER_SPAWN_X, PLAYER_SPAWN_Y), player);
            Grid.addNode(Character);
            ShowGrid = false;
        }

        public override bool Update(float ElapsedTime)
        {
            Engine.Drawer.String("Use Keyborad to move", "Arial", 8, System.Drawing.Color.Black, new System.Drawing.PointF(1, 1));
            Engine.Drawer.String("Use mouse to draw lines", "Arial", 8, System.Drawing.Color.Black, new System.Drawing.PointF(1, 10));
            return true;
        }
    }
}
