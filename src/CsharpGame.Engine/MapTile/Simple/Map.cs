using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class Map
    {
        public Size Size { get; set; }
        public string Name { get; set; }
        public Size TilesSize { get; set; }

        public Dictionary<int, Layout> _Layouts;
        public Dictionary<int, Layout> Layouts { get => _Layouts; }

        public Map(string name, Size size, Size tilesSize)
        {
            Name = name;
            Size = size;
            TilesSize = tilesSize;
            _Layouts = new Dictionary<int, Layout>();
        }

        /// <summary>
        /// Add layout to the map
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        public bool AddLayout(Layout layout)
        {
            if (layout == null)
                return false;
            layout.Order = _Layouts.Count;
            _Layouts.Add(_Layouts.Count, layout);
            return true;
        }
    }
}
