using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Platformer
{
    public class PlatformerGrid
    {
        public int Width { get; }
        public int Height { get; }

        public int Resolution { get; }

        public float Gravity { get; set; }
        public float Friction { get; }

        public float Epsilon { get; }

        public List<PlatformerGridCell> Cells { get; }
        public List<PlatformerNode> Nodes { get; }

        public PlatformerGrid(int with, int height, int resolution=16, float gravity=100, float friction=10)
        {
            Width = with;
            Height = height;
            Resolution = resolution;
            Gravity = gravity;
            Friction = friction;
            Epsilon = 0.0000001f;
            Cells = new List<PlatformerGridCell>();
            Nodes = new List<PlatformerNode>();
            for (var i = 0; i < Width * Height; ++i)
                Cells.Add(new PlatformerGridCell());
            //Set Default Celling
            for (int x = 0; x < Width; x++)
                setCeiling(x, Height - 1, true);
        }

        public bool validateCoordinates(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return false;
            return true;
        }

        public PlatformerGridCell getCell(int x, int y)
        {
            return Cells[x + y * Width];
        }

        public bool getWall(int x, int y)
        {
            if (!validateCoordinates(x, y))
                return false;

            return getCell(x, y).Wall;
        }

        public bool getCeiling(int x, int y)
        {
            if (!validateCoordinates(x, y))
                return false;

            return getCell(x, y).Celling;
        }

        public void setWall(int x, int y, bool wall)
        {
            if (validateCoordinates(x, y))
                getCell(x, y).Wall = wall;
        }

        public void setCeiling(int x, int y, bool celling)
        {
            if (validateCoordinates(x, y))
                getCell(x, y).Celling = celling;
        }

        public void addNode(PlatformerNode node)
        {
            if (node != null)
                Nodes.Add(node);
        }

        public void removeNode(PlatformerNode node)
        {
            if (node != null)
                Nodes.Remove(node);
        }
    }
}
