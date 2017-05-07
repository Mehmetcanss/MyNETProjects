using System.Linq;
using System.Windows.Media;

namespace PingPongGame
{
    public class ShapeWithPoints : CanvasShape
    {
        

        public ShapeWithPoints(PointCollection points)
        {
            double LowestY = points.Min(x => x.Y);
            double HighestY = points.Max(x => x.Y);
            this.Height = HighestY - LowestY;
            double LowestX = points.Min(x => x.X);
            double HighestX = points.Max(x => x.X);
            this.Width = HighestX - LowestX;
            this.Points = points;
        }

        private PointCollection  _points;

        public PointCollection Points {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                Notify("Points");
            }

        }



        Geometry _renderedGeo;

        public Geometry RenderedGeo
        {
            get
            {
                return _renderedGeo;

            }
            set
            {
                _renderedGeo = value;
            }
        }
    }
}
