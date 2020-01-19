using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class BadShipsExplodedEventArgs: EventArgs
    {
        public int NumberOfExplodedBadShips { get; set; }
        public Spaceship ShipDestroyed { get; set; }
    }
}
