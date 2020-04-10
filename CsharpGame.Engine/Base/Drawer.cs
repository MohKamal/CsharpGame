using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using CsharpGame.Engine.Components;

namespace CsharpGame.Engine.Base
{
    public class Drawer
    {
        private Graphics Graphic;
        private PictureBox _DrawingArea;
        private Bitmap DrawArea;
        public Drawer(PictureBox drawArea)
        {
            if (drawArea != null)
            {
                _DrawingArea = drawArea;
                DrawArea = new Bitmap(_DrawingArea.Size.Width, _DrawingArea.Size.Height);
                Graphic = Graphics.FromImage(DrawArea);
            }
        }

        /// <summary>
        /// Set picturebox image to current frame
        /// </summary>
        public void ToScreen()
        {
            _DrawingArea.Image = DrawArea;
        }

        /// <summary>
        /// Clear the frame
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color)
        {
            Graphic.Clear(color);
        }

        /// <summary>
        /// Draw Sprite with floats points
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool Sprite(float x, float y, Sprite sprite)
        {
            if (sprite == null)
                return false;

            Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)x,(int)y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(new Point(0,0), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
            return true;
        }

        /// <summary>
        /// Draw Sprtie with point vec2
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool Sprite(PointF point, Sprite sprite)
        {
            if (sprite == null)
                return false;

            Graphic.DrawImage(sprite.Graphic, point);
            return true;
        }

        /// <summary>
        /// Draw a game object to the frame
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool GameObject(GameObject obj)
        {
            if (obj == null)
                return false;

            Graphic.DrawImage(obj.Sprite.Graphic, new Rectangle(new Point((int)obj.Position.X, (int)obj.Position.Y), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), new Rectangle(new Point(0,0), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), GraphicsUnit.Pixel);
            return true;
        }

        public bool Line(Point p1, Point p2, Color color)
        {
            Pen pen = new Pen(color);
            Graphic.DrawLine(pen, p1, p2);
            return true;
        }

        public bool Rectangle(float x, float y, float with, float height, Color color)
        {
            SolidBrush solidBrush = new SolidBrush(color);
            Graphic.FillRectangle(solidBrush, x, y, with, height);
            return true;
        }

        /// <summary>
        /// Draw a map of tiles
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool Map(Map map)
        {
            if (map == null)
                return false;

            for (int x= 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    Graphic.DrawImage(map.Grid[x, y].Sprite.Graphic, new Rectangle(new Point((int)map.Grid[x, y].Position.X, (int)map.Grid[x, y].Position.Y),
                        new Size((int)map.Grid[x, y].Sprite.Width, (int)map.Grid[x, y].Sprite.Height)), new Rectangle(new Point(0, 0), new Size((int)map.Grid[x, y].Sprite.Width, (int)map.Grid[x, y].Sprite.Height)), GraphicsUnit.Pixel);
                }
            }

            return true;
        }

        /// <summary>
        /// Draw a string to screen
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontfamily"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool String(string text, string fontfamily, float size , Color color, PointF position)
        {
            SolidBrush brush = new SolidBrush(color);
            Font font = new Font(fontfamily, size);
            Graphic.DrawString(text, font, brush, position);
            return true;
        }
    }
}
