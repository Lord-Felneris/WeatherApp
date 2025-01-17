﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverter
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRain = (bool)value;
            if (isRain)
                return "Current Raining";
            else
                return "Not Current Raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = (string)value;
            if (isRaining == "Current Raining")
                return true;
            else
                return false;
        }
    }
}
