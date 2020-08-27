using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    /// <summary>
    /// This is a layer to draw sprites in it
    /// Every scene have at least one layer
    /// </summary>
    public class Layer
    {
        public int z_order { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Regroups all the game object
        /// </summary>
        private List<GameObject> _GameObjects;

        public Layer(string name)
        {
            Name = name;
            _GameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Get the list of the registred objects
        /// </summary>
        /// <returns></returns>
        public List<GameObject> RegistredObjects()
        {
            return _GameObjects;
        }

        /// <summary>
        /// Add a Game object to the list, to be draw, and rules applied automatically
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool RegisterGameObject(GameObject gameObject)
        {
            if (gameObject == null)
                return false;

            if (_GameObjects.Contains(gameObject))
                return false;

            _GameObjects.Add(gameObject);
            return true;
        }

    }
}
