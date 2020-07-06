using CsharpGame.Engine.Base;
using CsharpGame.Engine.Base.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Platformer
{
    public class PlatformGame
    {
        private PlatformerGrid _Grid;
        private Base.Engine _Core;

        //Public properties to edit by user
        public PlatformerGrid Grid { get => _Grid; }

        public PlatformCharacter Character { get; set; }

        public Camera Camera { get; set; }

        public bool ShowGrid { get; set; }

        public bool CreateWithMouse { get; set; }
        public float PLAYER_SPAWN_X { get; set; }
        public float PLAYER_SPAWN_Y { get; set; }
        public float PLAYER_JUMP_SPEED { get; set; }
        public float PLAYER_WALK_SPEED { get; set; }
        public Base.Engine Engine { get => _Core; }

        public PlatformGame(Base.Engine engine)
        {
            _Grid = new PlatformerGrid(100, 25);
            _Core = engine;
            ShowGrid = true;
            CreateWithMouse = true;
            PLAYER_SPAWN_X = 10;
            PLAYER_SPAWN_Y = 10;
            PLAYER_JUMP_SPEED = -100;
            PLAYER_WALK_SPEED = 80;
            if(Character == null)
                Character = new PlatformCharacter(new System.Drawing.PointF(PLAYER_SPAWN_X, PLAYER_SPAWN_Y), new Sprite(Grid.Resolution, Grid.Resolution));
            Grid.Nodes.Add(Character);
            Camera = new WorldCamera(_Core.ScreenWith(), _Core.ScreenHeight(), Grid.Width * Grid.Resolution, Grid.Height * Grid.Resolution);
        }

        /// <summary>
        /// Set a custom grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public bool RegisterGrid(PlatformerGrid grid)
        {
            if (grid == null)
                return false;
            _Grid = grid;
            return true;
        }


        /// <summary>
        /// Platformer logic, movements, collisions...
        /// </summary>
        /// <param name="ElapsedTime"></param>
        public void Run(float ElapsedTime)
        {
            Camera.SetPositionTo(Character);
            for(int i =0; i < Grid.Nodes.Count; i++)
            {
                PlatformerNode node = Grid.Nodes[i];

                if (node.Velocity.X != 0)
                    node.limitXSpeed(ElapsedTime, Grid.Epsilon);

                float vx = node.Velocity.X * ElapsedTime;
                node.MaxPosition = new System.Drawing.PointF(node.Position.X, node.MaxPosition.Y);
                node.Position = new System.Drawing.PointF(node.Position.X + vx, node.Position.Y);

                if(node.Velocity.X > 0)
                {
                    if(node.getCellRight(Grid.Resolution, Grid.Epsilon, node.Position.X) != node.getCellRight(Grid.Resolution, Grid.Epsilon, node.MaxPosition.X))
                    {
                        List<float> Cells = node.getYCells(Grid.Resolution, Grid.Epsilon);
                        for(float y = Cells[0]; y <= Cells[1]; y++)
                        {
                            if(Grid.getWall((int)node.getCellRight(Grid.Resolution, Grid.Epsilon, node.Position.X), (int)(y / Grid.Resolution)) ||
                                (y != Cells[0] && Grid.getCeiling((int)node.getCellRight(Grid.Resolution, Grid.Epsilon, node.Position.X), (int)(y/Grid.Resolution))))
                            {
                                node.collideCellRight(Grid.Resolution, Grid.Epsilon, node.Position.X);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (node.getCellLeft(Grid.Resolution, Grid.Epsilon, node.Position.X) != node.getCellLeft(Grid.Resolution, Grid.Epsilon, node.MaxPosition.X))
                    {
                        List<float> Cells = node.getYCells(Grid.Resolution, Grid.Epsilon);
                        for (float y = Cells[0]; y <= Cells[1]; y++)
                        {
                            if (Grid.getWall((int)node.getCellLeft(Grid.Resolution, Grid.Epsilon, node.Position.X), (int)(y / Grid.Resolution)) ||
                                (y != Cells[0] && Grid.getCeiling((int)node.getCellLeft(Grid.Resolution, Grid.Epsilon, node.Position.X), (int)(y / Grid.Resolution))))
                            {
                                node.collideCellLeft(Grid.Resolution, Grid.Epsilon, node.Position.X);
                                break;
                            }
                        }
                    }
                }

                if (node.onGround)
                {
                    List<float> Cells = node.getXCells(Grid.Resolution, Grid.Epsilon);
                    for(float x=Cells[0]; x <= Cells[1]; x++)
                    {
                        node.onGround = false;
                        if(Grid.getCeiling((int)x, (int)node.getCellBottom(Grid.Resolution, Grid.Epsilon, node.Position.Y) + 1) || 
                            (x != Cells[0] && Grid.getWall((int)x, (int)node.getCellBottom(Grid.Resolution, Grid.Epsilon, node.Position.Y) + 1)))
                        {
                            node.onGround = true;
                            break;
                        }
                    }
                }

                if (node.onGround)
                {
                    if(node.Velocity.X > 0)
                    {
                        node.Velocity = new System.Drawing.PointF(node.Velocity.X - Grid.Friction * ElapsedTime, node.Velocity.Y);
                        if (node.Velocity.X < 0)
                            node.Velocity = new System.Drawing.PointF(0, node.Velocity.Y);
                    }
                    else if(node.Velocity.X < 0)
                    {
                        node.Velocity = new System.Drawing.PointF(node.Velocity.X + Grid.Friction * ElapsedTime, node.Velocity.Y);
                        if (node.Velocity.X > 0)
                            node.Velocity = new System.Drawing.PointF(0, node.Velocity.Y);
                    }
                }

                if (!node.onGround)
                    node.Velocity = new System.Drawing.PointF(node.Velocity.X, node.Velocity.Y + Grid.Gravity * ElapsedTime);

                if (node.Velocity.Y != 0)
                {
                    node.limitYSpeed(ElapsedTime, Grid.Epsilon);

                    float vy = node.Velocity.Y * ElapsedTime;
                    node.MaxPosition = new System.Drawing.PointF(node.MaxPosition.X, node.Position.Y);
                    node.Position = new System.Drawing.PointF(node.Position.X, node.Position.Y + vy);

                    if (node.Velocity.Y > 0)
                    {
                        if (node.getCellBottom(Grid.Resolution, Grid.Epsilon, node.Position.Y) != node.getCellBottom(Grid.Resolution, Grid.Epsilon, node.MaxPosition.Y))
                        {
                            List<float> Cells = node.getXCells(Grid.Resolution, Grid.Epsilon);

                            for (float x = Cells[0]; x <= Cells[1]; x++)
                            {
                                if (Grid.getCeiling((int)x, (int)(node.getCellBottom(Grid.Resolution, Grid.Epsilon, node.Position.Y))) ||
                                    (x != Cells[0] && Grid.getWall((int)x, (int)node.getCellBottom(Grid.Resolution, Grid.Epsilon, node.Position.Y))))
                                {
                                    node.collideCellBottom(Grid.Resolution, Grid.Epsilon, node.Position.Y);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (node.getCellTop(Grid.Resolution, Grid.Epsilon, node.Position.Y) != node.getCellTop(Grid.Resolution, Grid.Epsilon, node.MaxPosition.Y))
                        {
                            List<float> Cells = node.getXCells(Grid.Resolution, Grid.Epsilon);

                            for (float x = Cells[0]; x <= Cells[1]; x++)
                            {
                                if (Grid.getCeiling((int)x, (int)(node.getCellTop(Grid.Resolution, Grid.Epsilon, node.Position.Y))) ||
                                    (x != Cells[0] && Grid.getWall((int)x, (int)node.getCellTop(Grid.Resolution, Grid.Epsilon, node.Position.Y))))
                                {
                                    node.collideCellTop(Grid.Resolution, Grid.Epsilon, node.Position.Y);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            UserInputs(ElapsedTime);
            if (CreateWithMouse)
                MouseDrawing();
            //user edits
            Update(ElapsedTime);


            if (ShowGrid)
            {
                //Grid
                for (int y = 0; y < Grid.Height; y++)
                {
                    if (Camera is WorldCamera)
                        Engine.Drawer.Line(new System.Drawing.Point(0, (y * Grid.Resolution) + (int)Camera.Offset.Y), new System.Drawing.Point(Grid.Width * Grid.Resolution, (y * Grid.Resolution) + (int)Camera.Offset.Y), System.Drawing.Color.Red);
                    else
                        Engine.Drawer.Line(new System.Drawing.Point(0, (y * Grid.Resolution)), new System.Drawing.Point(Grid.Width * Grid.Resolution, (y * Grid.Resolution)), System.Drawing.Color.Red);
                }
                for (int x = 0; x < Grid.Width; x++)
                {
                    if (Camera is WorldCamera)
                        Engine.Drawer.Line(new System.Drawing.Point((x * Grid.Resolution) + (int)Camera.Offset.X, 0), new System.Drawing.Point((x * Grid.Resolution) + (int)Camera.Offset.X, Grid.Height * Grid.Resolution), System.Drawing.Color.Red);
                    else
                        Engine.Drawer.Line(new System.Drawing.Point(x * Grid.Resolution, 0), new System.Drawing.Point(x * Grid.Resolution, Grid.Height * Grid.Resolution), System.Drawing.Color.Red);
                }
            }

            //Walls
            for(int x = 0; x < Grid.Width; x++)
            {
                for (int y = 0; y < Grid.Height; y++)
                {
                    PlatformerGridCell cell = Grid.getCell(x, y);
                    if (cell.Wall)
                    {
                        if (Camera is WorldCamera)
                            Engine.Drawer.Line(new System.Drawing.Point((x * Grid.Resolution) + (int)Camera.Offset.X, ((y + 1) * Grid.Resolution) + (int)Camera.Offset.Y), new System.Drawing.Point((x * Grid.Resolution) + (int)Camera.Offset.X, (y * Grid.Resolution) + (int)Camera.Offset.Y), System.Drawing.Color.Blue, Camera);
                        else
                            Engine.Drawer.Line(new System.Drawing.Point(x * Grid.Resolution, (y + 1) * Grid.Resolution), new System.Drawing.Point(x * Grid.Resolution, y * Grid.Resolution), System.Drawing.Color.Blue, Camera);
                    }
                    if (cell.Celling)
                    {
                        if (Camera is WorldCamera)
                            Engine.Drawer.Line(new System.Drawing.Point(((x + 1) * Grid.Resolution)  + (int)Camera.Offset.X, (y * Grid.Resolution) + (int)Camera.Offset.Y), new System.Drawing.Point((x * Grid.Resolution) + (int)Camera.Offset.X, (y * Grid.Resolution) + (int)Camera.Offset.Y), System.Drawing.Color.Green, Camera);
                        else
                            Engine.Drawer.Line(new System.Drawing.Point((x + 1) * Grid.Resolution, y * Grid.Resolution), new System.Drawing.Point(x * Grid.Resolution, y * Grid.Resolution), System.Drawing.Color.Green, Camera);
                    }
                }
            }

            //Nodes
            for (int i = 0; i < Grid.Nodes.Count; i++)
            {
                PlatformerNode node = Grid.Nodes[i];
                if (node.Sprite is SpriteSheet)
                {
                    if (Camera is WorldCamera)
                        Engine.Drawer.SpriteSheet(new System.Drawing.Point((int)node.Position.X + (int)Camera.Offset.X, (int)node.Position.Y + (int)Camera.Offset.Y), (SpriteSheet)node.Sprite, Camera);
                    else
                        Engine.Drawer.SpriteSheet(new System.Drawing.Point((int)node.Position.X, (int)node.Position.Y), (SpriteSheet)node.Sprite, Camera);

                }
                else
                {
                    if (Camera is WorldCamera)
                        Engine.Drawer.Sprite(new System.Drawing.Point((int)node.Position.X + (int)Camera.Offset.X, (int)node.Position.Y + (int)Camera.Offset.Y), node.Sprite, Camera);
                    else
                        Engine.Drawer.Sprite(new System.Drawing.Point((int)node.Position.X, (int)node.Position.Y), node.Sprite, Camera);
                }
            }

            //Debugging data
            //Engine.Drawer.Rectangle(Camera.Position.X, Camera.Position.Y, Camera.CameraSize.Width, Camera.CameraSize.Height, System.Drawing.Color.Black);
            //Engine.Drawer.Rectangle(Character.Position.X + (int)Camera.Offset.X, Character.Position.Y + (int)Camera.Offset.Y, Character.Sprite.Width, Character.Sprite.Height, System.Drawing.Color.Black);
            //Engine.Drawer.String($"Player Position: {Character.Position}", "Arial", 12, System.Drawing.Color.Black, new System.Drawing.PointF(100, 50));
            //Engine.Drawer.String($"Camera Position: {Camera.Position}", "Arial", 12, System.Drawing.Color.Black, new System.Drawing.PointF(100, 80));
            //Engine.Drawer.String($"Camera Offset: {Camera.Offset}", "Arial", 12, System.Drawing.Color.Black, new System.Drawing.PointF(100, 120));
            //Engine.Drawer.String($"Camera Max: {Camera.MaxPosition}", "Arial", 12, System.Drawing.Color.Black, new System.Drawing.PointF(100, 140));
        }

        private bool JumpDown = false;
        private bool LeftDown = false;
        private bool RightDown = false;
        private void UserInputs(float ElapsedTime)
        {
            //Key Down
            if (Engine.KeyClicked(System.Windows.Forms.Keys.Up))
            {
                if(!JumpDown && Character.onGround)
                {
                    JumpDown = true;
                    Character.SetVelocityY(PLAYER_JUMP_SPEED);
                }
            }

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Right))
                RightDown = true;

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Left))
                LeftDown = true;

            //Key Up
            if (!Engine.KeyClicked(System.Windows.Forms.Keys.Up))
                JumpDown = false;

            if (!Engine.KeyClicked(System.Windows.Forms.Keys.Right))
                RightDown = false;

            if (!Engine.KeyClicked(System.Windows.Forms.Keys.Left))
                LeftDown = false;

            //Moving charachter
            if (RightDown)
                Character.SetVelocityX(Math.Min((float)Character.Velocity.X + PLAYER_WALK_SPEED * ElapsedTime, (float)PLAYER_WALK_SPEED));
            if(LeftDown)
                Character.SetVelocityX(Math.Max((float)Character.Velocity.X - PLAYER_WALK_SPEED * ElapsedTime, (float)-PLAYER_WALK_SPEED));

            if (Character.Position.X < -Character.Sprite.Width ||
              Character.Position.Y < -Character.Sprite.Height ||
              Character.Position.X > Grid.Width * Grid.Resolution ||
              Character.Position.Y > Grid.Height * Grid.Resolution)
                Character.Position = new System.Drawing.PointF(PLAYER_SPAWN_X, PLAYER_SPAWN_Y);
        }

        /// <summary>
        /// Draw line when mouse is clicked
        /// </summary>
        private void MouseDrawing()
        {

            System.Drawing.Point MouseLoaction = Engine.MousePosition();
            if(Camera is WorldCamera)
                MouseLoaction = new System.Drawing.Point(MouseLoaction.X - (int)Camera.Offset.X, MouseLoaction.Y - (int)Camera.Offset.Y);
            float gridX = (float)Math.Floor((double)(MouseLoaction.X / Grid.Resolution));
            float gridY = (float)Math.Floor((double)(MouseLoaction.Y / Grid.Resolution));
            bool gridWall = false;
            findSelectedEdge(MouseLoaction.X, MouseLoaction.Y, gridX, gridY, gridWall);

            if (Engine.MouseClicked(System.Windows.Forms.MouseButtons.Left))
            {
                if (gridX == -1 || gridY == -1)
                    return;

                // Toggle selected edge
                if (gridWall)
                    Grid.setWall((int)gridX, (int)gridY, !Grid.getWall((int)gridX, (int)gridY));
                else
                    Grid.setCeiling((int)gridX, (int)gridY, !Grid.getCeiling((int)gridX, (int)gridY));
            }
        }

        private void findSelectedEdge(float mouseX, float mouseY, float gridX, float gridY, bool gridWall)
        {
            float deltaX = mouseX - gridX * Grid.Resolution;
            float deltaY = mouseY - gridY * Grid.Resolution;
            gridWall = deltaX * deltaX < deltaY * deltaY;
            if (deltaX + deltaY > Grid.Resolution)
            {
                if (deltaX > deltaY)
                {
                    gridX = Math.Min(gridX + 1, Grid.Width);
                }
                else
                {
                    gridY = Math.Min(gridY + 1, Grid.Height);
                }

                gridWall = !gridWall;
            }
        }

        /// <summary>
        /// Function to give the user a space to add modificaiton to the platformer logic
        /// </summary>
        /// <param name="ElapsedTime"></param>
        /// <returns></returns>
        public virtual bool Update(float ElapsedTime)
        {
            return true;
        }
    }
}
