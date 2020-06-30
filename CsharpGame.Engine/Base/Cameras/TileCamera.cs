using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base.Cameras
{
    public class TileCamera : Camera
    {
        public PointF Location { get; set; }
        public TileCamera(int screenWidth, int screenHeight, int levelWidth, int levelHeight, float speed = 10) : base(screenWidth, screenHeight, levelWidth, levelHeight, speed)
        {
            Position = new PointF(0, 0);
            Location = new PointF(0, 0);
        }

        /// <summary>
        /// Set the position to a gameObject
        /// </summary>
        /// <param name="object"></param>
        public override void SetPositionTo(GameObject @object)
        {
            Location = @object.Position;
            getOffset();
        }

        /// <summary>
        /// Get the camera offset to set the world movements
        /// </summary>
        /// <returns></returns>
        public override PointF getOffset()
        {
            Offset = new PointF((CameraSize.Width / 2) - Location.X, (CameraSize.Height / 2) - Location.Y);
            Checkoffset();
            return Offset;
        }

        /// <summary>
        /// Compard the camera location to the level size, so the camera stay's inside the level
        /// </summary>
        private void Checkoffset()
        {
            // Clamp camera to game boundaries
            if (Location.X < 0) Location = new PointF(0, Location.Y);
            if (Location.Y < 0) Location = new PointF(Location.X, 0);
            if (Location.X > LayoutSize.Width - CameraSize.Width) Location = new PointF(LayoutSize.Width - CameraSize.Width, Location.Y);
            if (Location.Y > LayoutSize.Height - CameraSize.Height) Location = new PointF(Location.X, LayoutSize.Height - CameraSize.Height);
        }
    }
}
