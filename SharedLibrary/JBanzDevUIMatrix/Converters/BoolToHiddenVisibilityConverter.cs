using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JBanzDevUIMatrix.Converters
{
    public class BoolToHiddenVisibilityConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = ToBoolean(value);
            var p = ToNullableBoolean(parameter);

            var result = Visibility.Hidden;

            if (p.HasValue)
            {
                if (p.Value == v)
                {
                    result = Visibility.Visible;
                }
            }
            else
            {
                if (v)
                {
                    result = Visibility.Visible;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible;
        }
    }
}
