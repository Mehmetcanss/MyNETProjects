using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PingPongGame
{
    class AggressiveRacket : ShapeWithPoints, AggressiveObject
    {
        public AggressiveRacket(PointCollection points) : base(points)
        {
        }

        public void Attack()
        {
            
        }
    }
}
