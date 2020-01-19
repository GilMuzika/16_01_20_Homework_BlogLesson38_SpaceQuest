using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class SpaceShipLocationEventArgs: LocationEventArgs
    {
        public Spaceship Spaceship { get; set; }
        public Bullet Bullet { get; set; }
    }
}
