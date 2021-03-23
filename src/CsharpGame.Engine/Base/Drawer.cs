using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
            try
            {
                _DrawingArea.Image = DrawArea;
            }
            catch { }
        }

        /// <summary>
        /// Clear the frame
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color)
        {
            try
            {
                Graphic.Clear(color);
            }
            catch { }
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

            if (sprite.Graphic == null)
                return false;
            if (sprite is SpriteSheet)
                SpriteSheet(x, y, (SpriteSheet)sprite);
            try
            {
                Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)x, (int)y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(new Point(0, 0), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw Sprite with floats points and ref to a camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool Sprite(float x, float y, Sprite sprite, Camera camera)
        {
            if (sprite == null)
                return false;

            if (sprite.Graphic == null)
                return false;
            if (sprite is SpriteSheet)
                SpriteSheet(x, y, (SpriteSheet)sprite);

            camera.UpdateMaxPosition();
            if (x >= camera.Offset.X && x <= camera.MaxPosition.X && y >= camera.Offset.Y && y <= camera.MaxPosition.Y)
            {
                try
                {
                    Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)x, (int)y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(new Point(0, 0), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
                }
                catch { }
                return true;
            }
            return false;
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
            if (sprite.Graphic == null)
                return false;

            if (sprite is SpriteSheet)
                SpriteSheet(point, (SpriteSheet)sprite);
            try
            {
                Graphic.DrawImage(sprite.Graphic, point);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw Sprtie with point vec2 and ref to a camera
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool Sprite(PointF point, Sprite sprite, Camera camera)
        {
            if (sprite == null)
                return false;
            if (sprite.Graphic == null)
                return false;

            if (sprite is SpriteSheet)
                SpriteSheet(point, (SpriteSheet)sprite);

            camera.UpdateMaxPosition();
            if (point.X >= camera.Position.X && point.X <= camera.MaxPosition.X && point.Y >= camera.Position.Y && point.Y <= camera.MaxPosition.Y)
            {
                try
                {
                    Graphic.DrawImage(sprite.Graphic, point);
                }
                catch { }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draw Sprite with floats points
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool SpriteSheet(float x, float y, SpriteSheet sprite)
        {
            if (sprite == null)
                return false;

            if (sprite.Graphic == null)
            return false;
            sprite.Update();
            try
            {
                Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)x, (int)y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(sprite.Frame(), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw Sprite with floats points with a ref to a camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool SpriteSheet(float x, float y, SpriteSheet sprite, Camera camera)
        {
            if (sprite == null)
                return false;

            if (sprite.Graphic == null)
                return false;
            camera.UpdateMaxPosition();
            if (x >= camera.Position.X && x <= camera.MaxPosition.X && y >= camera.Position.Y && y <= camera.MaxPosition.Y)
            {
                sprite.Update();
                try
                {
                    Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)x, (int)y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(sprite.Frame(), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
                }
                catch { }
                return true;
            }
            return false;
        }



        /// <summary>
        /// Draw Sprtie with point vec2
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool SpriteSheet(PointF point, SpriteSheet sprite)
        {
            if (sprite == null)
                return false;
            if (sprite.Graphic == null)
                return false;
            sprite.Update();
            try
            {
                Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)point.X, (int)point.Y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(sprite.Frame(), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
            }
            catch { }
            return true;
        }


        /// <summary>
        /// Draw Sprtie with point vec2 and ref to a camera
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool SpriteSheet(PointF point, SpriteSheet sprite, Camera camera)
        {
            if (sprite == null)
                return false;
            if (sprite.Graphic == null)
                return false;
            sprite.Update();
            camera.UpdateMaxPosition();
            if ((point.X >= camera.Position.X && point.X <= camera.MaxPosition.X) && (point.Y >= camera.Position.Y && point.Y <= camera.MaxPosition.Y))
            {
                try
                {
                    Graphic.DrawImage(sprite.Graphic, new Rectangle(new Point((int)point.X, (int)point.Y), new Size((int)sprite.Width, (int)sprite.Height)), new Rectangle(sprite.Frame(), new Size((int)sprite.Width, (int)sprite.Height)), GraphicsUnit.Pixel);
                }
                catch { }
                return true;
            }
            return false;
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
            if (obj.Sprite is SpriteSheet)
            {
                (obj.Sprite as SpriteSheet).Update();
                try
                {
                    Graphic.DrawImage(obj.Sprite.Graphic, new Rectangle(new Point((int)obj.Position.X, (int)obj.Position.Y), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), new Rectangle((obj.Sprite as SpriteSheet).Frame(), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), GraphicsUnit.Pixel);
                }
                catch { }
                return true;
            }
            try
            {
                Graphic.DrawImage(obj.Sprite.Graphic, new Rectangle(new Point((int)obj.Position.X, (int)obj.Position.Y), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), new Rectangle(new Point(0, 0), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), GraphicsUnit.Pixel);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw a game object to the frame with ref to a camerz
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public bool GameObject(GameObject obj, Camera camera)
        {
            if (obj == null)
                return false;
            if (obj.Sprite is SpriteSheet)
            {
                camera.UpdateMaxPosition();
                if (obj.Position.X >= camera.Position.X && obj.Position.X <= camera.MaxPosition.X && obj.Position.Y >= camera.Position.Y && obj.Position.Y <= camera.MaxPosition.Y)
                {
                    (obj.Sprite as SpriteSheet).Update();
                    try
                    {
                        Graphic.DrawImage(obj.Sprite.Graphic, new Rectangle(new Point((int)obj.Position.X, (int)obj.Position.Y), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), new Rectangle((obj.Sprite as SpriteSheet).Frame(), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), GraphicsUnit.Pixel);
                    }
                    catch { }
                    return true;
                }
                return false;
            }
            if (obj.Position.X >= camera.Position.X && obj.Position.X <= camera.MaxPosition.X && obj.Position.Y >= camera.Position.Y && obj.Position.Y <= camera.MaxPosition.Y)
            {
                try
                {
                    Graphic.DrawImage(obj.Sprite.Graphic, new Rectangle(new Point((int)obj.Position.X, (int)obj.Position.Y), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), new Rectangle(new Point(0, 0), new Size((int)obj.Sprite.Width, (int)obj.Sprite.Height)), GraphicsUnit.Pixel);
                }
                catch { }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Draw a line
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Line(Point p1, Point p2, Color color)
        {
            Pen pen = new Pen(color);
            try
            {
                Graphic.DrawLine(pen, p1, p2);
            }
            catch { }
            return true;
        }


        /// <summary>
        /// Draw a line with a ref to a camera
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Line(Point p1, Point p2, Color color, Camera camera)
        {
            camera.UpdateMaxPosition();
            if (p1.X >= camera.Position.X && p1.Y >= camera.Position.Y && p2.X <= camera.MaxPosition.X && p2.Y <= camera.MaxPosition.Y)
            {
                Pen pen = new Pen(color);
                try
                {
                    Graphic.DrawLine(pen, p1, p2);
                }
                catch { }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draw rectangle, if fill is true, it will be a solid rectangle with the selected color
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="fill"></param>
        /// <returns></returns>
        public bool Rectangle(float x, float y, float with, float height, Color color, bool fill=false)
        {
            if (!fill)
            {
                Pen pen = new Pen(color);
                try
                {
                    Graphic.DrawRectangle(pen, x, y, with, height);
                }
                catch { }
            }
            else
            {
                SolidBrush solidBrush = new SolidBrush(color);
                try
                {
                    Graphic.FillRectangle(solidBrush, x, y, with, height);
                }
                catch { }
            }
            return true;
        }

        /// <summary>
        /// Draw rectangle, if fill is true, it will be a solid rectangle with the selected color, and using a ref camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="fill"></param>
        /// <returns></returns>
        public bool Rectangle(float x, float y, float with, float height, Color color, Camera camera, bool fill = false)
        {
            if (x >= camera.Position.X && y >= camera.Position.Y && x <= camera.MaxPosition.X && y <= camera.MaxPosition.Y)
            {
                if (!fill)
                {
                    Pen pen = new Pen(color);
                    try
                    {
                        Graphic.DrawRectangle(pen, x, y, with, height);
                    }
                    catch { }
                }
                else
                {
                    SolidBrush solidBrush = new SolidBrush(color);
                    try
                    {
                        Graphic.FillRectangle(solidBrush, x, y, with, height);
                    }
                    catch { }
                }
                return true;
            }
            return false;
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
            try
            {
                Graphic.DrawString(text, font, brush, position);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw a string to screen witha ref to a camera
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontfamily"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool String(string text, string fontfamily, float size, Color color, PointF position, Camera camera)
        {
            camera.UpdateMaxPosition();
            if (position.X >= camera.Position.X && position.Y >= camera.Position.Y && position.X <= camera.MaxPosition.X && position.Y <= camera.MaxPosition.Y)
            {
                SolidBrush brush = new SolidBrush(color);
                Font font = new Font(fontfamily, size);
                try
                {
                    Graphic.DrawString(text, font, brush, position);
                }
                catch { }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draw circle (ellipse)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Circle(float x, float y, float with, float height, Color color)
        {
            Pen pen = new Pen(color);
            try
            {
                Graphic.DrawEllipse(pen, x, y, with, height);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw circle (ellipse)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Circle(PointF position, float with, float height, Color color)
        {
            Pen pen = new Pen(color);
            try
            {
                Graphic.DrawEllipse(pen, position.X, position.Y, with, height);
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Draw circle (ellipse) with ref to camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Circle(float x, float y, float with, float height, Color color, Camera camera)
        {
            if (x >= camera.Position.X && y >= camera.Position.Y && x <= camera.MaxPosition.X && y <= camera.MaxPosition.Y)
            {
                Pen pen = new Pen(color);
                try
                {
                    Graphic.DrawEllipse(pen, x, y, with, height);
                }
                catch { }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draw circle (ellipse) with ref to camera
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool Circle(PointF position, float with, float height, Color color, Camera camera)
        {
            if (position.X >= camera.Position.X && position.Y >= camera.Position.Y && position.X <= camera.MaxPosition.X && position.Y <= camera.MaxPosition.Y)
            {
                Pen pen = new Pen(color);
                try
                {
                    Graphic.DrawEllipse(pen, position.X, position.Y, with, height);
                }
                catch { }
                return true;
            }
            return false;
        }
    }
}
