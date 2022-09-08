namespace BattleCity.Models;

public enum TerrainTileTypeEnum
{
    Plain, //passable, shoot-thru
    WoodWall, //impassable, takes 1 shot to bring down
    StoneWall, //impassable, indestructible
    Water, //impassable, shoot-thru
    Pavement, //passable, 2x speed
    Forest //passable at half speed, shoot-thru
}