using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PingPongGame
{
    public class AggressiveRacket : ShapeWithPoints, IAggressive
    {
        public AggressiveRacket(PointCollection points) : base(points)
        {

        }

        private double _attackSpeed = 10;

        private bool _isAttacking;

        public bool IsAttacking
        {
            get { return _isAttacking; }
            set { _isAttacking = value;
                Notify("IsAttacking");
            }
        }


        public void Attack()
        {
            if (!IsAttacking)
            {
                CreateTriangleBullet(10);
                IsAttacking = true;

            }
            if (this.Bullet.Left > 0)
            {
                this.Bullet.Left-=_attackSpeed;
            }
            else
            {
                IsAttacking = false;
            }

        }

        private ShapeWithPoints _bullet;

        public ShapeWithPoints Bullet
        {
            get { return _bullet; }
            set
            {
                _bullet = value;
                Notify("Bullet");
            }
        }

        private void CreateTriangleBullet(double lineLength)
        {
            Point p = new Point(0, lineLength / 2);
            double sqrt3 = Math.Sqrt(3);
            double xOffSet = (lineLength / 2) * sqrt3;
            double yOffset = lineLength / 2;

            Point baseDown = new Point(p.X + xOffSet, p.Y + yOffset);
            Point baseUp = new Point(p.X + xOffSet, p.Y - yOffset);
            PointCollection pc = new PointCollection
            {
                p, baseDown, baseUp
            };
            this.Bullet = new ShapeWithPoints(pc);
            this.Bullet.Left = this.Left - this.Width - 10;
            this.Bullet.Top = this.Top + this.Height / 2;
            MediaPlayer mPlayer = new MediaPlayer();
            mPlayer.Open(new Uri("C:/Users/MSI/MyProjects/TestProjects/PingPongGame/Resources/Pew.mp3"));
            mPlayer.Play();
        }

    }
}
