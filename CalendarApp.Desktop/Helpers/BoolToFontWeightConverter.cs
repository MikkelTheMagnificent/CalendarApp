﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CalendarApp.Desktop.Helpers
{
    public class BoolToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCurrentMonth)
                return isCurrentMonth ? FontWeights.Bold : FontWeights.Normal;

            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
