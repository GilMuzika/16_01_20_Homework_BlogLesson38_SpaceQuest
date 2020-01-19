using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    public partial class MainForm : Form
    {
        private const int BAD_SHIPS_NUM = 3;
        private const int INITIAL_BAD_SHIP_SPEED = 3;
        private int BAD_SHIP_SPEED;
        private const int INITIAL_HIT_POINTS = 50;
        private int NUMBER_OF_BAD_SHIPS_AT_ONCE;
        private const int INITIAL_NUMBER_OF_BAD_SHIPS_AT_ONCE = 3;
        private List<Spaceship> _badShipsLst = new List<Spaceship>();

        private bool _keyLeft = false;
        private bool _keyRight = false;

        private Bullet _goodSpaceShipBulllet = new Bullet();     

        private int _goodSpaceShipMovingInterval = 0;
        private int _goodSpaceShipBulletMovingInterval = 0;

        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        
        private int goodSpaceShipLocationX;
        private int goodSpaceShipLocationY;
        

        private Spaceship _goodSpaceShip = new Spaceship(Properties.Resources.good_spaceship);                

        private SpaceQuestGameManager currentSpaceQuestGameManager;
        private GameViewer currentGameViewer = new GameViewer();
        public MainForm()
        {
            InitializeComponent();
            BAD_SHIP_SPEED = INITIAL_BAD_SHIP_SPEED;
            NUMBER_OF_BAD_SHIPS_AT_ONCE = INITIAL_NUMBER_OF_BAD_SHIPS_AT_ONCE;
            InitializeBadShips();
            InitializeGame();            
        }
        private void InitializeGame()
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, 0);
            this.StartPosition = FormStartPosition.Manual;

            lblNewGame.drawBorder(2, Color.Blue);
            lblNewGame.Cursor = Cursors.Hand;
            lblNewGame.Visible = false;




            this.pcbHitPoints.WriteNumberMessages(INITIAL_HIT_POINTS, "Your hip points: ");            
            this.pcbMessages.SendToBack();
            this.pcbLevel.SendToBack();

            _timer.Interval = 10;
            _timer.Start();

            currentSpaceQuestGameManager = new SpaceQuestGameManager(INITIAL_HIT_POINTS, this.ClientRectangle.Width / 2, this.ClientRectangle.Height - _goodSpaceShip.Height / 2 - 2, INITIAL_NUMBER_OF_BAD_SHIPS_AT_ONCE);
            this.pcbLevel.WriteNumberMessages(currentSpaceQuestGameManager.CurrentLevel, "Your current level:");
            this.pcbHitPoints.SendToBack();

            foreach (Spaceship s in _badShipsLst)
            {
                s.MoveSpaceShipNow += MoveSpaceShipNowEventHandler;
            }
            
            currentSpaceQuestGameManager.SpaceShipLocationChanged += currentGameViewer.googSpaceShipLocationChangedEventHandler;
            currentSpaceQuestGameManager.SpaceShipLocationChanged += currentGameViewer.GoodSpaceShipBulletLocationChangedEventHandler;
            currentSpaceQuestGameManager.BadShipExploded += currentGameViewer.BadShipExplodedEventHandler;
            currentGameViewer.BadShipExploded += (object sender, BadShipsExplodedEventArgs e) => 
                {
                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Enabled = false;
                    timer.AutoReset = false;
                    timer.Elapsed += (object sender2, ElapsedEventArgs e2) => 
                        { 
                            e.ShipDestroyed.BadShipToNormal();
                            SpaceShipToUpperLocation(e.ShipDestroyed);                            
                            e.ShipDestroyed.MoveSpaceShipNow = MoveSpaceShipNowEventHandler;
                            timer.Stop();
                        };
                    timer.Interval = 200;
                    timer.Start();
                    e.ShipDestroyed.MoveSpaceShipNow -= MoveSpaceShipNowEventHandler;                    
                    e.ShipDestroyed.Explosion();
                    
                };
            currentSpaceQuestGameManager.GoodSpaceShipHPChanged += currentGameViewer.GoodSpaceShipHPChangedEventHandler;
            currentGameViewer.GoodSpaceShipHPChanged += (object sender, PointEventArgs e) => 
                {                    
                    pcbHitPoints.WriteNumberMessages(e.HitPoints, "Your hip points:");
                    if(e.GotDamaged) _goodSpaceShip.GoodShipGotDamaged();
                    this.pcbMessages.BringToFront();
                    string secondMessage = string.Empty;
                    if (e.Message.Equals("עלית שלב!")) secondMessage = $"קיבלת תוספת {e.HowMuchDamage} נקודות";
                    else secondMessage = $"מינוס {e.HowMuchDamage} נקודות";

                    this.pcbMessages.WriteMessages(e.Message, 52, secondMessage, 20);

                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Enabled = false;
                    timer.Interval = 1000;
                    timer.Tick += (object sender2, EventArgs e2) => 
                        {
                            if (e.GotDamaged) _goodSpaceShip.GoodShipToNormal();
                            this.pcbMessages.SendToBack();
                            this.pcbMessages.Image = null;
                            timer.Stop();
                        };
                    timer.Start();
                };
            currentSpaceQuestGameManager.GoodSpaceShipDestroyed += currentGameViewer.GoodSpaceShipDestroyedEventHandler;
            currentGameViewer.GoodSpaceShipDestroyed += GameOverEventHandler;
            currentGameViewer.GoodSpaceShipDestroyed += (object sender, LocationEventArgs e) => 
                {
                    pcbMessages.WriteMessages("Game over!", 52, "!סגור ת'בסטה", 20);
                };
            currentGameViewer.GoodSpaceShipBulletLocationChanged += MoveGoodSpaceShipBulletMainFormEventHandler;
            currentSpaceQuestGameManager.SpaceShipFired += currentGameViewer.SpaceShipFiredEventhandler;
            currentGameViewer.SpaceShipFired += (object sender, FiringEventArgs e) => 
                {
                    currentGameViewer.GoodSpaceShipBulletLocationChanged -= MoveGoodSpaceShipBulletMainFormEventHandler;
                    _goodSpaceShipBulletMovingInterval = -10;
                };
            currentSpaceQuestGameManager.LevelUpReached += currentGameViewer.LevelUpReachedEventHandler;
            currentGameViewer.LevelUpReached += (object sender, LevelEventArgs e) =>
                {
                    this.pcbLevel.WriteNumberMessages(currentSpaceQuestGameManager.CurrentLevel, "Your current level:");                    
                    NUMBER_OF_BAD_SHIPS_AT_ONCE++;
                    BAD_SHIP_SPEED++;
                    currentSpaceQuestGameManager.NumberOfBadShips = NUMBER_OF_BAD_SHIPS_AT_ONCE;
                    this.AddBadShip();

                };


            _goodSpaceShip.Name = "good_SpaceShip";
            _goodSpaceShip.Good = true;
            _goodSpaceShip.LocationOfThisX = this.ClientRectangle.Width / 2;
            goodSpaceShipLocationX = _goodSpaceShip.LocationOfThisX;
            _goodSpaceShip.LocationOfThisY = this.ClientRectangle.Height - _goodSpaceShip.Height / 2 - 2;
            goodSpaceShipLocationY = _goodSpaceShip.LocationOfThisY;
            this.Controls.Add(_goodSpaceShip);
            _goodSpaceShipBulllet.LocationOfThisX = _goodSpaceShip.LocationOfThisX;
            _goodSpaceShipBulllet.LocationOfThisY = _goodSpaceShip.LocationOfThisY;
            this.Controls.Add(_goodSpaceShipBulllet);

            _timer.Tick += (object sender, EventArgs e) => 
                {
                    if (_goodSpaceShip.LocationOfThisX >= this.ClientRectangle.Width - _goodSpaceShip.Width / 2 - 10 && _keyRight == true) _goodSpaceShipMovingInterval = 0;
                    else if (_goodSpaceShip.LocationOfThisX <= _goodSpaceShip.Width / 2 + 10 && _keyLeft == true) _goodSpaceShipMovingInterval = 0;

                    int goodSpaceShipX = _goodSpaceShip.LocationOfThisX += _goodSpaceShipMovingInterval;

                    currentSpaceQuestGameManager.MoveSpaceShip(_goodSpaceShip, _goodSpaceShipBulllet, goodSpaceShipX, _goodSpaceShip.LocationOfThisY);
                    MoveBulletOnFiring(_goodSpaceShipBulllet, _goodSpaceShipBulllet.LocationOfThisX, _goodSpaceShipBulllet.LocationOfThisY += _goodSpaceShipBulletMovingInterval);
                    foreach(Spaceship s in _badShipsLst)
                    {
                        s.MoveMe(null, BAD_SHIP_SPEED);
                    }
                    AnimateBadShips();
                };

            this.KeyDown += new KeyEventHandler((object sender, KeyEventArgs e) => 
                {                    
                    switch(e.KeyCode)
                    {                        
                        case Keys.Left: _goodSpaceShipMovingInterval = -10; _keyLeft = true; _keyRight = false; break;
                        case Keys.Right: _goodSpaceShipMovingInterval = 10; _keyLeft = false; _keyRight = true;  break;                        
                    }                    
                });
            this.KeyUp += (object sender, KeyEventArgs e) => { if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) _goodSpaceShipMovingInterval = 0; _keyLeft = false; _keyRight = false; };

            this.KeyPress += (object sender, KeyPressEventArgs e) => 
                {
                    if (e.KeyChar == (char)Keys.Space)
                    {                        
                        currentSpaceQuestGameManager.FireSpaceShip(_goodSpaceShip.Good);
                    }
                };

            lblNewGame.Click +=  (object sender2, EventArgs e2) => 
                {
                    (sender2 as Label).Visible = false;
                    pcbMessages.SendToBack();
                    currentSpaceQuestGameManager.GoodSpaceShipHitPoints = INITIAL_HIT_POINTS;
                    currentSpaceQuestGameManager.CurrentLevel = 0;
                    currentSpaceQuestGameManager.NumberOfBadShips = INITIAL_NUMBER_OF_BAD_SHIPS_AT_ONCE;
                    pcbMessages.Image = null;
                    pcbHitPoints.WriteNumberMessages(currentSpaceQuestGameManager.GoodSpaceShipHitPoints, "Your hit points:");
                    pcbLevel.WriteNumberMessages(currentSpaceQuestGameManager.CurrentLevel, "Your current level:");
                    _goodSpaceShip.GoodShipToNormal();
                    for (int j = 0; j < _badShipsLst.Count * 10; j++)
                    {
                        for (int i = 0; i < _badShipsLst.Count; i++) if (!_badShipsLst[i].Good) { this.Controls.Remove(_badShipsLst[i]); }
                    }
                    BAD_SHIP_SPEED = INITIAL_BAD_SHIP_SPEED;
                    NUMBER_OF_BAD_SHIPS_AT_ONCE = INITIAL_NUMBER_OF_BAD_SHIPS_AT_ONCE;
                    _badShipsLst.Clear();
                    InitializeBadShips();
                    foreach (Spaceship s in _badShipsLst)
                    {
                        s.MoveSpaceShipNow += MoveSpaceShipNowEventHandler;
                    }



                };
            


        }
        private void MoveGoodSpaceShipBulletMainFormEventHandler(object sender, SpaceShipLocationEventArgs e)
        {
            if (e.Bullet != null)
            {
                e.Bullet.LocationOfThisX = e.X;
                e.Bullet.LocationOfThisY = e.Y;
            }
        }
        private void BulletUsedUp()
        {
            _goodSpaceShipBulletMovingInterval = 0;
            _goodSpaceShipBulllet.LocationOfThisX = _goodSpaceShip.LocationOfThisX;
            _goodSpaceShipBulllet.LocationOfThisY = _goodSpaceShip.LocationOfThisY;
            currentGameViewer.GoodSpaceShipBulletLocationChanged = MoveGoodSpaceShipBulletMainFormEventHandler;
        }
        private void MoveBulletOnFiring(Bullet bullet, int bulletLocFactorX, int bulletLocFactorY)
        {
            bullet.LocationOfThisX = bulletLocFactorX;
            bullet.LocationOfThisY = bulletLocFactorY;
            if (bullet.LocationOfThisY <= -5) 
            {
                BulletUsedUp();
            }
        }
        private void AnimateBadShips()
        {            
            for(int i = 0; i < _badShipsLst.Count; i++)
            {
                if (_badShipsLst[i].LocationOfThisY > this.ClientRectangle.Height + _badShipsLst[i].Height / 2 + 2)
                {
                    currentSpaceQuestGameManager.GoodSpaceShipGotDamaged(5, "!פיספסת חללית אויב", false);
                    SpaceShipToUpperLocation(_badShipsLst[i]);
                }
                if(_badShipsLst[i].Bounds.IntersectsWith(_goodSpaceShipBulllet.Bounds))
                {                    
                    currentSpaceQuestGameManager.EnemyShipDestroyed(_badShipsLst[i], 1);
                    BulletUsedUp();
                }
                if(_badShipsLst[i].Bounds.IntersectsWith(_goodSpaceShip.Bounds))
                {
                    currentSpaceQuestGameManager.GoodSpaceShipGotDamaged(10, "!איזה לוזר, התפוצצת", true);
                    SpaceShipToUpperLocation(_badShipsLst[i]);
                }

            }
        }
        private void SpaceShipToUpperLocation(Spaceship spaceship)
        {
            spaceship.LocationOfThisY = _rnd.Next(0 - spaceship.Height / 2 - 500, 0 - spaceship.Height / 2 - 50);
            spaceship.LocationOfThisX = _rnd.Next(spaceship.Width / 2 + 2, this.ClientRectangle.Width - (spaceship.Width / 2 + 2));
        }

        private void GameOverEventHandler(object sender, LocationEventArgs e)
        {            
            pcbHitPoints.WriteNumberMessages(currentSpaceQuestGameManager.GoodSpaceShipHitPoints, "Your hit points:");
            currentSpaceQuestGameManager.GoodSpaceShipHitPoints = INITIAL_HIT_POINTS;
            BAD_SHIP_SPEED = INITIAL_BAD_SHIP_SPEED;            
            _goodSpaceShip.Explosion();
            lblNewGame.Visible = true;
            foreach (Spaceship s in _badShipsLst)
            {
                s.MoveSpaceShipNow -= MoveSpaceShipNowEventHandler;
                SpaceShipToUpperLocation(s);
            }
            this.pcbMessages.SendToBack();
        }

    }
}
