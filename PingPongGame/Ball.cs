using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace PingPongGame
{
    public class Ball : CanvasShape
    {


        private HorizontalDirection _horizontalDirection = HorizontalDirection.Left;
        private VerticalDirection _verticalDirection = VerticalDirection.Down;

        public HorizontalDirection HorizontalDirection
        {
            get { return _horizontalDirection; }
            set { _horizontalDirection = value; }
        }


        public VerticalDirection VerticalDirection
        {
            get { return _verticalDirection; }
            set { _verticalDirection = value; }
        }


        public Ball(double height, double width, double top, double left)
        {
       
            this.Height = height;
            this.Width = width;
            this.Top = top;
            this.Left = left;

        }

    }
}
