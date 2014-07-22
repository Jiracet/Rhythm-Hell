using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmHell
{
    class GameTime
    {
        public GameTime()
        {
            //initialize game time
            Frame = 0;
        }

        public int Frame { get; set; }

        public int Seconds
        {
            get { return (int)(Math.Floor((double)Frame / 60)); }
            private set { }
        }

        public int Minutes
        {
            get { return (int)(Math.Floor((double)Frame / 3600) % 60); }
            private set { }
        }

        public int Milliseconds
        {
            get { return (int)(Math.Floor((double)Frame / 0.6)); }
            private set { }
        }
    }
}
