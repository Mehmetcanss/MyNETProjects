using AutomaticDataModels.Behaviors;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AutomaticDataModels
{
    public class PropertyFinder
    {

        private Dictionary<string, DependencyProperty> propertyDictionary;
        private Dictionary<DependencyProperty, IValueConverter> converterDictionary;
        private static PropertyFinder instance;
        private PropertyFinder()
        {
            propertyDictionary = new Dictionary<string, DependencyProperty>();
            converterDictionary = new Dictionary<DependencyProperty, IValueConverter>();
            BuildPropertyDictionary();
        }

        public static PropertyFinder CreateInstance()
        {
            if (instance == null)
            {
                instance = new PropertyFinder();
            }
            return instance;
        }

        private void BuildPropertyDictionary()
        {
            propertyDictionary.Add("HeightRequest", FrameworkElement.HeightProperty);
            propertyDictionary.Add("WidthRequest", FrameworkElement.WidthProperty);
            propertyDictionary.Add("HorizontalOptions", FrameworkElement.HorizontalAlignmentProperty);
            propertyDictionary.Add("VerticalOptions", FrameworkElement.VerticalAlignmentProperty);
            propertyDictionary.Add("Margin", FrameworkElement.MarginProperty);
            propertyDictionary.Add("Opacity", FrameworkElement.OpacityProperty);
            propertyDictionary.Add("IsVisible", FrameworkElement.VisibilityProperty);
            propertyDictionary.Add("IsEnabled", FrameworkElement.IsEnabledProperty);
            propertyDictionary.Add("BackgroundColor", UserControl.BackgroundProperty);
            propertyDictionary.Add("Text", ContentControl.ContentProperty);
            propertyDictionary.Add("TextColor", Control.ForegroundProperty);
            propertyDictionary.Add("BorderRadius", ButtonBehavior.CornerRadiusProperty);
            propertyDictionary.Add("BorderWidth", Button.BorderThicknessProperty);
            propertyDictionary.Add("FontSize", Button.FontSizeProperty);


            converterDictionary.Add(UserControl.BackgroundProperty, new Converters.BrushValueConverter());
            converterDictionary.Add(FrameworkElement.HorizontalAlignmentProperty, new Converters.HorizontalOptionsConverter());
            converterDictionary.Add(FrameworkElement.VerticalAlignmentProperty, new Converters.VerticalOptionsConverter());
            converterDictionary.Add(FrameworkElement.VisibilityProperty, new BooleanToVisibilityConverter());
            converterDictionary.Add(Control.ForegroundProperty, new Converters.BrushValueConverter());
            converterDictionary.Add(FrameworkElement.MarginProperty, new Converters.ThicknessValueConverter());
            converterDictionary.Add(Button.BorderThicknessProperty, new Converters.ThicknessValueConverter());

        }

        public bool TryGetProperty(string key, out DependencyProperty value)
        {
            bool found = false;
            found = propertyDictionary.TryGetValue(key, out value);
            return found;
        }

        public bool TryGetConverter(DependencyProperty key, out IValueConverter value)
        {
            bool found = false;
            found = converterDictionary.TryGetValue(key, out value);
            return found;
        }




    }
}
