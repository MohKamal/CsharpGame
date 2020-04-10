using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Engine.Base
{
    public class Audio
    {
        private SoundPlayer Player;
        private Stream _Stream;
        private string _File;

        public Audio(string file)
        {
            _File = file;
            Player = new SoundPlayer(file);
            Player.Load();
        }

        public Audio(Stream file)
        {
            _Stream = file;
            Player = new SoundPlayer(file);
            Player.Load();
        }

        public void Play()
        {
            Player.Play();
        }
    }
}
