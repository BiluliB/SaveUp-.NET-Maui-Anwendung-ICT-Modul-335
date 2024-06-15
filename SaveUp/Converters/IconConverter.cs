using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SaveUp.Converters
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isActive = (bool)value;
            string iconName = parameter.ToString();
            return isActive ? $"Resources/Images/{iconName}_filled.png" : $"Resources/Images/{iconName}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
