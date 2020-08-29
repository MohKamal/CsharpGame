using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    /// <summary>
    /// Game object have location, to move
    /// </summary>
    public class GameObject
    {
        public PointF Position { get; set; }
        public PointF Velocity { get; set; }
        public Sprite Sprite { get; set; }

        public string Name { get; set; }

        public Animation Animations { get; set; }

        /// <summary>
        /// If true, no physics rules with be applied on this object
        /// </summary>
        public bool Static { get; set; }

        public GameObject(PointF position, Sprite sprite)
        {
            Position = position;
            Sprite = sprite;
            Static = false;
            Animations = new Animation();
        }

        /// <summary>
        /// Change the current animation
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SetAnimation(string name)
        {
            SpriteSheet spriteSheet = Animations.GetAnimation(name);
            if (spriteSheet == null)
                return false;
            Sprite = spriteSheet;
            return true;
        }

        /// <summary>
        /// This function to check for some user rules, like falling gravity
        /// </summary>
        /// <returns></returns>
        public virtual bool MovingCondition(float X, float Y)
        {
            //Check for the move function
            return false;
        }

        /// <summary>
        /// Using the object velocity, this will change the position
        /// Also, it gonna verify the Moving Condition Function
        /// </summary>
        public void Move()
        {
            float NewPosX = this.Position.X + Velocity.X;
            float NewPosY = this.Position.Y + Velocity.Y;

            //Check if there is condition
            MovingCondition(NewPosX, NewPosY);

            this.Position = new PointF(NewPosX, NewPosY);
        }

        /// <summary>
        /// Stop the object from moving
        /// </summary>
        public void StopMoving()
        {
            this.Velocity = new PointF(0, 0);
        }

        /// <summary>
        /// Check if this object is in collision with another object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CollisionWith(GameObject other)
        {
            if (this.Position.X < other.Position.X + other.Sprite.Width &&
                   this.Position.X + this.Sprite.Width > other.Position.X &&
                   this.Position.Y < other.Position.Y + other.Sprite.Height &&
                   this.Sprite.Height + this.Position.Y > other.Sprite.Height)
            {
                return true;
            }
            return false;
        }
    }
}
