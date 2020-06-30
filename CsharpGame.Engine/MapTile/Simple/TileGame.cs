﻿using CsharpGame.Engine.Base;
using CsharpGame.Engine.Base.Cameras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.MapTile.Simple
{
    public class TileGame
    {
        private Core _Core;
        public Core Engine { get => _Core; }
        public WorldCamera Camera { get; set; }

        private Map _Map;
        public Map Map { get => _Map; }

        public TileGame(Core core)
        {
            _Core = core;
            _Core.DisplayFPS = true;
            _Map = new Map("Default", new System.Drawing.Size((Engine.ScreenWith() * 2) / 16, (Engine.ScreenHeight() * 2) / 16), new System.Drawing.Size(16, 16));
            Camera = new WorldCamera(Engine.ScreenWith(), Engine.ScreenHeight(), Map.Size.Width * Map.TilesSize.Width, Map.Size.Height * Map.TilesSize.Height);
            Camera.Speed = 2;
        }

        /// <summary>
        /// Register a new to be used
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool RegisterMap(Map map)
        {
            if (map == null)
                return false;
            _Map = map;
            return true;
        }

        /// <summary>
        /// Draw map tiles to the screen
        /// </summary>
        /// <returns></returns>
        public virtual bool DrawMap()
        {
            if (Map == null)
                return false;

            double startCol = Math.Floor((double)Camera.Location.X / Map.TilesSize.Width);
            double endCol = startCol + (Camera.CameraSize.Width / Map.TilesSize.Width);
            double startRow = Math.Floor((double)Camera.Location.Y / Map.TilesSize.Height);
            double endRow = startRow + (Camera.CameraSize.Height / Map.TilesSize.Height);

            double offsetX = -Camera.Location.X + (startCol * Map.TilesSize.Width);
            double offsetY = -Camera.Location.Y + (startRow * Map.TilesSize.Height);
            Camera.Offset = new PointF((float)offsetX, (float)offsetY);
            for (double c = startCol; c < endCol; c++)
            {
                for (double r = startRow; r < endRow; r++)
                {
                    Tile tile = Map.Grid[(int)c, (int)r];
                    double x = (c - startCol) * Map.TilesSize.Width + offsetX;
                    double y = (r - startRow) * Map.TilesSize.Height + offsetY;
                    Engine.Drawer.Sprite(new PointF((float)Math.Round(x), (float)Math.Round(y)), tile.Texture);
                }
            }

            return true;
        }

        public void Run(float ElapsedTime)
        {
            UserInputs(ElapsedTime);
            MouseMoving();
            MouseTileClick();
            //user edits
            Update(ElapsedTime);
            DrawMap();

            Engine.Drawer.String($"Use Arrows or mouse to move around", "Arial", 8, System.Drawing.Color.Black, new System.Drawing.PointF(20, 20));
            Engine.Drawer.String($"Use mouse left button to change any tile texture", "Arial", 8, System.Drawing.Color.Black, new System.Drawing.PointF(20, 30));

        }

        /// <summary>
        /// Function to give the user a space to add modificaiton to the tiles logic
        /// </summary>
        /// <param name="ElapsedTime"></param>
        /// <returns></returns>
        public virtual bool Update(float ElapsedTime)
        {
            return true;
        }

        public PointF WorldToScreen(double x, double y)
        {
            return new PointF((float)x - Camera.Location.X, (float)y - Camera.Location.Y);
        }

        public Point ScreenToWorld(double x, double y)
        {
            double startCol = Math.Floor((double)Camera.Location.X / Map.TilesSize.Width);
            double startRow = Math.Floor((double)Camera.Location.Y / Map.TilesSize.Height);
            // Change the tile texture for fun
            return new System.Drawing.Point((((int)x - (int)Camera.Offset.X) / Map.TilesSize.Width) + (int)startCol, (((int)y - (int)Camera.Offset.Y) / Map.TilesSize.Height) + (int)startRow);
        }

        private void UserInputs(float ElapsedTime)
        {
            //Key Down
            if (Engine.KeyClicked(System.Windows.Forms.Keys.Up))
            {
                if (Camera.Location.Y > 0)
                    Camera.Location = new PointF(Camera.Location.X, Camera.Location.Y - Camera.Speed);
                else
                    Camera.Location = new PointF(Camera.Location.X, 0);
            }

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Right))
            {
                if (Camera.Location.X < (Map.Size.Width * Map.TilesSize.Width) - Camera.CameraSize.Width)
                    Camera.Location = new PointF(Camera.Location.X + Camera.Speed, Camera.Location.Y);
                else
                    Camera.Location = new PointF((Map.Size.Width * Map.TilesSize.Width) - Camera.CameraSize.Width, Camera.Location.Y);
            }

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Left))
            {
                if (Camera.Location.X > 0)
                    Camera.Location = new PointF(Camera.Location.X - Camera.Speed, Camera.Location.Y);
                else
                    Camera.Location = new PointF(0, Camera.Location.Y);
            }

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Down))
            {
                if (Camera.Location.Y < (Map.Size.Height * Map.TilesSize.Height) - Camera.CameraSize.Height)
                    Camera.Location = new PointF(Camera.Location.X, Camera.Location.Y + Camera.Speed);
                else
                    Camera.Location = new PointF(Camera.Location.X, (Map.Size.Height * Map.TilesSize.Height) - Camera.CameraSize.Height);
            }

        }

        /// <summary>
        /// Move the map when the mouse is in the edge of the camera
        /// </summary>
        private void MouseMoving()
        {
            System.Drawing.Point MouseLoaction = Engine.MousePosition();

            if(MouseLoaction.X <= Camera.Position.X + 20)
            {
                if (Camera.Location.X > 0)
                    Camera.Location = new PointF(Camera.Location.X - Camera.Speed, Camera.Location.Y);
                else
                    Camera.Location = new PointF(0, Camera.Location.Y);
            }
            Camera.UpdateMaxPosition();
            if (MouseLoaction.X >= Camera.MaxPosition.X - 20)
            {
                if (Camera.Location.X < (Map.Size.Width * Map.TilesSize.Width) - Camera.CameraSize.Width)
                    Camera.Location = new PointF(Camera.Location.X + Camera.Speed, Camera.Location.Y);
                else
                    Camera.Location = new PointF((Map.Size.Width * Map.TilesSize.Width) - Camera.CameraSize.Width, Camera.Location.Y);
            }

            if(MouseLoaction.Y <= Camera.Position.Y + 20)
            {
                if (Camera.Location.Y > 0)
                    Camera.Location = new PointF(Camera.Location.X, Camera.Location.Y - Camera.Speed);
                else
                    Camera.Location = new PointF(Camera.Location.X, 0);
            }

            if(MouseLoaction.Y >= Camera.MaxPosition.Y - 20)
            {
                if (Camera.Location.Y < (Map.Size.Height * Map.TilesSize.Height) - Camera.CameraSize.Height)
                    Camera.Location = new PointF(Camera.Location.X, Camera.Location.Y + Camera.Speed);
                else
                    Camera.Location = new PointF(Camera.Location.X, (Map.Size.Height * Map.TilesSize.Height) - Camera.CameraSize.Height);
            }
        }

        public void MouseTileClick()
        {
            if (Engine.MouseClicked(System.Windows.Forms.MouseButtons.Left))
            {
                System.Drawing.Point MouseLoaction = Engine.MousePosition();
                Point tilePosition = ScreenToWorld(MouseLoaction.X, MouseLoaction.Y);
                Tile tile = Map.Grid[tilePosition.X, tilePosition.Y];
                if (tile != null)
                    tile.Texture.LoadFromFile(CsharpGame.Engine.Resource.tile_1);
            }
        }
    }
}
