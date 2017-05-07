using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongGame
{
    public abstract class CanvasShape : INotifyPropertyChanged
    {
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

        private double _left = 0;

        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                Notify("Left");
            }
        }



        private double _top = 0;

        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                Notify("Top");
            }
        }


        protected void Notify(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private double _height;

        public event PropertyChangedEventHandler PropertyChanged;

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
