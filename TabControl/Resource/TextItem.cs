using System;
using System.ComponentModel;

namespace TabControl.Resource
{
    public class TextItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TextItem()
        {

        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
                Notify("Name");
            }
        }

        private void Notify(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value;
                Notify("Value");

            }
        }

    }
}
