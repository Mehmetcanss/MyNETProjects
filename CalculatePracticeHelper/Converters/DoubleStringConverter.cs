using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CalculatePracticeHelper.Converters
{
    public class DoubleStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val;
            bool isDouble = Double.TryParse(value.ToString(), out val);
            if (isDouble)
            {
                if (val > 0) return value.ToString();
                return "";
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double converted;
            bool isDouble = Double.TryParse((string) value, out converted);
            if (isDouble)
            {
                return converted;
            }
            return -1;
        }
    }
}
