using Avalonia;

namespace BattleCity.Models;

public class CellLocation
{
    public CellLocation(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    // public bool Equals(object? other)
    // {
    //     return X == other.X && Y == other.Y;
    // }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (obj is CellLocation other)
            return X == other.X && Y == other.Y;

        return false;
    }

    public static bool operator ==(CellLocation l1, CellLocation l2)
    {
        if (l1 is null && l2 is null)
            return true;

        if (l1 is null)
            return false;
        
        return l1.Equals(l2);
    }

    public static bool operator !=(CellLocation l1, CellLocation l2)
    {
        return !(l1 == l2);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X * 397) ^ Y;
        }
    }

    public override string ToString()
    {
        return $"({X}:{Y})";
    }

    public Point ToPoint()
    {
        return new(GameField.CellSize * X, GameField.CellSize * Y);
    }

    public CellLocation WithX(int x)
    {
        return new(x, Y);
    }

    public CellLocation WithY(int y)
    {
        return new(X, y);
    }
}