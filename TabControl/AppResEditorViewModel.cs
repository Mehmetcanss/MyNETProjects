using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TabControl.Commands;
using TabControl.Resource;

namespace TabControl
{
    class AppResEditorViewModel : INotifyPropertyChanged
    {


        private ObservableCollection<ColorItem> _colorList;
        
        private ICommand _removeCommand;
        private ICommand _addCommand;
        private string _resourceType;
        private void Notify(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        private List<string> _resourceTypes;
        public AppResEditorViewModel()
        {
            this.ColorList = new ObservableCollection<ColorItem>();
            this.ResourceTypes = new List<string>() { "Color", "Text", "Size" };
            this.TextList = new ObservableCollection<TextItem>();
            InitCommands();
        }

        public List<string> ResourceTypes
        {
            get { return _resourceTypes; }
            set
            {
                _resourceTypes = value;
                Notify("ResourceTypes");
            }
        }

        private ObservableCollection<TextItem> _textList;

        public ObservableCollection<TextItem> TextList
        {
            get { return _textList; }
            set { _textList = value;
                Notify("TextList");
            }
        }



        public string ResourceType
        {
            get { return _resourceType; }
            set
            {
                _resourceType = value;
                Notify("ResourceType");
            }
        }

        public ObservableCollection<ColorItem> ColorList
        {
            get
            {
                return _colorList;
            }
            set
            {
                _colorList = value;
                Notify("ResourceList");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        private void InitCommands()
        {
            this.AddCommand = new CustomCommand(addDelegate);
            this.RemoveCommand = new CustomCommand(removeDelegate);
        }

        private void removeDelegate(object arg)
        {
            this.ColorList.Remove((ColorItem)arg);
        }

        private void addDelegate(object arg)
        {
            this.ColorList.Add(new ColorItem());
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set
            {
                _addCommand = value;
                Notify("AddCommand");
            }
        }

        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
            set
            {
                _removeCommand = value;
                Notify("RemoveCommand");
            }
        }


    }
}
