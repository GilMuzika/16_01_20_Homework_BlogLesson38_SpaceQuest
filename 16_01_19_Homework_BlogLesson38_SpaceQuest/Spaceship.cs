using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class Spaceship : PictureBase
    {
        private System.Windows.Forms.Timer _timer = new Timer();        

        public int WidthOfThis { get; }
        public int HeightOfThis { get; }

        public int NumberInArray { get; set; }

        public EventHandler<SpaceShipLocationEventArgs> MoveSpaceShipNow;

        public bool Good { get; set; } = false;
        public Spaceship(Bitmap spaceshipImage)
        {
            this.Image = spaceshipImage;
            this.Width = spaceshipImage.Width;
            this.Height = spaceshipImage.Height;

            this.WidthOfThis = spaceshipImage.Width;
            this.HeightOfThis = spaceshipImage.Height;


        }
        public void MoveMe(Bullet bullet, int mySpeed)
        {
            MoveSpaceShipNow?.Invoke(this, new SpaceShipLocationEventArgs { Spaceship = this, X = this.LocationOfThisX, Y = LocationOfThisY += mySpeed, Bullet = bullet });
        }
        public void Blink()
        {            
            _timer.Interval = 10;
            _timer.Start();            
            int count = 0;
            _timer.Enabled = false;
            _timer.Tick +=  (object sender, EventArgs e) => 
                {
                    
                    if (this.Visible) this.Visible = false;
                    else this.Visible = true;

                    count++;
                    if (count > 5) _timer.Stop();
                };

        }
        public void GoodShipGotDamaged()
        {
            this.Image = Properties.Resources.good_spaceship_damaged;
        }

        public void Explosion()
        {
            this.Image = Statics.ResizeImageProportionally(Properties.Resources.explosion_png_sprite_11_transparent, HeightOfThis);
            this.Width = HeightOfThis;
        }
        public void BadShipToNormal()
        {
            this.Image = Properties.Resources.bad_spaceship;
            if(this.InvokeRequired)
            {
                this.Invoke((Action)delegate { this.Width = WidthOfThis; });
            }
            else this.Width = WidthOfThis;
        }
        public void GoodShipToNormal()
        {
            this.Image = Properties.Resources.good_spaceship;
            this.Width = WidthOfThis;
        }

    }
}
