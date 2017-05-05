using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace PingPongGame
{
    public class Ball : INotifyPropertyChanged
    {
        private double _top;
        private double _left;
        private double _height;

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                Notify("Height");
            }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                Notify("Width");
            }
        }



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


        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                Notify("Top");
            }
        }

        public Ball(double height, double width, double top, double left)
        {
       
            this.Height = height;
            this.Width = width;
            this.Top = top;
            this.Left = left;

        }



        private void Notify(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                Notify("Left");
            }
        }

      


    }
}
