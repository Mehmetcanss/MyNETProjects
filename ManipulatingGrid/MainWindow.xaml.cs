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

namespace ManipulatingGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        private Point _clickPoint;
        Line l = null;
        private bool bordersActive = false;
        private bool BorderClicked = false;
     

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Border b = sender as Border;
            BorderClicked = true;
            _clickPoint = e.GetPosition((IInputElement)sender);

            l = new Line();

            l.Visibility = System.Windows.Visibility.Visible;
            l.StrokeThickness = 4;
            l.Stroke = System.Windows.Media.Brushes.Black;
            l.X1 = _clickPoint.X;
            l.X2 = _clickPoint.X + 1;
            l.Y1 = _clickPoint.Y;
            l.Y2 = _clickPoint.Y;
            if (!(b.Child == l))
            {
                b.Child = l;

            }
        }


        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (l != null)
            {
                Point pos = e.GetPosition((IInputElement)sender);
                l.X2 = pos.X;
            }
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            try
            {
                if (BorderClicked)
                {
                    BorderClicked = false;
                    Border b = sender as Border;
                    Grid g = VisualTreeHelper.GetParent(b) as Grid;
                    int row = Grid.GetRow(b);
                    if (g.RowDefinitions.Count == 0)
                    {
                        RowDefinition rd = new RowDefinition();
                        rd.Height = new GridLength(1, GridUnitType.Star);
                        g.RowDefinitions.Add(rd);
                    }
                    RowDefinition currentRow = g.RowDefinitions[row];

                    double currentRowHeight = currentRow.Height.Value;
                    double splitRowOneHeight = currentRowHeight * (_clickPoint.Y / b.ActualHeight);
                    double splitRowTwoHeight = currentRowHeight - splitRowOneHeight;

                    RowDefinition d1 = new RowDefinition();
                    d1.Height = new GridLength(splitRowOneHeight, GridUnitType.Star);
                    RowDefinition d2 = new RowDefinition();
                    d2.Height = new GridLength(splitRowTwoHeight, GridUnitType.Star);
                    RowDefinition[] allRows = new RowDefinition[g.RowDefinitions.Count + 1];
                    allRows[row] = d1;
                    allRows[row + 1] = d2;
                    for (int i = 0; i < g.RowDefinitions.Count;)
                    {
                        if (i < row)
                        {
                            allRows[i] = g.RowDefinitions[i];
                            i++;

                        }
                        else
                        {
                            if (i + 2 < allRows.Count())
                            {
                                allRows[i + 2] = g.RowDefinitions[i+1];

                            }
                            i++;
                        }
                    }
                    g.RowDefinitions.Clear();
                    for (int i = 0; i < allRows.Length; i++)
                    {
                        g.RowDefinitions.Add(allRows[i]);
                    }
                    DeactivateAllSupportBorders(mainGrid);
                    Cursor = Cursors.Arrow;
                    bordersActive = false;
                }

            }
            catch (Exception)
            {


            }


        }

        public void ActivateAllSupportBorders(Panel p)
        {
            Grid gd = p as Grid;
            if (gd != null)
            {
                InsertAllSupportBorders(gd);
            }
            foreach (var child in p.Children)
            {
                Panel childVG = child as Panel;
                if (childVG != null) ActivateAllSupportBorders(childVG);
            }
        }

        private void InsertAllSupportBorders(Grid g)
        {
            if (g.RowDefinitions.Count == 0)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                g.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < g.RowDefinitions.Count; i++)
            {

                InsertSupportBorder(g, i);

            }
        }
        private void InsertSupportBorder(Grid grid, int row)
        {

            Border b = new Border();
            b.BorderThickness = new Thickness(0);
            b.Background = Brushes.Blue;
            b.Tag = "SupportBorder";
            b.HorizontalAlignment = HorizontalAlignment.Stretch;
            b.VerticalAlignment = VerticalAlignment.Stretch;
            b.SetValue(Grid.RowProperty, row);
            b.PreviewMouseLeftButtonDown += Border_PreviewMouseLeftButtonDown;
            b.PreviewMouseLeftButtonUp += Border_PreviewMouseLeftButtonUp;
            b.PreviewMouseMove += Border_PreviewMouseMove;
            b.Tag = "SupportBorder";
            grid.Children.Add(b);


        }


        public void DeactivateAllSupportBorders(Panel vg)
        {
            Grid gd = vg as Grid;
            if (gd != null)
            {
                RemoveSupportBorders(gd);
            }
            foreach (var child in vg.Children)
            {
                Panel childVG = child as Panel;
                if (childVG != null) DeactivateAllSupportBorders(childVG);
            }
        }


        public void RemoveSupportBorders(Grid grid)
        {

            List<Border> supportBorders = GetSupportBorders(grid);
            foreach (var border in supportBorders)
                grid.Children.Remove(border);
        }

        public List<Border> GetSupportBorders(Grid grid)
        {
            List<Border> supportBorders = new List<Border>();


            foreach (var child in grid.Children)
            {
                Border border = child as Border;
                if (border != null)
                {

                    if (border.Tag.Equals("SupportBorder")) supportBorders.Add(border);

                }
            }
            return supportBorders;
        }

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!bordersActive)
            {
                ActivateAllSupportBorders(mainGrid);
                bordersActive = true;
                Cursor = Cursors.Pen;
            }
        }

        private void mainGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
