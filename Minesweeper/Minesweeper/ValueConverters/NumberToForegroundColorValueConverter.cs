using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Media;

namespace Minesweeper
{
    public class NumberToForegroundColorValueConverter : BaseValueConverter<NumberToForegroundColorValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "1":
                    return new SolidColorBrush(Colors.Blue);

                case "2":
                    return new SolidColorBrush(Colors.Green);

                case "3":
                    return new SolidColorBrush(Colors.Red);

                case "4":
                    return new SolidColorBrush(Colors.Purple);

                case "5":
                    return new SolidColorBrush(Colors.Maroon);

                case "6":
                    return new SolidColorBrush(Colors.Turquoise);

                case "7":
                    return new SolidColorBrush(Colors.Black);

                case "8":
                    return new SolidColorBrush(Colors.Gray);

                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}