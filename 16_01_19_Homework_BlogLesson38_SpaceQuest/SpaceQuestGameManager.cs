using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    class SpaceQuestGameManager
    {
        private int _goodSpaceShipHitPointMax;
        private int _currentLevel = 0;
        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }
        private int _goodSpaceShipHitPoints;
        public int GoodSpaceShipHitPoints
        {
            get { return _goodSpaceShipHitPoints; }
            set
            {
                _goodSpaceShipHitPoints = value;
                _goodSpaceShipHitPointMax = value;
            }
        }


        private int _shipXLocation;
        private int _shipYLocation;
        private int _numberOfBadShips;
        public int NumberOfBadShips { get => _numberOfBadShips; set => _numberOfBadShips = value; }
        private int _numberOfBadShipsInitial;        

        public event EventHandler<PointEventArgs> GoodSpaceShipHPChanged;
        public event EventHandler<SpaceShipLocationEventArgs> SpaceShipLocationChanged;        
        public event EventHandler<LocationEventArgs> GoodSpaceShipDestroyed;
        public event EventHandler<BadShipsExplodedEventArgs> BadShipExploded;
        public event EventHandler<LevelEventArgs> LevelUpReached;
        public event EventHandler<FiringEventArgs> SpaceShipFired;
        public SpaceQuestGameManager(int goodSpaceShipHitPoints, int shipXLocation, int shipYLocation, int numberOfBadShips)
        {
            _goodSpaceShipHitPoints = goodSpaceShipHitPoints;
            _goodSpaceShipHitPointMax = goodSpaceShipHitPoints;
            _shipXLocation = shipXLocation;
            _shipYLocation = shipYLocation;
            _numberOfBadShips = numberOfBadShips;
            _numberOfBadShipsInitial = numberOfBadShips;
        }
        public void OnEventOccured<T>(EventHandler<T> event_handler, T event_args)
        {
            if (event_handler != null)
            {
                event_handler.Invoke(this, event_args);
            }            
        }

        public void MoveSpaceShip(Spaceship spaceship, Bullet bullet, int newX, int newY)
        {
            _shipXLocation = newX;
            _shipYLocation = newY;

            OnEventOccured(SpaceShipLocationChanged, new SpaceShipLocationEventArgs { X = _shipXLocation, Y = _shipYLocation, Spaceship = spaceship, Bullet = bullet });
        }
        public void FireSpaceShip(bool good)
        {
            OnEventOccured(SpaceShipFired, new FiringEventArgs { Good = good });
        }


        public void GoodSpaceShipGotDamaged(int damage, string message, bool damaged)
        {
            _goodSpaceShipHitPoints -= damage;
            if (_goodSpaceShipHitPoints <= 0) OnEventOccured(GoodSpaceShipDestroyed, new LocationEventArgs { X = _shipXLocation, Y = _shipYLocation });
            else OnEventOccured(GoodSpaceShipHPChanged, new PointEventArgs { HitPoints = _goodSpaceShipHitPoints, HowMuchDamage = damage, Message = message, GotDamaged = damaged });
        }
        public void GoodSpaceShipGotExtraHipPoints(int extra)
        {
            _goodSpaceShipHitPoints += extra;
            OnEventOccured(GoodSpaceShipHPChanged, new PointEventArgs { HitPoints = _goodSpaceShipHitPoints });
        }
        public void EnemyShipDestroyed(Spaceship sheepDestroyed, int numberOfBadShipsDestroyed)
        {            
            _numberOfBadShips -= numberOfBadShipsDestroyed;

            if (_numberOfBadShips <= 0) 
            {
                _currentLevel++;                
                OnEventOccured(LevelUpReached, new LevelEventArgs { CurrentLevel = _currentLevel });                
                OnEventOccured(GoodSpaceShipHPChanged, new PointEventArgs { HitPoints = _goodSpaceShipHitPoints, Message = "עלית שלב!", HowMuchDamage = _goodSpaceShipHitPointMax - _goodSpaceShipHitPoints });
                _goodSpaceShipHitPoints = _goodSpaceShipHitPointMax;
                _numberOfBadShips = _numberOfBadShipsInitial;

            }

            OnEventOccured(BadShipExploded, new BadShipsExplodedEventArgs { NumberOfExplodedBadShips = _numberOfBadShips, ShipDestroyed = sheepDestroyed });
        }
    }
}
