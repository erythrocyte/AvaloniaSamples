using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using BattleCity.Models;

namespace BattleCity.Utils.Converters;

public class TerrainTileConverter : IValueConverter
{
    private static Dictionary<TerrainTileTypeEnum, Bitmap> _cache;

    public static TerrainTileConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var cache = GetCache();
        var tt_value = (TerrainTileTypeEnum)value;
        return cache[tt_value];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private Dictionary<TerrainTileTypeEnum, Bitmap> GetCache()
    {
        if (_cache is null)
        {
            _cache = Enum.GetValues(typeof(TerrainTileTypeEnum)).OfType<TerrainTileTypeEnum>().ToDictionary(t => t, t =>
                new Bitmap(
                    typeof(TerrainTileConverter).GetTypeInfo()
                        .Assembly.GetManifestResourceStream($"BattleCity.Assets.{t}.png")));
        }

        return _cache;
    }
}