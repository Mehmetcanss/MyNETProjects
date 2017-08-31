using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Efe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Faktorial(8);
        }

        public void MesajGoster(string d)
        {
            MessageBox.Show(d);
        }
        public void Faktorial(int a)
        {
            long result = a;
            for(int  i = a -1; i > 0; i--)
            {
                result *= i;
            }
            MesajGoster(result.ToString());
        }
    }
}
