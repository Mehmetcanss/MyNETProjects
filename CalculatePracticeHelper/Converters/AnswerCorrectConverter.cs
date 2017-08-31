using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CalculatePracticeHelper.Converters
{
    public class AnswerCorrectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val == 0)
            {
                Uri absoluteUri = new Uri("pack://application:,,,/Images/false.png");
                BitmapImage bitmapImage = new BitmapImage(absoluteUri);
                return bitmapImage;
            }
            if(val == 1)
            {
                Uri absoluteUri = new Uri("pack://application:,,,/Images/true.png");
                BitmapImage bitmapImage = new BitmapImage(absoluteUri);
                return bitmapImage;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
