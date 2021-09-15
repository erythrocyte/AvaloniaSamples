using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NugetListDemo.Views;
using NugetListDemo.ViewModels;
using MessageBox.Avalonia;

namespace NugetListDemo
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            base.OnFrameworkInitializationCompleted();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var vm = new MainWindowViewModel();
                vm.OnGetView += () => { return desktop.MainWindow; };
                desktop.MainWindow = new MainWindow();
                desktop.MainWindow.DataContext = vm;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
