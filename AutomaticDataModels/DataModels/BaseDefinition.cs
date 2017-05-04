using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AutomaticDataModels
{
    public abstract class BaseDefinition : INotifyPropertyChanged
    {

        private double _widthRequest = 50;
        private double _heightRequest = 50;
        private string _backgroundColor = "Yellow";
        private string _margin = "0,0,0,0";
        private string _horizontalOptions = "Start";
        private string _verticalOptions = "Start";
        private double _opacity = 1;
        private bool _isEnabled = true;
        private bool _isVisible = true;
        private Object _control;


    


        public event PropertyChangedEventHandler PropertyChanged;

        public BaseDefinition()
        {
            this.PropertyChanged += BaseDefinition_PropertyChanged;

        }

        private void BaseDefinition_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public double HeightRequest
        {
            get { return _heightRequest; }
            set
            {
                _heightRequest = value;
                Notify("HeightRequest");
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                Notify("IsVisible");
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                Notify("IsEnabled");
            }
        }


        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                Notify("Opacity");
            }
        }


        public string HorizontalOptions
        {
            get { return _horizontalOptions; }
            set
            {
                _horizontalOptions = value;
                Notify("HorizontalOptions");
            }
        }


        public string VerticalOptions
        {
            get { return _verticalOptions; }
            set
            {
                _verticalOptions = value;
                Notify("VerticalOptions");
            }
        }


        public string Margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
                Notify("Margin");
            }
        }


        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                Notify("BackgroundColor");
            }
        }

        protected void Notify(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public double WidthRequest
        {
            get { return _widthRequest; }
            set
            {
                _widthRequest = value;
                Notify("WidthRequest");

            }
        }

        public Object Control
        {
            get { return _control; }
            set { _control = value; }
        }




    }
}
