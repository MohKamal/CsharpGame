using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Platformer
{
    public class PlatformerNode : GameObject
    {
        private bool _onGround;

        public PointF MaxPosition { get; set; }
        public bool onGround { get { return _onGround; } set { _onGround = value; } }
        public PlatformerNode(PointF position, Sprite sprite) : base(position, sprite)
        {
            _onGround = false;
        }

        public void SetVelocityX(float vx)
        {
            Velocity = new PointF(vx, Velocity.Y);
        }

        public void SetVelocityY(float vy)
        {
            Velocity = new PointF(Velocity.X, vy);
        }

        public List<float> getXCells(int resolution, float epsilon)
        {
            List<float> cells = new List<float>();

            float start = (float)Math.Floor((double)((Position.X + epsilon) / resolution));
            float end = (float)Math.Floor((double)((Position.X + Sprite.Width - epsilon) / resolution));
            cells.Add(start);
            cells.Add(end);
            return cells;
        }

        public List<float> getYCells(int resolution, float epsilon)
        {
            List<float> cells = new List<float>();

            float start = (float)Math.Floor((double)((Position.Y+ epsilon) / resolution));
            float end = (float)Math.Floor((double)((Position.Y + Sprite.Height - epsilon) / resolution));
            cells.Add(start);
            cells.Add(end);
            return cells;
        }

        public float getCellBottom(int resolution, float epsilon, float y)
        {
            return (float)Math.Floor((double)(y + Sprite.Height - epsilon) / resolution);
        }

        public float getCellTop(int resolution, float epsilon, float y)
        {
            return (float)Math.Floor((double)(y + epsilon) / resolution);
        }

        public float getCellRight(int resolution, float epsilon, float x)
        {
            return (float)Math.Floor((double)(x + Sprite.Width - epsilon) / resolution);
        }

        public float getCellLeft(int resolution, float epsilon, float x)
        {
            return (float)Math.Floor((double)(x + epsilon) / resolution);
        }

        public void collideCellBottom(int resolution, float epsilon, float y)
        {
            _onGround = true;
            Velocity = new PointF(Velocity.X, 0);
            Position = new PointF(Position.X,
                getCellBottom(resolution, epsilon, y) * resolution - Sprite.Height
                );
        }

        public void collideCellTop(int resolution, float epsilon, float y)
        {
            _onGround = true;
            Velocity = new PointF(Velocity.X, 0);
            Position = new PointF(Position.X,
                getCellTop(resolution, epsilon, y) * resolution
                );
        }

        public void collideCellRight(int resolution, float epsilon, float x)
        {
            _onGround = true;
            Velocity = new PointF(0, Velocity.Y);
            Position = new PointF(getCellRight(resolution, epsilon, x) * resolution - Sprite.Width,
                Position.Y);
        }

        public void collideCellLeft(int resolution, float epsilon, float x)
        {
            _onGround = true;
            Velocity = new PointF(0, Velocity.Y);
            Position = new PointF(getCellRight(resolution, epsilon, x) * resolution,
                Position.Y);
        }

        public void limitXSpeed(float ElapsedTime, float epsilon)
        {
            if (Velocity.X * ElapsedTime < -Sprite.Width + epsilon)
                Velocity = new PointF((-Sprite.Width+ epsilon) / ElapsedTime,
                    Velocity.Y);

            if (Velocity.X * ElapsedTime > Sprite.Width - epsilon)
                Velocity = new PointF((Sprite.Width - epsilon) / ElapsedTime,
                    Velocity.Y);
        }

        public void limitYSpeed(float ElapsedTime, float epsilon)
        {
            if (Velocity.Y * ElapsedTime < -Sprite.Height + epsilon)
                Velocity = new PointF(Velocity.X,
                    (-Sprite.Height + epsilon) / ElapsedTime);

            if (Velocity.Y * ElapsedTime > Sprite.Height - epsilon)
                Velocity = new PointF(Velocity.X,
                    (Sprite.Height - epsilon) / ElapsedTime);
        }

    }
}
