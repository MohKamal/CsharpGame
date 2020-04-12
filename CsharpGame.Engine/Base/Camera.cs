using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    public class Camera
    {
        public PointF Position { get; set; }

        public PointF Offset { get; set; }
        public PointF TileOffset { get; set; }

        public Size VisibleArea { get; set; }

        public Size TileSize { get; set; }

        private Size ScreenSize { get; set; }

        public Size LevelSize { get; set; }

        public Camera(int screenWidth, int screenHeight, int tileWidth, int tileHeight, int levelWidth, int levelHeight)
        {
            ScreenSize = new Size(screenWidth, screenHeight);
            TileSize = new Size(tileWidth, tileHeight);
            LevelSize = new Size(levelWidth, levelHeight);
            CalculeVisibleArea();
            CalculeOffset();
        }

        public Size CalculeVisibleArea()
        {
            VisibleArea = new Size(ScreenSize.Width / TileSize.Width, ScreenSize.Height / TileSize.Height);
            return VisibleArea;
        }

        public PointF CalculeOffset()
        {
            Offset = new PointF(Position.X - VisibleArea.Width / 2, Position.Y - VisibleArea.Height / 2);
            // Clamp camera to game boundaries
            if (Offset.X < 0) Offset = new PointF(0, Offset.Y);
            if (Offset.Y < 0) Offset = new PointF(Offset.X, 0);
            if (Offset.X > LevelSize.Width - VisibleArea.Width) Offset = new PointF(LevelSize.Width - VisibleArea.Width, Offset.Y);
            if (Offset.Y > LevelSize.Height - VisibleArea.Height) Offset = new PointF(Offset.X, LevelSize.Height - VisibleArea.Height);
            return Offset;
        }

        public PointF CalculeTileOffset()
        {
            TileOffset = new PointF((Offset.X - (int)Offset.X) * TileSize.Width, (Offset.Y - (int)Offset.Y) * TileSize.Height);
            return TileOffset;
        }

        public void SetPositionTo(GameObject @object)
        {
            Position = @object.Position;
        }
    }
}
