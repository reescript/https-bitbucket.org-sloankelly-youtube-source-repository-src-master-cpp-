using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SloanKelly.Tools.SGE.Converters.Thickness
{
    internal class IntegerToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double length = double.Parse(value.ToString());
            return new GridLength(length);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
