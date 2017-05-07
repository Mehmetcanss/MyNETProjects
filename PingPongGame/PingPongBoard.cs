using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PingPongGame
{
    public class PingPongBoard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double speed = 10;
        private Racket _player;
        private double _width;
        private double _height;
        private Racket _enemy;
        private AggressiveRacket _superEnemy;

        private CanvasShape _currentEnemy;

        public CanvasShape CurrentEnemy
        {
            get { return _currentEnemy; }
            set { _currentEnemy = value; }
        }


        public AggressiveRacket SuperEnemy
        {
            get { return _superEnemy; }
            set
            {
                _superEnemy = value;
                Notify("SuperEnemy");
            }
        }


        private Stopwatch st;


        private Ball _ball;
        private DispatcherTimer timer;
        private MediaPlayer _mediaPlayer = new MediaPlayer();
        Random random = new Random();


        public Ball Ball
        {
            get { return _ball; }
            set
            {
                _ball = value;
                Notify("Ball");
            }
        }

        private bool _godMode;

        public bool GodMode
        {
            get { return _godMode; }
            set
            {
                _godMode = value;
                Notify("GodMode");
            }
        }


        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                Notify("Width");
            }
        }



        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                Notify("Height");
            }
        }


        private double _top;

        public double Top
        {
            get { return _top; }
            set { _top = value; }
        }



        public Racket Enemy
        {
            get { return _enemy; }
            set
            {
                _enemy = value;
                Notify("Enemy");
            }
        }


        public Racket Player
        {
            get { return _player; }
            set
            {
                _player = value;
                Notify("Player");
            }
        }

        public PingPongBoard()
        {
            this.Player = new Racket(25, 90);

            this.Enemy = new Racket(25, 80);

            this.CurrentEnemy = Enemy;

            InitCommands();
        }

        private ICommand _godModeCommand;

        public ICommand GodModeCommand
        {
            get { return _godModeCommand; }
            set
            {
                _godModeCommand = value;
                Notify("GodModeCommand");
            }
        }



        private void InitCommands()
        {
            this.MouseMoveCommand = new CustomCommand((obj) => { return true; }, MouseMove);
            this.WindowLoadedCommand = new CustomCommand((obj) => { return true; }, WindowLoaded);
            this.GodModeCommand = new CustomCommand((obj) => { return true; }, TurnOnGodMode);
        }

        private void Notify(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            double ballTop = Ball.Top;


            //ball hit south bound
            if (ballTop + Ball.Height > this.Height)
            {
                Ball.VerticalDirection = VerticalDirection.Up;

                //ball hit north bound
            }
            else if (ballTop <= 1)
            {
                Ball.VerticalDirection = VerticalDirection.Down;
            }

            if (HitPlayer(Ball))
            {
                Ball.HorizontalDirection = HorizontalDirection.Right;
                _mediaPlayer.Open(new Uri("C:/Users/MSI/MyProjects/TestProjects/PingPongGame/Resources/bong.mp3"));
                _mediaPlayer.Play();

            }
            else if (HitEnemy())
            {
                Ball.HorizontalDirection = HorizontalDirection.Left;
                _mediaPlayer.Open(new Uri("C:/Users/MSI/MyProjects/TestProjects/PingPongGame/Resources/bong.mp3"));
                _mediaPlayer.Play();
            }

            if (Ball.Left < speed * -1 || Ball.Left > this.Width + speed)
            {
                MessageBox.Show("You Lost You Fucking Loser!!!");
                RestartGame();

            }

            if (Ball.HorizontalDirection == HorizontalDirection.Right)
            {
                //right_up
                if (Ball.VerticalDirection == VerticalDirection.Up)
                {
                    Ball.Left += speed;
                    Ball.Top -= speed;
                }
                //right_down
                else
                {
                    Ball.Left += speed;
                    Ball.Top += speed;
                }
            }
            else
            {
                //left_up
                if (Ball.VerticalDirection == VerticalDirection.Up)
                {
                    Ball.Left -= speed;
                    Ball.Top -= speed;
                    //left_down
                }
                else
                {
                    Ball.Left -= speed;
                    Ball.Top += speed;
                }
            }



            catchBall();




            if (st.ElapsedMilliseconds > 1000)
            {
                //speed++;

                //st.Reset();
                //st.Start();

            }
        }

        private void catchBall()
        {
            //catch with the middle part of the racket
            double top = Ball.Top - CurrentEnemy.Height / 2;
            if (top < 0)
                CurrentEnemy.Top = 0;
            else if (top > this.Height - CurrentEnemy.Height)
                CurrentEnemy.Top = this.Height - CurrentEnemy.Height;
            else
                CurrentEnemy.Top = Ball.Top - CurrentEnemy.Height / 2;

        }

        private void TurnOnGodMode(object par)
        {
            this.Enemy = null;
            this.SuperEnemy = CreateSuperPolygon();
            SuperEnemy.Left = this.Width - SuperEnemy.Width;
            this.CurrentEnemy = SuperEnemy;
            this.GodMode = true;
            MediaPlayer mPlayer = new MediaPlayer();

            mPlayer.Open(new Uri("C:/Users/MSI/MyProjects/TestProjects/PingPongGame/Resources/GodMode.mp3"));
            mPlayer.Play();
            mPlayer.Play();

        }

        private AggressiveRacket CreateSuperPolygon()
        {
            PointCollection pc = new PointCollection();
            pc.Add(new Point(0, 0));
            pc.Add(new Point(100, 0));
            pc.Add(new Point(100, 350));
            pc.Add(new Point(0, 350));
            pc.Add(new Point(0, 300));
            pc.Add(new Point(60, 300));
            pc.Add(new Point(60, 50));
            pc.Add(new Point(0, 50));
            return new AggressiveRacket(pc);

        }

        private ICommand _mouseMoveCommand;

        public ICommand MouseMoveCommand
        {
            get { return _mouseMoveCommand; }
            set
            {
                _mouseMoveCommand = value;
                Notify("MouseMoveCommand");

            }
        }

        private ICommand _windowLoaded;

        public ICommand WindowLoadedCommand
        {
            get { return _windowLoaded; }
            set
            {
                _windowLoaded = value;
                Notify("WindowLoadedCommand");
            }
        }




        private void MouseMove(object par)
        {
            Point mousePosition = (Point)par;
            if (mousePosition.Y < this.Height - this.Player.Height / 9 * 8)
            {

                this.Player.Top = mousePosition.Y;

            }

        }

        private void WindowLoaded(object par)
        {

            StartGame();

        }

        private void StartGame()
        {
            this.Ball = new Ball(20, 20, this.Height / 2, this.Width / 2);
            Enemy.Left = this.Width - Enemy.Width;
            timer = new DispatcherTimer();
            st = new Stopwatch();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
            timer.Tick += Attack;
            timer.Start();
            st.Start();
        }

        private void Attack(object sender, EventArgs e)
        {
            if(this.CurrentEnemy!= null)
            {
                if(this.CurrentEnemy is IAggressive)
                {
                    ((IAggressive)CurrentEnemy).Attack();
                    if(HitPlayer(this.SuperEnemy.Bullet))
                    {
                        MediaPlayer player = new MediaPlayer();
                        player.Open(new Uri("C:/Users/MSI/MyProjects/TestProjects/PingPongGame/Resources/ShotgonHit.mp3"));
                        player.Play();
                    }
                }
                
            }
        }

        private void RestartGame()
        {
            this.Ball = new Ball(20, 20, this.Height / 2, this.Width / 2);
            timer.Stop();
            timer.Start();
            st.Stop();
            st.Start();

        }

        private bool HitPlayer(CanvasShape shape)
        {
            double BallTop = shape.Top;
            double BallBottom = this.Height - shape.Top - shape.Height;
            double PlayerTop = Player.Top;
            double PlayerBottom = this.Height - Player.Top - Player.Height;

            bool a = BallTop + shape.Height > PlayerTop;
            bool b = BallBottom + shape.Height > PlayerBottom;
            bool c = shape.Left < Player.Width && shape.Left > speed * -1;

            return a && b && c;

        }
        private bool HitEnemy()
        {
            double BallTop = Ball.Top;
            double BallBottom = this.Height - Ball.Top - Ball.Height;
            double EnemyTop = CurrentEnemy.Top;
            double EnemyBottom = this.Height - CurrentEnemy.Top - CurrentEnemy.Height;

            bool a = BallTop + Ball.Height > EnemyTop;
            bool b = BallBottom + Ball.Height > EnemyBottom;
            bool c = Ball.Left >= this.Width - CurrentEnemy.Width;

            return a && b && c;


        }

    }
}
