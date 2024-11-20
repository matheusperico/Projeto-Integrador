using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ControleDeEstoque.Converters
{
    public class EnumToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return Enum.Parse(targetType, value.ToString());
        }
    }
}
