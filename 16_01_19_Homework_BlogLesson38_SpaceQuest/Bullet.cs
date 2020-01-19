using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class Bullet: PictureBase
    {

        public Bullet()
        {
            this.Width = BulletImage().Width;
            this.Height = BulletImage().Height;
            this.Image = BulletImage();
        }
        private Bitmap BulletImage()
        {
            Bitmap forPicture = new Bitmap(4, 8);
            Graphics _graphicsObj = Graphics.FromImage(forPicture);
            Brush myBrush = new SolidBrush(Color.Black);
            _graphicsObj.FillRectangle(myBrush, 0 ,0, forPicture.Width, forPicture.Height);
            _graphicsObj.Dispose();
            return forPicture;
        }
    }

}
