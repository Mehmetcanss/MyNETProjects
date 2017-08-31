using System.Windows;
using System.Windows.Input;

namespace TabControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new AppResEditorViewModel();
        }

    

        private void Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new Window().Show();

        }
    }
}
