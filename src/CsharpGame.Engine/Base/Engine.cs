using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace CsharpGame.Engine.Base
{
    //Core
    public class Engine
    {
        /// <summary>
        /// Init the picturebox to draw the game
        /// </summary>
        /// <param name="drawingArea"></param>
        public Engine(PictureBox drawingArea)
        {
            DrawingArea = drawingArea;
            _GameObjects = new List<GameObject>();
            DisplayFPS = true;
            CalculeFPS = true;
            KeyboardKey = new List<Keys>();
            MouseButton = new List<MouseButtons>();
            MousePos = new Point(0, 0);
            Scenes = new Dictionary<int, Scene>();
            //Add default scene
            RegisterScene(new Scene("default", this));
        }

        private Point MousePos;

        /// <summary>
        /// Frame timer and count to see the fps count
        /// </summary>
        private float FrameTimer;
        private int FrameCount;
        public int FPS;
        public bool DisplayFPS;
        public bool CalculeFPS;

        /// <summary>
        /// List of all the scenes in the game
        /// </summary>
        public Dictionary<int, Scene> Scenes { get; private set; }
        public Scene CurrentScene { get; private set; }

        /// <summary>
        /// This function checks the scenes and set the current scene
        /// </summary>
        /// <returns></returns>
        private bool ExecuteScenes()
        {
            if (CurrentScene == null)
            {
                if (Scenes.Count > 0)
                {
                    foreach (KeyValuePair<int, Scene> entry in Scenes)
                    {
                        if (!entry.Value.IsEnded)
                        {
                            CurrentScene = entry.Value;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Run spefic scene
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public bool GoToScene(Scene scene)
        {
            if (scene != null)
            {
                if (CurrentScene != null)
                {
                    CurrentScene.Ended();
                }
                CurrentScene = scene;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Register a scene to the engine scene with a default order of execution
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public bool RegisterScene(Scene scene)
        {
            if (scene == null)
                return false;

            Scenes.Add(Scenes.Count, scene);
            return true;
        }

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

        /// <summary>
        /// Screen Width
        /// </summary>
        /// <returns></returns>
        public int ScreenWidth()
        {
            return DrawingArea.Width - 1;
        }

        /// <summary>
        /// Screen Height
        /// </summary>
        /// <returns></returns>
        public int ScreenHeight()
        {
            return DrawingArea.Height - 1;
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
            DrawingArea.MouseDown += DrawingArea_MouseDown;
            DrawingArea.MouseUp += DrawingArea_MouseUp;
            DrawingArea.MouseMove += DrawingArea_MouseMove;
            EngineActive = OnCreate();
            timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            return EngineActive;
        }

        /// <summary>
        /// Stop The engine Thread and timer
        /// </summary>
        /// <returns></returns>
        private bool EndEngineThread()
        {
            timer.Stop();
            timer.Dispose();
            return true;
        }

        private void DrawingArea_MouseUp(object sender, MouseEventArgs e)
        {
            IsClicking = false;
            if (MouseButton.Contains(e.Button))
                MouseButton.Remove(e.Button);
        }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e)
        {
            IsClicking = true;
            if (!MouseButton.Contains(e.Button))
                MouseButton.Add(e.Button);
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            IsClicking = false;
            if (KeyboardKey.Contains(e.KeyCode))
                KeyboardKey.Remove(e.KeyCode);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            IsClicking = true;
            if (!KeyboardKey.Contains(e.KeyCode))
                KeyboardKey.Add(e.KeyCode);
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            MousePos = e.Location;
        }

        /// <summary>
        /// Get mouse Location on the drawing area
        /// </summary>
        /// <returns></returns>
        public Point MousePosition()
        {
            return MousePos;
        }

        /// <summary>
        /// Check if the mouse cursor is on top of a gameObject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool MouseOnTopOf(GameObject gameObject)
        {
            if (gameObject == null)
                return false;

            return (MousePosition().X < gameObject.Position.X + gameObject.Sprite.Width &&
               MousePosition().X + 1 > gameObject.Position.X &&
               MousePosition().Y < gameObject.Position.Y + gameObject.Sprite.Height &&
               MousePosition().Y + 1 > gameObject.Position.Y);
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

        /// <summary>
        /// Check if the user is clicking a specific key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool MouseClicked(MouseButtons keys)
        {
            return MouseButton.Contains(keys);
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

            //Execute Scenes
            ExecuteScenes();
            if(CurrentScene != null)
            {
                //Set the OnCreate function for scene
                if (!CurrentScene.IsCreated)
                {
                    CurrentScene.OnCreate();
                    CurrentScene.Created();
                }

                //Execute user logic
                CurrentScene.OnUpdate(ElapsedTime);
                //Get layers by z-order
                var sortedDict = from entry in CurrentScene.Layers orderby entry.Key ascending select entry;
                foreach (KeyValuePair<int, Layer> entry in sortedDict)
                {
                    //Draw registred game object
                    foreach (GameObject gameObject in entry.Value.RegistredObjects())
                    {
                        if (gameObject.ShowIt)
                            Drawer.GameObject(gameObject);
                    }
                }
            }

            //Draw registred game object
            foreach (GameObject gameObject in _GameObjects)
            {
                if (gameObject.ShowIt)
                    Drawer.GameObject(gameObject);
            }

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
                Drawer.String($"FPS: {FPS}", "Arial", 10f, Color.Red, new PointF(ScreenWidth() - 100, 10));
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

        /// <summary>
        /// Start the game
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            return EndEngineThread();
        }

        /// <summary>
        /// Get ressource path
        /// </summary>
        /// <param name="ressourceName"></param>
        /// <returns></returns>
        public string Ressources(string ressourceName)
        {
            return Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, $@"Resources\{ressourceName}");
        }
    }
}
