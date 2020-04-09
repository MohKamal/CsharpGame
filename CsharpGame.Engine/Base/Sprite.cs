using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    /// <summary>
    /// Sprite for holding game graphics
    /// </summary>
    public class Sprite
    {
        public float Width { get; set; }
        public float Height { get; set; }

        /// <summary>
        /// The sprite picture
        /// </summary>
        private Image _Graphic;
        public Image Graphic { get { return _Graphic; } }

        public Sprite(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Loading sprite image from file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool LoadFromFile(string file)
        {
            try
            {
                _Graphic = Image.FromFile(file);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loading sprite error : {ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// Loading sprite image from ressources
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool LoadFromFile(Bitmap file)
        {
            try
            {
                _Graphic = (Image)file;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loading sprite error : {ex.Message}");
            }
            return false;
        }
    }
}
