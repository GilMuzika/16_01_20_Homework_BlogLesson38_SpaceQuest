using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class PointEventArgs: EventArgs
    {
        public int HitPoints { get; set; }
        public int HowMuchDamage { get; set; }
        public string Message { get; set; }
        public bool GotDamaged { get; set; } = false;

        public PointEventArgs(int[] param)
        {
            HitPoints = param[0];
        }

        public PointEventArgs()
        {
        }
    }
}
