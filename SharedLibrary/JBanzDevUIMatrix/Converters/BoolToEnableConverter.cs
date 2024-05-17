using System;
using System.Globalization;
using System.Windows.Data;

namespace JBanzDevUIMatrix.Converters
{
    public class BoolToEnableConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return (bool)value;
            }

            var parameterBool = bool.Parse(parameter.ToString());

            if ((bool)value == parameterBool)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
