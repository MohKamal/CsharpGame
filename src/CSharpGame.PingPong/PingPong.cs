using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpGame.PingPong
{
    public class PingPong : Engine
    {
        public PingPong(PictureBox drawingArea) : base(drawingArea) { DisplayFPS = true; CalculeFPS = true; }

        public Sprite PlayerSprite { get; private set; }
        public Sprite _PlayerSprite { get; private set; }
        public Sprite BallSprite { get; private set; }

        public GameObject Player { get; private set; }
        public GameObject _Player { get; private set; }
        public GameObject Ball { get; private set; }

        int Score = 0;

        private Random Rnd { get; set; }
        int ballSpeedX = 0;
        int ballSpeedY = 0;


        int Red = 0;
        int Blue = 0;
        int Green = 0;
        public override bool OnCreate()
        {
            PlayerSprite = new Sprite(30, 100);
            PlayerSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.bar);
            _PlayerSprite = new Sprite(30, 100);
            _PlayerSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.bar);
            BallSprite = new Sprite(30, 30);
            BallSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.ball);
            Player = new GameObject(new PointF(10, 120), PlayerSprite);
            _Player = new GameObject(new PointF(100, 120), _PlayerSprite);
            Ball = new GameObject(new PointF(120, 120), BallSprite);
            Rnd = new Random();
            ballSpeedX = Rnd.Next(-10, 10);
            ballSpeedY = Rnd.Next(-10, 10);
            //This will draw the game objects to screnn without using the draw methor in the OnUpdate function
            this.Scenes[0].Layers[0].RegisterGameObject(Player);
            this.Scenes[0].Layers[0].RegisterGameObject(Ball);
            Layer layer = new Layer("top");

            layer.RegisterGameObject(_Player);
            this.Scenes[0].RegisterLayer(layer);

            return true;
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            //Drawer.GameObject(Player);
            //Drawer.GameObject(Ball);

            //Simple Logic
            Player.Position = new PointF(10, MousePosition().Y);
            //Dont go behind the screen dud
            if (Player.Position.Y >= (ScreenHeight() - Player.Sprite.Height))
                Player.Position = new PointF(10, ScreenHeight() - Player.Sprite.Height);

            if (Ball.Position.X <= 0)
            {
                ballSpeedX = Rnd.Next(0, 10);
                Score--;
            }

            if (Ball.Position.X >= (ScreenWidth() - Ball.Sprite.Width))
                ballSpeedX = Rnd.Next(-10, 0);

            if (Ball.Position.Y <= 0)
                ballSpeedY = Rnd.Next(0, 10);

            if (Ball.Position.Y >= (ScreenHeight() - Ball.Sprite.Height))
                ballSpeedY = Rnd.Next(-10, 0);

            Drawer.Clear(Color.FromArgb(Red, Green, Blue));

            Ball.Position = new PointF(Ball.Position.X + ballSpeedX, Ball.Position.Y + ballSpeedY);

            //Collision
            if (Player.Position.X < Ball.Position.X + Ball.Sprite.Width &&
               Player.Position.X + Player.Sprite.Width > Ball.Position.X &&
               Player.Position.Y < Ball.Position.Y + Ball.Sprite.Height &&
               Player.Sprite.Height + Player.Position.Y > Ball.Position.Y)
            {
                ballSpeedX *= -1;
                Score++;
                Red = Rnd.Next(0, 255);
                Blue = Rnd.Next(0, 255);
                Green = Rnd.Next(0, 255);
            }

            Drawer.String($"Score : {Score}", "Arial", 10, Color.Black, new PointF((ScreenWidth() / 2) - 5, 10)); 

            return true;
        }
    }
}
