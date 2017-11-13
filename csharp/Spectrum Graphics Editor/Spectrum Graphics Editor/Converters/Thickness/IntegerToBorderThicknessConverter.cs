using System;
using System.Globalization;
using System.Windows.Data;

namespace SloanKelly.Tools.SGE.Converters.Thickness
{
    internal sealed class IntegerToBorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double thickness = double.Parse(value.ToString());
            return new System.Windows.Thickness(thickness);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
