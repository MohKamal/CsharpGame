using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpGame.Engine.Base;
using CsharpGame.Engine.Components;
using CsharpGame.Engine.Physics;

namespace CsharpGame.Test
{
    public class Game : Core
    {
        public Game(PictureBox pictureBox) : base(pictureBox) { this.DisplayFPS = true; this.CalculeFPS = true; }

        Sprite Sprite;
        GameObject Player;
        Physics physics;
        Map map;
        public override bool OnCreate()
        {
            Sprite = new Sprite(16, 16);
            Sprite.LoadFromFile(Test.Properties.Resources.men_1);
            Player = new GameObject(new System.Drawing.PointF(10, 10), Sprite);
            physics = new Physics(this);
            physics.Gravity = 10.0f;

            map = new Map(this.ScreenWith() / 16, this.ScreenHeight() / 16);
            map.GenerateDefaultMap();

            this.RegisterGameObject(Player);
            this.RegisterMap(map);
            return true;
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            if (IsClicking)
            {
                if (KeyboardKey == Keys.Right)
                {
                    Player.Velocity = new System.Drawing.PointF(Player.Velocity.X + (25f * (float)ElapsedTime), Player.Velocity.Y);
                }
                else if (KeyboardKey == Keys.Left)
                {
                    Player.Velocity = new System.Drawing.PointF(Player.Velocity.X +  + (-25f * (float)ElapsedTime), Player.Velocity.Y);
                }
                else if (KeyboardKey == Keys.Up)
                {
                    if(Player.Velocity.Y == 0)
                        Player.Velocity = new System.Drawing.PointF(Player.Velocity.X, Player.Velocity.Y + (-15f * (float)ElapsedTime));
                }
                else if (KeyboardKey == Keys.Down)
                {
                    Player.Velocity = new System.Drawing.PointF(Player.Velocity.X, Player.Velocity.Y + (5f * (float)ElapsedTime));
                }
            }

            Player.Move();
            physics.ApplyPhysiques(ElapsedTime);
            Drawer.GameObject(Player);

            return true;
        }
    }
}
