# CsharpGame
A Simple 2D game engine in CSharp

## Attention 
If you used the first version, the main class was Core, now, its Engine.


# Concept
This engine run a timer to drawer on picturebox with System.Drawing.Graphic object. With a Detla time, so it work on every CPU with same speed, but lower/Higher FPS.

# Let's Start
To use this Library, first create a form projet (.NET 4.7 or higher), Then put picturebox on the form.
After that, create a class, call it what every you want, and inherit from Engine Class.

```C#
public class MyGame : Engine
{
    //ToDo
}
```
# Documentation

## Engine Class

The Engine class has to main function to override, OnCreate and OnUpdate.

### OnCreate

This function run once when game start, mainly it used to initilize game objects, or set defaut variables values...etc.

```C#
    //From pingpong project
    public override bool OnCreate()
    {
        PlayerSprite = new Sprite(30, 100);
        PlayerSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.bar);
        BallSprite = new Sprite(30, 30);
        BallSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.ball);
        Player = new GameObject(new PointF(10, 120), PlayerSprite);
        Ball = new GameObject(new PointF(120, 120), BallSprite);
        Rnd = new Random();
        ballSpeedX = Rnd.Next(-10, 10);
        ballSpeedY = Rnd.Next(-10, 10);
        RegisterGameObject(Player);
        RegisterGameObject(Ball);
        return true;
    }
```

### OnUpdate

This function run's every frame, this is where the logic of the game is implemented, like calcule, drawing, mouse and keyboard detection.

```C#
    //From pingpong project
    public override bool OnUpdate(double ElapsedTime)
    {
        //Drawer.GameObject(Player);
        //Drawer.GameObject(Ball);

        //Simple Logic
        Player.Position = new PointF(10, MousePosition().Y);
        //Dont go behind the screen dud
        if (Player.Position.Y >= (ScreenHeight() - Player.Sprite.Height))
            Player.Position = new PointF(10, ScreenHeight() - Player.Sprite.Height);

        if (Ball.Position.X <= 0)
        {
            ballSpeedX = Rnd.Next(0, 10);
            Score--;
        }

        if (Ball.Position.X >= (ScreenWith() - Ball.Sprite.Width))
            ballSpeedX = Rnd.Next(-10, 0);

        if (Ball.Position.Y <= 0)
            ballSpeedY = Rnd.Next(0, 10);

        if (Ball.Position.Y >= (ScreenHeight() - Ball.Sprite.Height))
            ballSpeedY = Rnd.Next(-10, 0);

        Drawer.Clear(Color.FromArgb(Red, Green, Blue));

        Ball.Position = new PointF(Ball.Position.X + ballSpeedX, Ball.Position.Y + ballSpeedY);

        //Collision
        if (Player.Position.X < Ball.Position.X + Ball.Sprite.Width &&
            Player.Position.X + Player.Sprite.Width > Ball.Position.X &&
            Player.Position.Y < Ball.Position.Y + Ball.Sprite.Height &&
            Player.Sprite.Height + Player.Position.Y > Ball.Position.Y)
        {
            ballSpeedX *= -1;
            Score++;
            Red = Rnd.Next(0, 255);
            Blue = Rnd.Next(0, 255);
            Green = Rnd.Next(0, 255);
        }

        Drawer.String($"Score : {Score}", "Arial", 10, Color.Black, new PointF((ScreenWith() / 2) - 5, 10)); 

        return true;
    }
```

### Others

#### RegisterGameObject

This function is used to add you game objects to the engine, so they will be drawed automatically wihtout you calling the Drawer Class.

```C#
    //From pingpong project
    public override bool OnCreate()
    {
        PlayerSprite = new Sprite(30, 100);
        PlayerSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.bar);
        Player = new GameObject(new PointF(10, 120), PlayerSprite);
        //Add the Player GameObject to the engine
        RegisterGameObject(Player);
        return true;
    }
```

#### ScreenWith & ScreenHeight

This function return the picturebox where the game is drawed Size.

```C#
    if (Ball.Position.X >= (ScreenWith() - Ball.Sprite.Width))
        ballSpeedX = Rnd.Next(-10, 0);

    if (Ball.Position.Y >= (ScreenHeight() - Ball.Sprite.Height))
        ballSpeedY = Rnd.Next(-10, 0);
```

#### MousePosition

This function return the Mouse position relative to the picturebox.

```C#
Player.Position = new PointF(10, MousePosition().Y);
```

#### MouseClicked

This function return a bool value, when a mouse button is clicked.

```C#
    if (MouseClicked(MouseButtons.Left))
        Drawer.String("Mouse left button clicked", "Arial", 10, Color.Black, new PointF(10, 10));
```

#### KeyClicked

This function return a bool value if a specific key is clicked

```C#
    if (KeyClicked(Keys.Escape))
        return false;
```

### Public Properties

FPS properties are public, you can display them where every you want. By default the FPS is not calculed. To display the FPS by default, set the DispalyFPS property to true and the CalculeFPS to true.
Also you can get the FPS propety as Integer.

```C#
    public MyGame(PictureBox drawingArea) : base(drawingArea) 
    { 
        DisplayFPS = true; 
        CalculeFPS = true; 
    }
```

## Drawer

This class used to draw everything to the picturebox, like sprites, pictures, string...

### Clear

This function clean the picturebox with a color of you choice.
```C#
Drawer.Clear(Color.Black);
```

### Sprite

This function draw sprite object. It has two signatures, one with Camera and the other without.

```C#
public bool Sprite(PointF point, Sprite sprite){}
public bool Sprite(PointF point, Sprite sprite, Camera camera){}
```

This Function also can draw spritesheet objects.

### SpriteSheet

This function draw spritesheet object. It has two signatures, one with Camera and the other without.

```C#
public bool SpriteSheet(float x, float y, SpriteSheet sprite){}
public bool SpriteSheet(float x, float y, SpriteSheet sprite, Camera camera){}
public bool SpriteSheet(PointF point, SpriteSheet sprite){}
public bool SpriteSheet(PointF point, SpriteSheet sprite, Camera camera){}
```

### GameObject

This function draw gameobjects. It has two signatures, one with Camera and the other without.

```C#
public bool GameObject(GameObject obj){}
public bool GameObject(GameObject obj, Camera camera){}
```

### Line

This function allow you to draw a line between two points. It has two signatures, one with Camera and the other without.

```C#
public bool Line(Point p1, Point p2, Color color){}
public bool Line(Point p1, Point p2, Color color, Camera camera){}
```

### Rectangle
This function allow to draw a Rectangle. It has two signatures, one with Camera and the other without.
By default, only the perimeter is drawed, to fill the rectangle, set the fill option to true.

```C#
public bool Rectangle(float x, float y, float with, float height, Color color, bool fill=false){}
public bool Rectangle(float x, float y, float with, float height, Color color, Camera camera, bool fill = false){}
```

### String
This function allow you draw a string to screen. It has two signatures, one with Camera and the other without.

```C#
public bool String(string text, string fontfamily, float size , Color color, PointF position){}
public bool String(string text, string fontfamily, float size, Color color, PointF position, Camera camera){}
```

### Circle

This function allow you to draw a Circle to screen. It has two signatures, one with Camera and the other without.

```C#
public bool Circle(float x, float y, float with, float height, Color color){}
public bool Circle(PointF position, float with, float height, Color color){}
public bool Circle(float x, float y, float with, float height, Color color, Camera camera){}
public bool Circle(PointF position, float with, float height, Color color, Camera camera){}
```

## Camera

This function allows to create games with world bigger then the picturebox Size.
For now, there are two Camera to use:
- FixedCamera : this only take a size smaller or equal to the picturebox size, so the game will stay only in the screen, but only the objects in the camera range with be displayed.

```C#
public Camera Camera { get; set; }
//Example
Camera = new FixedCamera(200, 200, Grid.Width * Grid.Resolution, Grid.Height * Grid.Resolution);
```
- WorldCamera : This camera take the size of the picturebox and you can move the world around, or follow a player around the game world.

```C#
public Camera Camera { get; set; }
//Example
Camera = new WorldCamera(_Core.ScreenWith(), _Core.ScreenHeight(), Grid.Width * Grid.Resolution, Grid.Height * Grid.Resolution);
```

You can create you own camera by inheriting the Camera Class.

```C#
    public class CustomCamera : Camera
    {
        public PointF Location { get; set; }
        public CustomCamera(int screenWidth, int screenHeight, int levelWidth, int levelHeight, float speed = 10) : base(screenWidth, screenHeight, levelWidth, levelHeight, speed)
        {
            Position = new PointF(0, 0);
            Location = new PointF(0, 0);
        }

        /// <summary>
        /// Set the position to a gameObject
        /// </summary>
        /// <param name="object"></param>
        public override void SetPositionTo(GameObject @object)
        {
            Location = @object.Position;
            getOffset();
        }

        /// <summary>
        /// Get the camera offset to set the world movements
        /// </summary>
        /// <returns></returns>
        public override PointF getOffset()
        {
            Offset = new PointF((CameraSize.Width / 2) - Location.X, (CameraSize.Height / 2) - Location.Y);
            //Check offset
            return Offset;
        }
    }
```

### Functions to override

You can override some functions to your needs in your Custom Camera.

- SetPositionTo : By default, this function used to follow an object in the game
- getOffset : By default, the offset that can be added to the world to move around, but if you create a new camera, you have to override this function.
- UpdateMaxPosition : By default, this function get the right bottom point of the camera.

## Sprites

You can use this object to draw images to screen. Aslo, GameObject use sprites for it graphic parte, means that you need a sprite to create GameObject.
Sprite have only Size and picture file.

```C#
    public Sprite PlayerSprite { get; private set; }
    public override bool OnCreate()
    {
        PlayerSprite = new Sprite(30, 100);
        //Getting the file from project resources
        PlayerSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.bar);
    }
```

## SpriteSheet

This object is used to draw objects with animation. SpriteSheet is a collection of Sprites arranged in a grid. The Sprites are then compiled into an Animation Clip that will play each Sprite in order to create the animation, much like a flipbook.

```C#
    if (Camera is WorldCamera)
        Engine.Drawer.SpriteSheet(new System.Drawing.Point((int)node.Position.X + (int)Camera.Offset.X, (int)node.Position.Y + (int)Camera.Offset.Y), (SpriteSheet)node.Sprite, Camera);
    else
        Engine.Drawer.SpriteSheet(new System.Drawing.Point((int)node.Position.X, (int)node.Position.Y), (SpriteSheet)node.Sprite, Camera);
```

## GameObject

The difference between Sprite and GameObject is the position, by default GameObeject are objects that will be moving or have any animation.
To make them simple, you can define a GameObject and registred to the Engine, so it will be drawed by default, without you set that manually.

```C#
    public GameObject Player { get; private set; }
    public override bool OnCreate()
    {
        PlayerSprite = new Sprite(30, 100);
        PlayerSprite.LoadFromFile(CSharpGame.PingPong.Properties.Resources.bar);
        Player = new GameObject(new PointF(10, 120), PlayerSprite);
        RegisterGameObject(Player);
        return true;
    }
```

## Animation

Animation is a collection of SpriteSheets saved with a name. This will help you run different animations according to one or more conditions.

```C#
    //Character frames
    SpriteSheet Stand;
    SpriteSheet RunLeft;
    SpriteSheet RunRight;
    SpriteSheet JumpLeft;
    SpriteSheet JumpRight;

    public CustomPlatform(Engine.Base.Engine engine) : base(engine)
    {
        //Generate empty Grid (Platform)
        RegisterGrid(new PlatformerGrid((engine.ScreenWith() * 2) / 32, (engine.ScreenHeight() * 2) / 32, 32));
        //Init the character animations and frames
        Stand = new SpriteSheet("stand", 32, 32, 1, 5, 5, Test.Properties.Resources.charachter);
        RunLeft = new SpriteSheet("run_left", 32, 32, 15, 6, 9, Test.Properties.Resources.charachter);
        RunRight = new SpriteSheet("run_right", 32, 32, 15, 0, 3, Test.Properties.Resources.charachter);
        JumpLeft = new SpriteSheet("jump_left", 32, 32, 5, 10, 10, Test.Properties.Resources.charachter);
        JumpRight = new SpriteSheet("jump_right", 32, 32, 5, 4, 4, Test.Properties.Resources.charachter);
        //Set the spawn position to 200
        PLAYER_SPAWN_Y = 600;
        //Create character and set the animation
        Character = new PlatformCharacter(new System.Drawing.PointF(PLAYER_SPAWN_X, PLAYER_SPAWN_Y), Stand);
        Character.Animations.RegisterAnimation(Stand);
        Character.Animations.RegisterAnimation(RunLeft);
        Character.Animations.RegisterAnimation(RunRight);
        Character.Animations.RegisterAnimation(JumpLeft);
        Character.Animations.RegisterAnimation(JumpRight);
        //Add character to the platform
        Grid.addNode(Character);
        //Default animation
        Character.SetAnimation("stand");
    }

    public override bool Update(float ElapsedTime)
    {
        if (!Character.onGround)
        {
            if (Character.Velocity.X < 0)
                Character.SetAnimation("jump_left");
            else
                Character.SetAnimation("jump_right");
        }
        else
        {
            if (Character.Velocity.X < 0)
                Character.SetAnimation("run_left");
            else if(Character.Velocity.X > 0)
                Character.SetAnimation("run_right");
            else
                Character.SetAnimation("stand");
        }

        return true;
    }
```

## Audio

To play sound effects or music, you can use the Audio object.

### Attention

This object is still not on point, if any bug or suggestion, i will take it.

```C#
    //Audios
    Audio Jump;
    bool JumpSound = false;

    public CustomPlatform(Engine.Base.Engine engine) : base(engine)
    {
        Jump = new Audio(Engine.Ressources("Jump.wav"));
    }

    public override bool Update(float ElapsedTime)
    {
        //Check if the player is not on ground to play the jumping audio
        if (!Character.onGround)
        {
            if (!JumpSound)
            {
                JumpSound = true;
                Jump.Play(false); //False to play on loop
            }
        }
        else
        {
            JumpSound = false;
        }

        return true;
    }

```

# More

For now, there are two define game concepts, Platform game, and map tile game. You can run them to test the engine (use the Test project).

One more project is the pingpong, was made for making a video, you can use also.
 