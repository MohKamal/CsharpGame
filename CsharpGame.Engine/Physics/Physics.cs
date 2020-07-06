using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Physics
{
    public class Physics
    {
        private Base.Engine _Core { get; set; }
        public float Gravity { get; set; }

        /// <summary>
        /// Init the physic object and properties
        /// </summary>
        /// <param name="core"></param>
        public Physics(Base.Engine core)
        {
            _Core = core;
            Gravity = 20.0f;
            Collision.Core = _Core;
        }

        /// <summary>
        /// Check every object game, and apply rules
        /// </summary>
        public void ApplyPhysiques(double ElapsedTime)
        {
            foreach(GameObject gameObject in _Core.RegistredObjects())
            {
                if (!gameObject.Static)
                {
                    gameObject.Velocity = new System.Drawing.PointF(gameObject.Velocity.X, gameObject.Velocity.Y + (Gravity * (float)ElapsedTime));
                    if(gameObject.Velocity.Y > Gravity)
                        gameObject.Velocity = new System.Drawing.PointF(gameObject.Velocity.X, Gravity);

                    Collision collision = new Collision(gameObject);
                    //collision.CollidedWidthMap();
                    gameObject.Move();
                }
            }
        }

        public class Collision
        {
            private GameObject _Object;
            public GameObject Object { get => _Object; }

            public static Base.Engine Core { get; set; }

            public Collision(GameObject gameObject)
            {
                _Object = gameObject;
            }

            //public bool CollidedWidthMap()
            //{
            //    Tile tile = Core.Map.GetTile(_Object.Position.X, _Object.Position.Y);
            //    if (tile != null)
            //    {
            //        Tile under = Core.Map.GetTile((int)(tile.Position.X / tile.Sprite.Width), (int)(tile.Position.Y / tile.Sprite.Height) + 1);
            //        if (under != null)
            //        {
            //            Collision collision = new Collision(_Object);
            //            if (under.Ground)
            //                _Object.StopMoving();
            //        }
            //    }
            //    return false;
            //}

            public bool CollidedWidth(GameObject other)
            {
                if (_Object.Position.X < other.Position.X + other.Sprite.Width &&
                       _Object.Position.X + _Object.Sprite.Width > other.Position.X &&
                       _Object.Position.Y < other.Position.Y + other.Sprite.Height &&
                       _Object.Sprite.Height +_Object.Position.Y > other.Sprite.Height)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
