using System.Collections.Generic;
using Avalonia.Input;

namespace BattleCity.Models;

static class Keyboard
{
    public static readonly HashSet<Key> Keys = new HashSet<Key>();
    public static bool IsKeyDown(Key key) => Keys.Contains(key);
}