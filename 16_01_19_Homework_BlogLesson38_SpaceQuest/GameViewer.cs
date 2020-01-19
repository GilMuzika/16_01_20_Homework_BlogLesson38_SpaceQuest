using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class GameViewer
    {
        public event EventHandler<PointEventArgs> GoodSpaceShipHPChanged;
        public event EventHandler<SpaceShipLocationEventArgs> SpaceShipLocationChanged;
        public  EventHandler<SpaceShipLocationEventArgs> GoodSpaceShipBulletLocationChanged;
        public event EventHandler<FiringEventArgs> SpaceShipFired;
        public event EventHandler<LocationEventArgs> GoodSpaceShipDestroyed;
        public event EventHandler<BadShipsExplodedEventArgs> BadShipExploded;
        public event EventHandler<LevelEventArgs> LevelUpReached;

        public void LevelUpReachedEventHandler(object sender, LevelEventArgs e)
        {
            LevelUpReached?.Invoke(sender, e);
        }
          
        public void GoodSpaceShipDestroyedEventHandler(object sender, LocationEventArgs e)
        {
            GoodSpaceShipDestroyed?.Invoke(sender, e);
        }
        public void GoodSpaceShipHPChangedEventHandler(object sender, PointEventArgs e)
        {
            GoodSpaceShipHPChanged?.Invoke(sender, e);
        }
        public void BadShipExplodedEventHandler(object sender, BadShipsExplodedEventArgs e)
        {
            BadShipExploded?.Invoke(sender, e);
        }
        public void googSpaceShipLocationChangedEventHandler(object sender, SpaceShipLocationEventArgs e)
        {
            SpaceShipLocationChanged?.Invoke(sender, e);
        }
        public void GoodSpaceShipBulletLocationChangedEventHandler(object sender, SpaceShipLocationEventArgs e)
        {
            GoodSpaceShipBulletLocationChanged?.Invoke(sender, e);
        }
        public void SpaceShipFiredEventhandler(object sender, FiringEventArgs e)
        {
            SpaceShipFired?.Invoke(sender, e);
        }


    }
}
