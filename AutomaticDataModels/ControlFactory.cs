using System;
using System.Windows;
using System.Windows.Controls;

namespace AutomaticDataModels
{
    public class ControlFactory
    {
        public static FrameworkElement CreateControl(Type t)
        {
        
            FrameworkElement UIElement = (FrameworkElement)t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            if(UIElement is Button)
            {
                Button b = UIElement as Button;
                ControlTemplate g = (ControlTemplate)Application.Current.Resources["ButtonTemplate"];
                b.Template = (ControlTemplate) Application.Current.Resources["ButtonTemplate"];
            }
            return UIElement;
        }
    }
}
