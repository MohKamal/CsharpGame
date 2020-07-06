using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class Tile
    {
        public Point Position { get; set; }
        public Sprite Texture { get; set; }
        public Size Size { get; }

        public Guid ID { get; }

        public Tile(Size size) {
            ID = Guid.NewGuid();
            Size = size;
        }

        public List<Edge> Connections { get; set; } = new List<Edge>();

        public double? MinCostToStart { get; set; }
        public Tile NearestToStart { get; set; }
        public bool Visited { get; set; }
        public double StraightLineDistanceToEnd { get; set; }

        internal void ConnectClosestNodes(Tile[,] nodes)
        {
            //var connections = new List<Edge>();
            //foreach (var node in nodes)
            //{
            //    if (node.ID == this.ID)
            //        continue;

            //    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
            //    connections.Add(new Edge
            //    {
            //        ConnectedNode = node,
            //        Length = dist,
            //        Cost = dist,
            //    });
            //}
            //connections = connections.OrderBy(x => x.Length).ToList();
            //foreach (var cnn in connections)
            //{
            //    //Connect three closes nodes that are not connected.
            //    if (!Connections.Any(c => c.ConnectedNode == cnn.ConnectedNode))
            //        Connections.Add(cnn);

            //    //Make it a two way connection if not already connected
            //    if (!cnn.ConnectedNode.Connections.Any(cc => cc.ConnectedNode == this))
            //    {
            //        var backConnection = new Edge { ConnectedNode = this, Length = cnn.Length };
            //        cnn.ConnectedNode.Connections.Add(backConnection);
            //    }
            //}

            int Rows = nodes.GetLength(0) - 1;
            int Cols = nodes.GetLength(1) - 1;

            if (Position.X > 0)
            {
                Tile node = nodes[Position.X - 1, Position.Y];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if (Position.Y > 0)
            {
                Tile node = nodes[Position.X, Position.Y - 1];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if (Position.X < Rows)
            {
                Tile node = nodes[Position.X + 1, Position.Y];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if (Position.Y < Cols)
            {
                Tile node = nodes[Position.X, Position.Y + 1];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if(Position.X > 0 && Position.Y > 0)
            {
                Tile node = nodes[Position.X - 1, Position.Y - 1];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if (Position.X < Rows && Position.Y < Cols)
            {
                Tile node = nodes[Position.X + 1, Position.Y + 1];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if (Position.X > 0 && Position.Y < Cols)
            {
                Tile node = nodes[Position.X - 1, Position.Y + 1];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }

            if (Position.X < Rows && Position.Y > 0)
            {
                Tile node = nodes[Position.X + 1, Position.Y - 1];
                if (node.ID != this.ID)
                {
                    var dist = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                    Edge e = new Edge()
                    {
                        ConnectedNode = node,
                        Length = dist,
                        Cost = dist,
                    };
                    if (!Connections.Contains(e))
                        Connections.Add(e);
                }
            }
        }

        public double StraightLineDistanceTo(Tile end)
        {
            return Math.Sqrt(Math.Pow(Position.X - end.Position.X, 2) + Math.Pow(Position.Y - end.Position.Y, 2));
        }

        internal bool ToCloseToAny(List<Tile> nodes)
        {
            foreach (var node in nodes)
            {
                var d = Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
                if (d < 0.01)
                    return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{ID} - {Position}";
        }
    }

    public class Edge
    {
        public double Length { get; set; }
        public double Cost { get; set; }
        public Tile ConnectedNode { get; set; }

        public override string ToString()
        {
            return "-> " + ConnectedNode.ToString();
        }
    }
}
