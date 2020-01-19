using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class LocationEventArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }

        public LocationEventArgs(int[] param)
        {
            X = param[0];
            Y = param[1];
        }

        public LocationEventArgs()
        {
        }
    }
}
