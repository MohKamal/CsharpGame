using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    public class Animation
    {
        private List<SpriteSheet> _Animations;

        public Animation()
        {
            _Animations = new List<SpriteSheet>();
        }

        /// <summary>
        /// Add new animation
        /// </summary>
        /// <param name="spriteSheet"></param>
        /// <returns></returns>
        public bool  RegisterAnimation(SpriteSheet spriteSheet)
        {
            if (spriteSheet == null)
                return false;

            if (spriteSheet.Graphic == null)
                return false;

            if (_Animations.Contains(spriteSheet))
                return false;
            _Animations.Add(spriteSheet);
            return true;
        }

        /// <summary>
        /// get animation by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SpriteSheet GetAnimation(string name)
        {
            return _Animations.Where(x => x.Name == name.ToLower()).FirstOrDefault();
        }
    }
}
