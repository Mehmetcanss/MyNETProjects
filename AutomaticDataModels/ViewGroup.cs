using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticDataModels
{
    class ViewGroup : BaseDefinition
    {

        private ObservableCollection<BaseDefinition> _children = new ObservableCollection<BaseDefinition>();

        public ViewGroup() : base()
        {

        }
        public ObservableCollection<BaseDefinition> Children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
