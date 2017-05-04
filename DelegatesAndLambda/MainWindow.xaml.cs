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

namespace DelegatesAndLambda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// a delegate is a function model, it is used as a type
        /// Calculate Delegate is a Function type
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>double</returns>
        private delegate double CalculateDelegate(int a, int b);

        


        /// <summary>
        /// Action is a ready made delegate, that has no return type, .i.e returns void
        /// SomeFunction is a Function Variable
        /// </summary>
        private Action<string, string, string> SomeFunction;

        /// <summary>
        /// Func is a read made delegate that always has a return type
        /// The AnotherFunction is a Function variable that can be assigned, 
        /// it takes two parameters of type double and returns double
        /// </summary>

        private Func<double, double, string> AnotherFunction;


        /// <summary>
        /// Using Delegates with event handler
        /// </summary>

        private event CalculateDelegate someEvent;

        private event Func<double, double, string> anotherEvent;


        public MainWindow()
        {
            InitializeComponent();

            ///
            CalculateDelegate someCalculator = (a, b) => (a * b) * 10;

            CalculateDelegate anotherCalc = delegate (int a, int b) { return a + b; };


            SomeFunction = (a, b, c) => MessageBox.Show(a + b + c);
            
            AnotherFunction = (a, b) => ((a * b) * 10).ToString();

            

            //other ways to declare 

            SomeFunction = new Action<string, string, string>((a, b, c) => MessageBox.Show(a + b + c));
            SomeFunction = new Action<string, string, string>(delegate (string a, string b, string c) { MessageBox.Show("hello"); });

        }
    }
}
