using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfCommands
{
    public class InteractionController
    {
        public  readonly RoutedUICommand Exit = new RoutedUICommand
                        (
                                "Exit",
                                "Exit",
                                typeof(InteractionController),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                                }
                        );

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

  


    }
}
