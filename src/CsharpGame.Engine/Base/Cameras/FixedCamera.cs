using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base.Cameras
{
    /// <summary>
    /// Camera that don't along the level, stay inside the visible game screen only
    /// If you want the camera follow the a gameobject and move along the level, use WorldCamera
    /// </summary>
    public class FixedCamera : Camera
    {
        public FixedCamera(int screenWidth, int screenHeight, int levelWidth, int levelHeight, float speed = 10) : base(screenWidth, screenHeight, levelWidth, levelHeight, speed)
        {
        }
    }
}
