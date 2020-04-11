using CsharpGame.Engine.Base;
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
        private Core _Core;

        //Public properties to edit by user
        public PlatformerGrid Grid { get => _Grid; }

        public PlatformCharacter Character { get; set; }

        public bool ShowGrid { get; set; }

        public bool CreateWithMouse { get; set; }

        public float PLAYER_SPAWN_X { get; set; }
        public float PLAYER_SPAWN_Y { get; set; }
        public float PLAYER_JUMP_SPEED { get; set; }
        public float PLAYER_WALK_SPEED { get; set; }
        public Core Engine { get => _Core; }

        public PlatformGame(Core engine)
        {
            _Grid = new PlatformerGrid(25, 25);
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
            for(int i=0; i < Grid.Nodes.Count; i++)
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
            Update(ElapsedTime);
            if (ShowGrid)
            {
                //Grid
                for (int y = 0; y < Grid.Height; y++)
                    Engine.Drawer.Line(new System.Drawing.Point(0, y * Grid.Resolution), new System.Drawing.Point(Grid.Width * Grid.Resolution, y * Grid.Resolution), System.Drawing.Color.Red);

                for (int x = 0; x < Grid.Height; x++)
                    Engine.Drawer.Line(new System.Drawing.Point(x * Grid.Resolution, 0), new System.Drawing.Point(x * Grid.Resolution, Grid.Height * Grid.Resolution), System.Drawing.Color.Red);
            }

            //Walls
            for(int x = 0; x < Grid.Width; x++)
            {
                for(int y = 0; y < Grid.Height; y++)
                {
                    PlatformerGridCell cell = Grid.getCell(x, y);
                    if (cell.Wall)
                        Engine.Drawer.Line(new System.Drawing.Point(x * Grid.Resolution, (y + 1) * Grid.Resolution), new System.Drawing.Point(x*Grid.Resolution, y*Grid.Resolution), System.Drawing.Color.Blue);
                    if (cell.Celling)
                        Engine.Drawer.Line(new System.Drawing.Point((x + 1) * Grid.Resolution, y * Grid.Resolution), new System.Drawing.Point(x * Grid.Resolution, y * Grid.Resolution), System.Drawing.Color.Green);
                }
            }

            //Nodes
            for (int i = 0; i < Grid.Nodes.Count; i++)
            {
                PlatformerNode node = Grid.Nodes[i];
                if (node.Sprite is SpriteSheet)
                    Engine.Drawer.SpriteSheet(node.Position, (SpriteSheet)node.Sprite);
                else
                    Engine.Drawer.Sprite(node.Position, node.Sprite);
                //Engine.Drawer.Rectangle(node.Position.X, node.Position.Y, node.Sprite.Width, node.Sprite.Height, System.Drawing.Color.Red);
            }
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
              Character.Position.X > Engine.ScreenWith()||
              Character.Position.Y > Engine.ScreenHeight())
                Character.Position = new System.Drawing.PointF(PLAYER_SPAWN_X, PLAYER_SPAWN_Y);
        }

        /// <summary>
        /// Draw line when mouse is clicked
        /// </summary>
        private void MouseDrawing()
        {

            System.Drawing.Point MouseLoaction = Engine.MousePosition();
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
