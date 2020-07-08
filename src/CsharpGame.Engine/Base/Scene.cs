using CsharpGame.Engine.MapTile.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    /// <summary>
    /// This object is to create multiple scenes in one game, like menu, level 1, level 2...
    /// </summary>
    public class Scene
    {
        public string Name { get; private set; }
        public int Order { get; private set; }

        public bool IsEnded { get; private set; }
        public bool IsCreated { get; private set; }

        public Engine Engine { get; private set; }

        public Scene(string name, Engine engine)
        {
            Name = name;
            IsEnded = false;
            Engine = engine;
            IsCreated = false;
        }

        /// <summary>
        /// Set the scene to ended
        /// </summary>
        public void Ended()
        {
            IsEnded = true;
        }

        /// <summary>
        /// Set created to true when objects are created
        /// </summary>
        public void Created()
        {
            IsCreated = true;
        }

        public virtual bool OnCreate() { return true; }

        public virtual bool OnUpdate(double ElapsedTime) { return true; }
    }
}
