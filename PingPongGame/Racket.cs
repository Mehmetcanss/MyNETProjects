using System;
using System.ComponentModel;
using System.Windows;

namespace PingPongGame
{
    public class Racket : INotifyPropertyChanged
    {
        private double _width;
        private double _height;
        private double _top = 0;


        public Racket(double width, double height)
        {
            this.Width = width;
            this.Height = height;
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



        public event PropertyChangedEventHandler PropertyChanged;

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                Notify("Width");
            }
        }

        private void Notify(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
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



    }
}
