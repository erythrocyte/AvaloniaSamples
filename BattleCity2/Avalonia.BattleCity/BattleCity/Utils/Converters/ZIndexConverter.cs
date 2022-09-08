using System;
using System.Globalization;
using Avalonia.Data.Converters;
using BattleCity.Models;

namespace BattleCity.Utils.Converters;

public class ZIndexConverter : IValueConverter
{
    public static ZIndexConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Player
            ? 2
            : value is Tank
                ? 1
                : 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}