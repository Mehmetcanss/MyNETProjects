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

        private double speed = 5;
        private Racket _player;
        private double _width;
        private double _height;
        private Racket _enemy;
        private Stopwatch st;
        private double _godScale = 5;

        public double GodScale
        {
            get { return _godScale; }
            set
            {
                _godScale = value;
                Notify("GodScale");
            }
        }

        private Ball _ball;
        private DispatcherTimer timer;
        private MediaPlayer _mediaPlayer = new MediaPlayer();
        Random random = new Random();
        int randomBottomBound = 10;
        int randomUpperBound = 40;

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
            this.Player = new Racket(25, 500);
            this.Enemy = new Racket(25, 80);

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

            if (HitPlayer())
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


            if (Ball.Top < this.Height - Enemy.Height)
                catchBall();
            else Enemy.Top = this.Height - Enemy.Height;


            if (st.ElapsedMilliseconds > 1000)
            {
                speed++;
                st.Reset();
                st.Start();
            }

        }

        private void catchBall()
        {
            //catch with the middle part of the racket
            Enemy.Top = Ball.Top - Enemy.Height / 2;
            
        }

        private void TurnOnGodMode(object par)
        {
            this.Enemy.Height *= 5;
            this.GodMode = true;
            MediaPlayer mPlayer = new MediaPlayer();
            randomBottomBound = 0;
            randomUpperBound = 0;
            mPlayer.Open(new Uri("C:/Users/MSI/MyProjects/TestProjects/PingPongGame/Resources/GodMode.mp3"));
            mPlayer.Play();

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
            timer = new DispatcherTimer();
            st = new Stopwatch();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            st.Start();
        }

        private void RestartGame()
        {
            this.Ball = new Ball(20, 20, this.Height / 2, this.Width / 2);
            timer.Stop();
            timer.Start();
            st.Stop();
            st.Start();

        }

        private bool HitPlayer()
        {
            double BallTop = Ball.Top;
            double BallBottom = this.Height - Ball.Top - Ball.Height;
            double PlayerTop = Player.Top;
            double PlayerBottom = this.Height - Player.Top - Player.Height;

            bool a = BallTop + Ball.Height > PlayerTop;
            bool b = BallBottom + Ball.Height > PlayerBottom;
            bool c = Ball.Left < Player.Width && Ball.Left > speed * -1;

            return a && b && c;

        }
        private bool HitEnemy()
        {
            double BallTop = Ball.Top;
            double BallBottom = this.Height - Ball.Top - Ball.Height;
            double EnemyTop = Enemy.Top;
            double EnemyBottom = this.Height - Enemy.Top - Enemy.Height;

            bool a = BallTop + Ball.Height > EnemyTop;
            bool b = BallBottom + Ball.Height > EnemyBottom;
            bool c = Ball.Left >= this.Width - Enemy.Width;

            return a && b && c;


        }

    }
}
