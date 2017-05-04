using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AutomaticDataModels
{
    class Converters
    {
        public class BrushValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                BrushConverter converter = new BrushConverter();
                System.Windows.Media.Brush converted = (System.Windows.Media.Brush) converter.ConvertFromString((string)value);

                return converted;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }
        }
        public class VerticalOptionsConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string sVal = (string) value;
                switch(sVal)
                {
                    case "Start":
                        return VerticalAlignment.Top;
                    case "Center":
                        return VerticalAlignment.Center;
                    case "FillAndExpand":
                        return VerticalAlignment.Stretch;
                    case "End":
                        return VerticalAlignment.Bottom;

                }
                return VerticalAlignment.Top;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }
        }
        public class HorizontalOptionsConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string sVal = (string)value;
                switch (sVal)
                {
                    case "Start":
                        return HorizontalAlignment.Left;
                    case "Center":
                        return HorizontalAlignment.Center;
                    case "FillAndExpand":
                        return HorizontalAlignment.Stretch;
                    case "End":
                        return HorizontalAlignment.Right;

                }
                return HorizontalAlignment.Left;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }
        }
        public class ThicknessValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {

                Thickness converted = (Thickness)new ThicknessConverter().ConvertFrom(value);
                return converted;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    }
}
