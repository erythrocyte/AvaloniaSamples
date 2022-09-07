using System.Collections.Generic;
using Avalonia;

namespace BattleCity.Models;

public class TerrainTile : GameObject
{
    private static readonly Dictionary<TerrainTileTypeEnum, double> Speeds = new Dictionary<TerrainTileTypeEnum, double>
    {
        {TerrainTileTypeEnum.Plain, 1},
        {TerrainTileTypeEnum.WoodWall, 0},
        {TerrainTileTypeEnum.StoneWall, 0},
        {TerrainTileTypeEnum.Water, 0},
        {TerrainTileTypeEnum.Pavement, 2},
        {TerrainTileTypeEnum.Forest, 0.5}
    };
    
    private static readonly Dictionary<TerrainTileTypeEnum, bool> ShootThrus = new Dictionary<TerrainTileTypeEnum, bool>
    {
        {TerrainTileTypeEnum.Plain, true},
        {TerrainTileTypeEnum.WoodWall, false},
        {TerrainTileTypeEnum.StoneWall, false},
        {TerrainTileTypeEnum.Water, true},
        {TerrainTileTypeEnum.Pavement, true},
        {TerrainTileTypeEnum.Forest, true},
    };


    public double Speed => Speeds[Type];
    public bool ShootThru => ShootThrus[Type];
    public bool IsPassable => Speed > 0.1;
    public TerrainTileTypeEnum Type { get; set; }

    public TerrainTile(Point location, TerrainTileTypeEnum type) : base(location)
    {
        Type = type;
    }
}