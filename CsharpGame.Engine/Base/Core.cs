using CsharpGame.Engine.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace CsharpGame.Engine.Base
{
    //Core
    public class Core
    {
        /// <summary>
        /// Init the picturebox to draw the game
        /// </summary>
        /// <param name="drawingArea"></param>
        public Core(PictureBox drawingArea)
        {
            DrawingArea = drawingArea;
            _GameObjects = new List<GameObject>();
            DisplayFPS = true;
            CalculeFPS = true;
            KeyboardKey = new List<Keys>();
            MouseButton = new List<MouseButtons>();
        }

        /// <summary>
        /// Frame timer and count to see the fps count
        /// </summary>
        private float FrameTimer;
        private int FrameCount;
        public int FPS;
        public bool DisplayFPS;
        public bool CalculeFPS;

        /// <summary>
        /// Those functions will be overrided by the user
        /// OnCreate is fired on time at first to setup the game
        /// OnUpdate is executed each time
        /// </summary>
        /// <returns></returns>
        public virtual bool OnCreate() { return true; }
        public virtual bool OnUpdate(double ElapsedTime) { return true; }

        /// <summary>
        /// Add a Game object to the list, to be draw, and rules applied automatically
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool RegisterGameObject(GameObject gameObject)
        {
            if (gameObject == null)
                return false;

            if (_GameObjects.Contains(gameObject))
                return false;

            _GameObjects.Add(gameObject);
            return true;
        }

        public bool RegisterMap(Map map)
        {
            if (map == null)
                return false;
            return true;
        }

        /// <summary>
        /// Screen Width
        /// </summary>
        /// <returns></returns>
        public int ScreenWith()
        {
            return DrawingArea.Width;
        }

        /// <summary>
        /// Screen Height
        /// </summary>
        /// <returns></returns>
        public int ScreenHeight()
        {
            return DrawingArea.Height;
        }
        /// <summary>
        /// Get the list of the registred objects
        /// </summary>
        /// <returns></returns>
        public List<GameObject> RegistredObjects()
        {
            return _GameObjects;
        }

        /// <summary>
        /// Regroups all the game object
        /// </summary>
        private List<GameObject> _GameObjects;

        /// <summary>
        /// Drawer to draw the game
        /// </summary>
        public Drawer Drawer;
        private PictureBox DrawingArea;

        /// <summary>
        /// Delta time variables
        /// </summary>
        private DateTime _StartTime = DateTime.Now;
        private DateTime _EndTime = DateTime.Now;
        private double ElapsedTime;

        /// <summary>
        /// Replacing the while with this timer
        /// </summary>
        private System.Timers.Timer timer;

        /// <summary>
        /// For stoping the engine
        /// </summary>
        bool EngineActive = true;

        /// <summary>
        /// Checking if the user is clicking a key
        /// </summary>
        public bool IsClicking = false;

        /// <summary>
        /// Clicked Keys
        /// </summary>
        private List<Keys> KeyboardKey;

        /// <summary>
        /// Clicked mouse button
        /// </summary>
        private List<MouseButtons> MouseButton;

        /// <summary>
        /// Where the Magic is happening
        /// Regrouping all the functions to get the game logic
        /// </summary>
        /// <returns></returns>
        private bool EngineThread()
        {
            Drawer = new Drawer(DrawingArea);
            Form form = DrawingArea.FindForm();
            if(form != null)
            {
                form.KeyDown += Form_KeyDown;
                form.KeyUp += Form_KeyUp;
            }
            DrawingArea.MouseClick += DrawingArea_MouseClick;
            DrawingArea.MouseMove += DrawingArea_MouseMove;
            EngineActive = OnCreate();
            timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            return EngineActive;
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            IsClicking = false;
            KeyboardKey.Remove(e.KeyCode);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            IsClicking = true;
            KeyboardKey.Add(e.KeyCode);
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void DrawingArea_MouseClick(object sender, MouseEventArgs e)
        {
            IsClicking = true;
        }

        /// <summary>
        /// Check if the user is clicking a specific key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool KeyClicked(Keys keys)
        {
            return KeyboardKey.Contains(keys);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Clear the frame
            Drawer.Clear(Color.White);
            //Calcule Detla time
            _EndTime = DateTime.Now;
            ElapsedTime = _EndTime.Subtract(_StartTime).TotalMilliseconds / 1000;
            _StartTime = _EndTime;
            //Executre the user logic
            EngineActive = OnUpdate(ElapsedTime);
            //Draw registred game object
            foreach (GameObject gameObject in _GameObjects)
                Drawer.GameObject(gameObject);
            //Apply new frame to the picturebox
            Drawer.ToScreen();

            //Get FPS
            if (CalculeFPS)
            {
                FrameTimer += (float)ElapsedTime;
                FrameCount++;
                if (FrameTimer >= 1.0f)
                {
                    FrameTimer -= 1.0f;
                    FPS = FrameCount;
                    FrameCount = 0;
                    if (DisplayFPS)
                        Console.WriteLine($"FPS: {FPS}");
                }
            }


            if (DisplayFPS)
                Drawer.String($"FPS: {FPS}", "Arial", 10f, Color.Red, new PointF(ScreenWith() - 100, 10));
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (DrawingArea == null)
            {
                Console.WriteLine("Drawing control is null");
                return false;
            }
            return EngineThread();
        }
    }
}
