using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    public class SpriteSheet : Sprite
    {
        private int CurrentFrame;
        private string _Name;
        private int Counter;
        private int FrameSpeed;
        private int StartFrame;
        private int EndFrame;
        private int FramesPerRow;
        private List<int> AnimationSequence;

        public string Name { get => _Name; }

        public SpriteSheet(string name, float width, float height, int frameSpeed, int startframe, int endframe, Bitmap file) : base(width, height)
        {
            _Name = name.ToLower();
            CurrentFrame = 0;
            Counter = 0;
            FrameSpeed = frameSpeed;
            EndFrame = endframe;
            StartFrame = startframe;
            AnimationSequence = new List<int>();
            // create the sequence of frame numbers for the animation
            for (var frameNumber = StartFrame; frameNumber <= EndFrame; frameNumber++)
                AnimationSequence.Add(frameNumber);
            LoadFromFile(file);
            FramesPerRow = (int)Math.Floor((double)(Graphic.Width / Width));
        }

        public void Update()
        {
            // update to the next frame if it is time
            int animCount = AnimationSequence.Count;
            if (animCount == 0)
                animCount = 1;
            if (Counter == (FrameSpeed - 1))
                CurrentFrame = (CurrentFrame + 1) % animCount;

            // update the counter
            Counter = (Counter + 1) % FrameSpeed;
        }
        

        public Point Frame()
        {
            // get the row and col of the frame
            int row = (int)Math.Floor((double)(AnimationSequence[CurrentFrame] / FramesPerRow));
            int col = (int)Math.Floor((double)(AnimationSequence[CurrentFrame] % FramesPerRow));
            return new Point((int)(col * Width), (int)(row * Height));
        }
    }
}
