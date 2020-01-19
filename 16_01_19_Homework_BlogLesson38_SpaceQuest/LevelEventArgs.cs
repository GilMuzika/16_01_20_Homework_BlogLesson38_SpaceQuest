using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class LevelEventArgs: EventArgs
    {
        public int CurrentLevel { get; set; }

        public LevelEventArgs(int[] param)
        {
            CurrentLevel = param[0];
        }

        public LevelEventArgs()
        {
        }
    }
}
