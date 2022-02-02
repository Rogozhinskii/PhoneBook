using PhoneBook.WPF.Core.Converters.Base;
using System;
using System.Globalization;
using System.Windows;

namespace PhoneBook.WPF.Core.Converters
{
    public class StringToVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return Visibility.Visible;
            if (string.IsNullOrEmpty(value.ToString()))
                return Visibility.Visible;
            return Visibility.Hidden;
        }
    }
}
