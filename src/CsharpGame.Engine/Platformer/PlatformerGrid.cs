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

        /// <summary>
        /// Validate an x & y positions
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool validateCoordinates(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return false;
            return true;
        }

        /// <summary>
        /// Get cell by positiion x & y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public PlatformerGridCell getCell(int x, int y)
        {
            return Cells[x + y * Width];
        }

        /// <summary>
        /// Check if the current cell is a wall
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool getWall(int x, int y)
        {
            if (!validateCoordinates(x, y))
                return false;

            return getCell(x, y).Wall;
        }


        /// <summary>
        /// Check if the current cell is celling
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool getCeiling(int x, int y)
        {
            if (!validateCoordinates(x, y))
                return false;

            return getCell(x, y).Celling;
        }

        /// <summary>
        /// Make the cell a wall
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wall"></param>
        public void setWall(int x, int y, bool wall)
        {
            if (validateCoordinates(x, y))
                getCell(x, y).Wall = wall;
        }

        /// <summary>
        /// Make a cell celling
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="celling"></param>
        public void setCeiling(int x, int y, bool celling)
        {
            if (validateCoordinates(x, y))
                getCell(x, y).Celling = celling;
        }

        /// <summary>
        /// A node to the grid
        /// </summary>
        /// <param name="node"></param>
        public void addNode(PlatformerNode node)
        {
            if (node != null)
                Nodes.Add(node);
        }

        /// <summary>
        /// Remove a node to the grid
        /// </summary>
        /// <param name="node"></param>
        public void removeNode(PlatformerNode node)
        {
            if (node != null)
                Nodes.Remove(node);
        }
    }
}
