using BattleCity.Models;

namespace BattleCity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GameField Field { get; }

        public MainWindowViewModel(GameField field)
        {
            Field = field;
        }
    }
}