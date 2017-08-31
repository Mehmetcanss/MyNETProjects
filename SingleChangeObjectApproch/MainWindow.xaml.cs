using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using UndoRedo_SingleObjectRepresntingchange;



namespace UndoRedo_SingleObjectRepresntingchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        
        Container objectContainer = null;
     
        public MainWindow()
        {
            InitializeComponent();          
 
            bdrMainArea.Child = null;
            if (objectContainer == null)
            {
                objectContainer = new Container();

            }
            bdrMainArea.Child = objectContainer;
        }     

    }
}
