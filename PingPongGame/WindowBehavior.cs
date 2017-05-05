using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PingPongGame
{
    public class WindowBehavior
    {




        public static ICommand GetWindowLoaded(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(WindowLoadedProperty);
        }

        public static void SetWindowLoaded(DependencyObject obj, ICommand value)
        {
            obj.SetValue(WindowLoadedProperty, value);
        }

        // Using a DependencyProperty as the backing store for WindowLoaded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowLoadedProperty =
            DependencyProperty.RegisterAttached("WindowLoaded", typeof(ICommand), typeof(WindowBehavior), new PropertyMetadata(CommandSet));

        private static void CommandSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = (Window)d;
            window.Loaded += Window_Loaded;
        }

        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ICommand windowLoadedCommand = (ICommand)((DependencyObject)sender).GetValue(WindowBehavior.WindowLoadedProperty);
            windowLoadedCommand.Execute(e);

        }
    }
}
