using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TabControl.Converters
{
    public class DataGridItemsSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                BindingProxy proxy = parameter as BindingProxy;

                AppResEditorViewModel model = proxy.Data as AppResEditorViewModel;

                if (model.ResourceType.Equals("Text"))
                    return model.TextList;
                if (model.ResourceType.Equals("Color"))
                    return model.ColorList;

                return DependencyProperty.UnsetValue;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
