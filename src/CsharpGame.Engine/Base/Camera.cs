using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    /// <summary>
    /// Base camera object
    /// </summary>
    public class Camera
    {
        public PointF Position { get; set; }

        public PointF Offset { get; set; }

        public Size CameraSize { get; set; }

        private PointF _MaxPosition;
        public PointF MaxPosition { get { return _MaxPosition; } }

        private Size ScreenSize { get; set; }

        public Size LayoutSize { get; set; }

        public float Speed { get; set; }

        public Camera(int screenWidth, int screenHeight, int levelWidth, int levelHeight, float speed=10)
        {
            ScreenSize = new Size(screenWidth, screenHeight);
            LayoutSize = new Size(levelWidth, levelHeight);
            CameraSize = new Size(screenWidth, screenHeight);
            Speed = speed;
            UpdateMaxPosition();
            getOffset();
        }

        public virtual void UpdateMaxPosition()
        {
            _MaxPosition = new PointF(Position.X + CameraSize.Width, Position.Y + CameraSize.Height);
        }

        public virtual PointF getOffset()
        {
            Offset = new PointF((CameraSize.Width / 2) - Position.X, (CameraSize.Height / 2) - Position.Y);
            return Offset;
        }

        public virtual void SetPositionTo(GameObject @object)
        {
            PointF BordersX = new PointF(Position.X + @object.Sprite.Width, Position.X + (CameraSize.Width - (@object.Sprite.Width + 10)));
            PointF BordersY = new PointF(Position.Y + @object.Sprite.Height, Position.Y + (CameraSize.Height - (@object.Sprite.Height + 10)));

            if (@object.Position.X < BordersX.X)
            {
                if (Position.X > @object.Position.X - (CameraSize.Width / 2))
                    Position = new PointF(Position.X - Speed, Position.Y);
            }

            if (@object.Position.X > BordersX.Y)
            {
                if (Position.X < @object.Position.X - (CameraSize.Width / 2))
                    Position = new PointF(Position.X + Speed, Position.Y);
            }

            if (@object.Position.Y < BordersY.X)
            {
                if (Position.Y > @object.Position.Y - (CameraSize.Height / 2))
                    Position = new PointF(Position.X, Position.Y - Speed);
            }

            if (@object.Position.Y > BordersY.Y)
            {
                if (Position.Y < @object.Position.Y - (CameraSize.Height / 2))
                    Position = new PointF(Position.X, Position.Y + Speed);
            }

            getOffset();
            //Checkoffset();
        }
    }
}
