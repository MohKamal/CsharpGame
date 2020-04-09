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
        /// <summary>
        /// If true, no physics rules with be applied on this object
        /// </summary>
        public bool Static { get; set; }

        public GameObject(PointF position, Sprite sprite)
        {
            Position = position;
            Sprite = sprite;
            Static = false;
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
    }
}
