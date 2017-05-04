using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace ExperimentingWithEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {



        private FrameworkElement _selectedControl;
        private Point startPoint;

        public FrameworkElement SelectedControl
        {
            get { return _selectedControl; }
            set { _selectedControl = value; }
        }

        public MainWindow()
        {




            InitializeComponent();
            mButton.AllowDrop = false;
            BackgroundGrid.PreviewMouseLeftButtonDown += BackgroundGrid_PreviewMouseLeftButtonDown;
            mButton.PreviewMouseLeftButtonDown += MButton_PreviewMouseLeftButtonDown;




        }


        private void HandleMouseMove(object sender, MouseEventArgs e)
        {

            if (e.OriginalSource == this.SelectedControl)
            {

                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (Mouse.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {


                    if (this._selectedControl != null)
                    {
                        //detachFromParent(_selectedControl);
                        System.Windows.DragDrop.DoDragDrop(this, this.SelectedControl, DragDropEffects.Move);
                        e.Handled = true;
                        Console.WriteLine("DoDragDrop Called");
                    }
                }
            }
        }






        private void MButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = Mouse.GetPosition(null);
            Button button = sender as Button;
            button.BorderThickness = new Thickness(5);
            button.BorderBrush = Brushes.Black;
            this.SelectedControl = button;

        }

        private void BackgroundGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.SelectedControl != null)
            {
                Button button = SelectedControl as Button;
                button.BorderThickness = new Thickness(0);
                this.SelectedControl = null;

            }
        }



        private void DragDrop(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            //Button dat = (Button)e.Data.GetData(formats[0]);
            //((Grid)sender).Children.Add(dat);
            e.Handled = true;

        }

        private void detachFromParent(FrameworkElement element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if (parent != null)
            {
                ((Panel)parent).Children.Remove(element);

            }
        }

        private void firstGrid_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void firstGrid_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
