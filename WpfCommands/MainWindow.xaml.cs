using System.Windows;
using System.Windows.Input;

namespace WpfCommands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private InteractionController InterController;



        public MainWindow()
        {
            InitializeComponent();
            this.InterController = new InteractionController();
            this.DataContext = InterController;

        }

        private void CutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (txtEditor != null) && (txtEditor.SelectionLength > 0);
        }

        private void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Cut();
        }

        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Paste();
        }


    }
}
