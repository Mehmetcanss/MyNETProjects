using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace InvestigatingAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        DateTime one;

        private string _number;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                Notify("Number");
            }
        }

        private void Notify(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Number = "1";


        }


        public  Task<long> LongRunningOperationAsync() // assume we return an int from this long running operation 
        {
            long i = 0;
            return Task.Run(() =>
            {
                for (i = 0; i < 1000000000; i++) ;
                return i;
            });

        ;
        }

        //async keyword allows you to use the "await" keyword, the code after await keyword is always run on the calling context,
        //in this case the UI thread
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            //start a new task
            var task =  LongRunningOperationAsync();
            
            //this code will immediately run without waiting
            this.Number = "55";

            //use await when you need the result of the task
            var result = await task;
            MessageBox.Show(result.ToString());
        }

        private void counter_Click(object sender, RoutedEventArgs e)
        {
            double no = Double.Parse(Number);
            no++;
            this.Number = no.ToString();
        }
    }
}
