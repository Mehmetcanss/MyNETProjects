using System;
using System.ComponentModel;
using System.Windows.Media;

namespace TabControl.Resource
{
    public class ColorItem : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; Notify("Name"); }
        }

 

        public string Hex
        {
            get {
                return this.Color.ToString();
            }
         
        }



        private void Notify(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set {
                _value = value;
                Notify("Value");
            }
        }
        private Color _color;

        public event PropertyChangedEventHandler PropertyChanged;

        public Color Color
        {
            get { return _color; }
            set {
                _color = value; Notify("Color"); Notify("Hex");
            }
        }

        public ColorItem()
        {
            this.Name = "resource1";
            this.Color = Colors.Blue;
        }

    }
}
