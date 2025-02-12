using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CalendarApp.Desktop.Helpers
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCurrentMonth)
                return isCurrentMonth ? Brushes.Black : Brushes.Gray;

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
