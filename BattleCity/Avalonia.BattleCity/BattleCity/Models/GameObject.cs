using Avalonia;
using BattleCity.Utils.Converters;
using ReactiveUI;

namespace BattleCity.Models;

public abstract class GameObject : ReactiveObject
{
    private Point _location;

    protected GameObject(Point location)
    {
        Location = location;
    }

    public Point Location
    {
        get => _location;
        set => this.RaiseAndSetIfChanged(ref _location, value);
    }

    public virtual int Layer => 0;
}