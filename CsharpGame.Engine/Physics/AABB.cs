using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Physics
{
    public class AABB
    {
        public PointF Min { get; set; }
        public PointF Max { get; set; }

        public bool AABBvsAABB(AABB a, AABB b)
        {
            // Exit with no intersection if found separated along an axis
            if (a.Max.X < b.Min.X || a.Min.X > b.Max.X) return false;
            if (a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y) return false;
            // No separating axis found, therefor there is at least one overlapping axis
            return true;
        }
    }
}
