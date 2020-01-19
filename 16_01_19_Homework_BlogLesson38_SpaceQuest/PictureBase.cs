using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    public class PictureBase: PictureBox
    {
        protected int _locationOfThisX;
        protected int _locationOfThisY;
        public int LocationOfThisX
        {
            get => _locationOfThisX;
            set
            {
                _locationOfThisX = value;
                LocationOfThis = new Point(_locationOfThisX, _locationOfThisY);
            }
        }
        public int LocationOfThisY
        {
            get => _locationOfThisY;
            set
            {
                _locationOfThisY = value;
                LocationOfThis = new Point(_locationOfThisX, _locationOfThisY);
            }
        }
        protected Point LocationOfThis
        {
            get { return new Point(this.Location.X - this.Width / 2, this.Location.Y - this.Height / 2); }

            set 
            {
                if(this.InvokeRequired)
                {
                    this.Invoke((Action)delegate { this.Location = new Point(value.X - this.Width / 2, value.Y - this.Height / 2); } );
                }
                else this.Location = new Point(value.X - this.Width / 2, value.Y - this.Height / 2); 
            }

        }
    }
}
