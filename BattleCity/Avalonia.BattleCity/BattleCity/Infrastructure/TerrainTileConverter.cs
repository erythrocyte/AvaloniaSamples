using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using BattleCity.Models;

namespace BattleCity.Infrastructure;

public class TerrainTileConverter : IValueConverter
{
    
    public static TerrainTileConverter Instance { get; } = new TerrainTileConverter();
    private static Dictionary<TerrainTileTypeEnum, Bitmap> _cache;

    Dictionary<TerrainTileTypeEnum, Bitmap> GetCache()
    {

        return
            _cache ??
            (_cache = Enum.GetValues(typeof(TerrainTileTypeEnum)).OfType<TerrainTileTypeEnum>().ToDictionary(t => t, t =>
                new Bitmap(
                    typeof(TerrainTileConverter).GetTypeInfo()
                        .Assembly.GetManifestResourceStream($"Avalonia.BattleCity.Resources.{t}.png"))));
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => GetCache()[(TerrainTileTypeEnum) value];

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}