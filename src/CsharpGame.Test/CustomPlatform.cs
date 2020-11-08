using CsharpGame.Engine.Base;
using CsharpGame.Engine.Base.Cameras;
using CsharpGame.Engine.Platformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Test
{
    public class CustomPlatform : PlatformGame
    {
        //Character frames
        SpriteSheet Stand;
        SpriteSheet RunLeft;
        SpriteSheet RunRight;
        SpriteSheet JumpLeft;
        SpriteSheet JumpRight;

        PlatformerNode Another;
        //Audios
        Audio Jump;
        bool JumpSound = false;

        public CustomPlatform(Engine.Base.Engine engine) : base(engine)
        {
            //Generate empty Grid (Platform)
            RegisterGrid(new PlatformerGrid((engine.ScreenWidth() * 2) / 32, (engine.ScreenHeight() * 2) / 32, 32));
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

            //Another character to use a ref
            Another = new PlatformerNode(new System.Drawing.PointF(20*32, 20), Stand);
            Another.Animations.RegisterAnimation(Stand);
            Another.Animations.RegisterAnimation(RunLeft);
            Another.Animations.RegisterAnimation(RunRight);
            Another.Animations.RegisterAnimation(JumpLeft);
            Another.Animations.RegisterAnimation(JumpRight);
            Grid.addNode(Another);
            Another.SetAnimation("stand");
            //Hide the Grid
            ShowGrid = false;
            Jump = new Audio(Engine.Ressources("Jump.wav"));
        }

        public override bool Update(float ElapsedTime)
        {
            //Display the guide text
            Engine.Drawer.String("Use Keyborad to move", "Arial", 8, System.Drawing.Color.Black, new System.Drawing.PointF(1, 1));
            Engine.Drawer.String("Use mouse to draw lines", "Arial", 8, System.Drawing.Color.Black, new System.Drawing.PointF(1, 10));
            //Check if the player is not on ground to play the jumping audio
            if (!Character.onGround)
            {
                if (!JumpSound)
                {
                    JumpSound = true;
                    Jump.Play(false);
                }
                if (Character.Velocity.X < 0)
                    Character.SetAnimation("jump_left");
                else
                    Character.SetAnimation("jump_right");
            }
            else
            {
                JumpSound = false;
                if (Character.Velocity.X < 0)
                    Character.SetAnimation("run_left");
                else if(Character.Velocity.X > 0)
                    Character.SetAnimation("run_right");
                else
                    Character.SetAnimation("stand");
            }

            return true;
        }
    }
}
