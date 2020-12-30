using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Moody.Snake.Model;

namespace Moody.Snake.Converter
{
    public class FieldContentToFillConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is FieldContent fieldContent))
                throw new InvalidOperationException();

            switch (fieldContent)
            {
                case FieldContent.Empty:
                    return new SolidColorBrush(Colors.Black);
                case FieldContent.Fruit:
                    return new SolidColorBrush(Colors.Coral);
                case FieldContent.Snake:
                    return new SolidColorBrush(Colors.Green);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}