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

namespace InvestigatingAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime one;
        public MainWindow()
        {
            InitializeComponent();
            MyTask().ContinueWith((task) => Console.Write("Hello"));
            //MyMethodAsync().ContinueWith((x) => Console.WriteLine("Hello"));
   
        }

        private async Task<int> MyTask()
        {
            one = DateTime.Now;
            Task<int> task =  MyMethodAsync();
            Console.WriteLine("Hello called after: " + (DateTime.Now - one).TotalSeconds);
            var result = await task;
            Console.WriteLine("Endresult called after: " + (DateTime.Now - one).TotalSeconds);
            return result;

        }

        public async Task<int> MyMethodAsync()
        {
            
            Task<int> longRunningTask = LongRunningOperationAsync();
            // independent work which doesn't need the result of LongRunningOperationAsync can be done here

            int k = 24;
            int c = k + 55;
            Console.WriteLine(c + "called after: " + (DateTime.Now - one).TotalSeconds);

            //and now we call await on the task if this task is finished, the thread will continue here
            //if not MyMethod async will return to its caller and Console.WriteLine("Hello") will be called
            int result = await longRunningTask;
            //use the result 
            Console.WriteLine(result + "called after: " + (DateTime.Now - one).TotalSeconds);
            return result;
        }

        public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
        {
            await Task.Run(() =>
            {
                for (long i = 0; i < 1000000000; i++) ;
            });

            return 1;
        }

        
    }
}
