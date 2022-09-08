namespace BattleCity.Models;

public class Tank : MovingGameObject
{
    private readonly double _speed;

    public Tank(GameField field, CellLocation location, FacingEnum facing, double speed) : base(field, location, facing)
    {
        _speed = speed;
    }

    protected override double SpeedFactor => _speed * base.SpeedFactor;
}