using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberOfPaths
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string EnterPosition;
        private int mouseOffset = 6;

        private FrameworkElement SelectedElement;


        private string MouseOnEdge(Point pos, FrameworkElement enteredElement)
        {

            this.DistanceFromBottomBound = (enteredElement.ActualHeight - pos.Y).ToString();
            //first handle the four corners
            if (pos.X < mouseOffset && pos.Y < mouseOffset)
            {
                //enteredFromLeftUpCorner
                this.Cursor = Cursors.SizeNWSE;
                return "northwest";

            }
            else if (Math.Abs(enteredElement.ActualWidth - pos.X) < mouseOffset && pos.Y < mouseOffset)
            {
                //entered from rightUpCorner
                this.Cursor = Cursors.SizeNESW;
                return "northeast";
            }
            else if (pos.X < mouseOffset && Math.Abs(enteredElement.ActualHeight - pos.Y) < mouseOffset)
            {
                //entered from leftDown Corner#
                this.Cursor = Cursors.SizeNESW;
                return "southwest";

            }
            else if (Math.Abs(enteredElement.ActualWidth - pos.X) < mouseOffset &&
                Math.Abs(enteredElement.ActualHeight - pos.Y) < mouseOffset)
            {
                //entered from rightDown Corner
                this.Cursor = Cursors.SizeNWSE;
                return "southeast";
            }
            else if (pos.X < mouseOffset)
            {
                //entered from left
                this.Cursor = Cursors.SizeWE;
                return "west";

            }
            else if (Math.Abs(enteredElement.ActualWidth - pos.X) < mouseOffset)
            {
                //entered from right
                this.Cursor = Cursors.SizeWE;
                return "east";
            }
            else if (Math.Abs(enteredElement.ActualHeight - pos.Y) < mouseOffset)
            {
                //entered from down
                this.Cursor = Cursors.SizeNS;
                return "south";

            }
            else if (pos.Y < mouseOffset)
            {
                //entered from top
                this.Cursor = Cursors.SizeNS;
                return "north";

            }

            this.Cursor = Cursors.Arrow;
            return "none";

        }


        private string _distanceFromBottomBound;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DistanceFromBottomBound
        {
            get { return _distanceFromBottomBound; }
            set
            {
                _distanceFromBottomBound = value;
                Notify("DistanceFromBottomBound");
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
            PathCalculator pathCalculator = new PathCalculator(10000, 10000);

            //int lineOffset = 500 / 20;

            //draw vertical lines
            //for(int i = 1; i <= 19; i++)
            //{
            //    double x = lineOffset * i;
            //    double y1 = 0;
            //    double y2 = this.Height;
            //    Line l = new Line();
            //    l.X1 = x; l.X2 = x; l.Y1 = y1; l.Y2 = y2;
            //    l.StrokeThickness = 1;
            //    l.Stroke = Brushes.Black;
            //    MainCanvas.Children.Add(l);
            //}

            //Task<long> result = pathCalculator.CalculateNumberOfPaths(18, 18);
            //MessageBox.Show(pathCalculator.GetNumberOfPathsToPoint(9999,9999).ToString());
        }



        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {



        }

        private string _gridBottomMargin;

        public string GridBottomMargin
        {
            get { return _gridBottomMargin; }
            set
            {
                _gridBottomMargin = value;
                Notify("GridBottomMargin");
            }
        }


        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }


        public Rect BoundsRelativeTo(FrameworkElement element,
                                   Visual relativeTo)
        {
            return
              element.TransformToVisual(relativeTo)
                     .TransformBounds(LayoutInformation.GetLayoutSlot(element));
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            // Return the general transform for the specified visual object.


            //GeneralTransform generalTransform1 = myStackPanel.TransformToVisual(myTextBlock);

            //// Retrieve the point value relative to the child.
            //Point currentPoint = generalTransform1.Transform(new Point(0, 0));
        }


        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SelectedElement = (FrameworkElement)sender;
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.SelectedElement != null)
            {

                Point p = Mouse.GetPosition(this.SelectedElement);
                string edge = MouseOnEdge(p, this.SelectedElement);


                if (Mouse.LeftButton == MouseButtonState.Pressed && edge != "none")
                {
                    Thickness newMargin = GetMarginBasedOnOffset(this.SelectedElement, edge, p);

                    SelectedElement.Margin = newMargin;

                }

            }

        }

        private double GetOffSetBasedOnEdge(FrameworkElement element, string from, Point p)
        {
            switch (from)
            {
                case "west":
                    return p.X;
                case "east":
                    return element.ActualWidth - p.X;
                case "south":
                    return element.ActualHeight - p.Y;
                case "north":
                    return p.Y;
            }
            throw new Exception(from + " is not a valid value");
        }

        private Thickness GetMarginBasedOnOffset(FrameworkElement element, string from, Point p)
        {
            Thickness m = element.Margin;
            double northOffSet = 0;
            double southOffSet = 0;
            double westOffSet = 0;
            double eastOffSet = 0;


            switch (from)
            {
                case "northwest":
                    northOffSet = GetOffSetBasedOnEdge(element, "north", p);
                    westOffSet = GetOffSetBasedOnEdge(element, "west", p);
                    break;
                case "northeast":
                    northOffSet = GetOffSetBasedOnEdge(element, "north", p);
                    eastOffSet = GetOffSetBasedOnEdge(element, "east", p);
                    break;
                case "southwest":
                    southOffSet = GetOffSetBasedOnEdge(element, "south", p);
                    westOffSet = GetOffSetBasedOnEdge(element, "west", p);
                    break;
                case "southeast":
                    southOffSet = GetOffSetBasedOnEdge(element, "south", p);
                    eastOffSet = GetOffSetBasedOnEdge(element, "east", p);
                    break;
                case "west":
                    westOffSet = GetOffSetBasedOnEdge(element, "west", p);
                    break;
                case "east":
                    eastOffSet = GetOffSetBasedOnEdge(element, "east", p);
                    break;
                case "south":
                    southOffSet = GetOffSetBasedOnEdge(element, "south", p);
                    break;
                case "north":
                    northOffSet = GetOffSetBasedOnEdge(element, "north", p);
                    break;
                default:
                    throw new Exception(from + " is not a valid value");
            }
            return new Thickness(m.Left + westOffSet, m.Top + northOffSet, m.Right + eastOffSet, m.Bottom + southOffSet);
        }
    }
}
