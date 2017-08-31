using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TabControl.Converters
{
    public class DataGridColumnVisibilityConverter : IValueConverter
    {

        private static Dictionary<string, List<string>> VisibilityDictionary = new Dictionary<string, List<string>>()
        {
            { "Color", new List<string>() {"Hex", "Value", "Name", "Color"} },
            { "Text", new List<string>() { "Name", "Value" } }
        };
        
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                string val = value as string;
                var columns = VisibilityDictionary[val];
                if(columns.Contains((string) parameter))
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
