using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PingPongGame
{
    class MouseBehavior
    {


        public static ICommand GetMouseMoveCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseMoveCommandProperty);
        }

        public static void SetMouseMoveCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseMoveCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseMoveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseMoveCommandProperty =
            DependencyProperty.RegisterAttached("MouseMoveCommand", typeof(ICommand), typeof(MouseBehavior), new PropertyMetadata(MouseMoveCommandSet));

        private static void MouseMoveCommandSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement el = d as FrameworkElement;
            el.PreviewMouseMove += El_MouseMove;
        }


        private static void El_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement el = sender as FrameworkElement;
            ICommand mouseMoveCommand = (ICommand)el.GetValue(MouseBehavior.MouseMoveCommandProperty);
            Point position = e.GetPosition((IInputElement)sender);
            mouseMoveCommand.Execute(position);
        }
    }
}
