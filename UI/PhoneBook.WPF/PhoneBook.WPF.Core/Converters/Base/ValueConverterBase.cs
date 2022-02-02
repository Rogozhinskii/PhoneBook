using System;
using System.Globalization;
using System.Windows.Data;

namespace PhoneBook.WPF.Core.Converters.Base
{
    public abstract class ValueConverterBase : IValueConverter
    {
        /// <summary>
        /// Реализует прямое преобразование входных парамметров
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);


        /// <summary>
        /// Реализует обратное преобразование входных  параметров
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Обратное преобразование не предусмотрено");
        }
    }
}
