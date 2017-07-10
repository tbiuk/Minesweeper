using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Media;

namespace Minesweeper
{
    public class ElementStateToBackgroundColorValueConverter : BaseValueConverter<ElementStateToBackgroundColorValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ElementState)value)
            {
                case ElementState.Unknown:
                    return new SolidColorBrush(Color.FromRgb(230, 230, 230));

                case ElementState.Clear:
                    return new SolidColorBrush(Colors.LightGray);

                case ElementState.Mine:
                    return new SolidColorBrush(Colors.LightGray);

                case ElementState.TrueMine:
                    return new SolidColorBrush(Colors.LightGray);

                case ElementState.DetonatedMine:
                    return new SolidColorBrush(Colors.Red);

                case ElementState.NotFoundMine:
                    return new SolidColorBrush(Colors.Yellow);

                case ElementState.NotFoundClear:
                    return new SolidColorBrush(Colors.LightYellow);

                case ElementState.FalsePositive:
                    return new SolidColorBrush(Colors.Yellow);                    

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}