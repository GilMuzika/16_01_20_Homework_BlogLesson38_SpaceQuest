using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    partial class MainForm
    {     
        private Random _rnd = new Random();        
        private void InitializeBadShips()
        {
            for (int i = 0; i < NUMBER_OF_BAD_SHIPS_AT_ONCE; i++)
            {
                Spaceship badship = new Spaceship(Properties.Resources.bad_spaceship);
                badship.LocationOfThisX = _rnd.Next(badship.Width / 2 + 2, this.ClientRectangle.Width - (badship.Width / 2 + 2));
                badship.LocationOfThisY = _rnd.Next(0 - badship.Height / 2 - 500, 0 - badship.Height / 2 - 50);
                badship.Name = "bad_ship_" + (i+1);
                badship.NumberInArray = i;
                _badShipsLst.Add(badship);                
                this.Controls.Add(badship);
            }
        }
        private void AddBadShip()
        {
            Spaceship badship = new Spaceship(Properties.Resources.bad_spaceship);
            badship.LocationOfThisX = _rnd.Next(badship.Width / 2 + 2, this.ClientRectangle.Width - (badship.Width / 2 + 2));
            badship.LocationOfThisY = _rnd.Next(0 - badship.Height / 2 - 500, 0 - badship.Height / 2 - 50);
            badship.Name = "bad_ship_" + (_badShipsLst.Count + 1);
            badship.NumberInArray = _badShipsLst.Count;
            badship.MoveSpaceShipNow += MoveSpaceShipNowEventHandler;
            _badShipsLst.Add(badship);
            this.Controls.Add(badship);
        }
        private void MoveSpaceShipNowEventHandler(object sender, SpaceShipLocationEventArgs e)
        {
            currentSpaceQuestGameManager.MoveSpaceShip(e.Spaceship, e.Bullet, e.X, e.Y);
        }

    }
}
