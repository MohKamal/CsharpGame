using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Physics
{
    public class Circle
    {
        public float Radius { get; set; }
        public Point Position { get; set; }

        public float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt((((a.X - b.X)^2) + ((a.Y - b.Y)^2)));
        }

        public bool CirclevsCircleUnoptimized(Circle a, Circle b)
        {
            float r = a.Radius + b.Radius;
            return r < Distance(a.Position, b.Position);
        }

        public bool CirclevsCircleOptimized(Circle a, Circle b)
        {
            float r = a.Radius + b.Radius;
            r *= r;
            return r < (((a.Position.X + b.Position.X)^2) + ((a.Position.Y + b.Position.Y)^2));
        }
    }
}
