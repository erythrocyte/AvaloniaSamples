using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BattleCity.Models;
using BattleCity.ViewModels;
using BattleCity.Views;

namespace BattleCity
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var field = new GameField();
                var game = new Game(field);
                game.Start();
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(field),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}