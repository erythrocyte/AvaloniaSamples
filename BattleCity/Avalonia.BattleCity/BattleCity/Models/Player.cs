namespace BattleCity.Models;

public class Player : MovingGameObject
{
    public Player(GameField field, CellLocation location, FacingEnum facing) : base(field, location, facing)
    {
    }
}