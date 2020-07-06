using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class AStar
    {
        public Tile Start { get; private set; }
        public Tile End { get; private set; }
        public Layout Map { get; private set; }
        public int NodeVisits { get; private set; }
        public double ShortestPathLength { get; set; }
        public double ShortestPathCost { get; private set; }

        public List<Tile> Find(Tile start, Tile goal, Layout layout)
        {
            Start = start;
            End = goal;
            Map = layout;
            foreach (Tile t in layout.Grid)
            {
                if (t.Visited)
                    t.Visited = false;
            }
            ShortestPathLength = 0;
            ShortestPathCost = 0;
            return GetShortestPathAstart();
        }

        public List<Tile> GetShortestPathAstart()
        {
            foreach (var node in Map.Grid)
                node.StraightLineDistanceToEnd = node.StraightLineDistanceTo(End);
            AstarSearch();
            var shortestPath = new List<Tile>();
            shortestPath.Add(End);
            BuildShortestPath(shortestPath, End);
            shortestPath.Reverse();
            return shortestPath;
        }

        private void AstarSearch()
        {
            NodeVisits = 0;
            Start.MinCostToStart = 0;
            var prioQueue = new List<Tile>();
            prioQueue.Add(Start);
            do
            {
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);
                NodeVisits++;
                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == End)
                    return;
            } while (prioQueue.Any());
        }

        private void BuildShortestPath(List<Tile> list, Tile node)
        {
            if (node.NearestToStart == null)
                return;
            list.Add(node.NearestToStart);
            ShortestPathLength += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Length;
            ShortestPathCost += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Cost;
            BuildShortestPath(list, node.NearestToStart);
        }
    }
}
