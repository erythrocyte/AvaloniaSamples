using System;
using System.Linq;
using Avalonia.Input;

namespace BattleCity.Models;

public class Game: GameBase
{
    private readonly GameField _field;

    public Game(GameField field)
    {
        _field = field;
    }

    private Random Random { get; } = new Random();

    protected override void Tick()
    {
        if (!_field.Player.IsMoving)
        {
            if (Keyboard.IsKeyDown(Key.Up))
                _field.Player.SetTargetFacing(FacingEnum.North);
            else if (Keyboard.IsKeyDown(Key.Down))
                _field.Player.SetTargetFacing(FacingEnum.South);
            else if (Keyboard.IsKeyDown(Key.Left))
                _field.Player.SetTargetFacing(FacingEnum.West);
            else if (Keyboard.IsKeyDown(Key.Right))
                _field.Player.SetTargetFacing(FacingEnum.East);
        }
        foreach (var tank in _field.GameObjects.OfType<Tank>())
            if (!tank.IsMoving)
            {
                if (!tank.SetTargetFacing(tank.Facing))
                {
                    if (!tank.SetTargetFacing((FacingEnum) Random.Next(4)))
                        tank.SetTargetFacing(null);
                }
            }

        foreach(var obj in _field.GameObjects.OfType<MovingGameObject>())
            obj.MoveToTarget();
    }
}