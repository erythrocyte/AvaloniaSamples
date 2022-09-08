using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace BattleCity.Models;

public class MovingGameObject : GameObject
{
    private readonly GameField _field;
    private CellLocation _cellLocation;
    private FacingEnum _facing;
    private CellLocation _targetCellLocation;

    protected MovingGameObject(GameField field, CellLocation location, FacingEnum facing) : base(location.ToPoint())
    {
        _field = field;
        Facing = facing;
        CellLocation = location;
        TargetCellLocation = location;
    }

    public override int Layer => 1;

    public FacingEnum Facing
    {
        get => _facing;
        set => this.RaiseAndSetIfChanged(ref _facing, value);
    }

    public CellLocation CellLocation
    {
        get => _cellLocation;
        private set => this.RaiseAndSetIfChanged(ref _cellLocation, value);
    }

    public CellLocation TargetCellLocation
    {
        get => _targetCellLocation;
        private set => this.RaiseAndSetIfChanged(ref _targetCellLocation, value);
    }

    public bool IsMoving => TargetCellLocation != CellLocation;

    protected virtual double SpeedFactor => (double)1 / 15;

    public bool SetTargetCell(CellLocation loc)
    {
        if (IsMoving)
            //We are the bear rolling from the hill
            throw new InvalidOperationException("Unable to change direction while moving");
        if (loc == CellLocation)
            return true;
        Facing = GetDirection(CellLocation, loc);
        if (loc.X < 0 || loc.Y < 0)
            return false;
        if (loc.X >= _field.Width || loc.Y >= _field.Height)
            return false;
        if (!_field.Tiles[loc.X, loc.Y].IsPassable)
            return false;

        if (
            _field.GameObjects.OfType<MovingGameObject>()
            .Any(t => t != this && (t.CellLocation == loc || t.TargetCellLocation == loc)))
            return false;

        TargetCellLocation = loc;
        return true;
    }

    public CellLocation GetTileAtDirection(FacingEnum facing)
    {
        if (facing == FacingEnum.North)
            return CellLocation.WithY(CellLocation.Y - 1);
        if (facing == FacingEnum.South)
            return CellLocation.WithY(CellLocation.Y + 1);
        if (facing == FacingEnum.West)

            return CellLocation.WithX(CellLocation.X - 1);
        return CellLocation.WithX(CellLocation.X + 1);
    }

    public bool SetTargetFacing(FacingEnum? facing)
    {
        return SetTargetCell(facing.HasValue ? GetTileAtDirection(facing.Value) : CellLocation);
    }

    private FacingEnum GetDirection(CellLocation current, CellLocation target)
    {
        if (target.X < current.X)
            return FacingEnum.West;
        if (target.X > current.X)
            return FacingEnum.East;
        if (target.Y < current.Y)
            return FacingEnum.North;
        return FacingEnum.South;
    }

    public void SetLocation(CellLocation loc)
    {
        CellLocation = loc;
        Location = loc.ToPoint();
    }

    public void MoveToTarget()
    {
        if (TargetCellLocation == CellLocation)
            return;
        var speed = GameField.CellSize *
                    (_field.Tiles[CellLocation.X, CellLocation.Y].Speed +
                     _field.Tiles[TargetCellLocation.X, TargetCellLocation.Y].Speed) / 2
                    * SpeedFactor;
        var pos = Location;
        var direction = GetDirection(CellLocation, TargetCellLocation);
        if (direction == FacingEnum.North)
        {
            pos = pos.WithY(pos.Y - speed);
            Location = pos;
            if (pos.Y / GameField.CellSize <= TargetCellLocation.Y)
                SetLocation(TargetCellLocation);
        }
        else if (direction == FacingEnum.South)
        {
            pos = pos.WithY(pos.Y + speed);
            Location = pos;
            if (pos.Y / GameField.CellSize >= TargetCellLocation.Y)
                SetLocation(TargetCellLocation);
        }
        else if (direction == FacingEnum.West)
        {
            pos = pos.WithX(pos.X - speed);
            Location = pos;
            if (pos.X / GameField.CellSize <= TargetCellLocation.X)
                SetLocation(TargetCellLocation);
        }
        else if (direction == FacingEnum.East)
        {
            pos = pos.WithX(pos.X + speed);
            Location = pos;
            if (pos.X / GameField.CellSize >= TargetCellLocation.X)
                SetLocation(TargetCellLocation);
        }
    }
}