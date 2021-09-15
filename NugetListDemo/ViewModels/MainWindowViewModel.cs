using System.Reflection;

namespace NugetListDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var avaVersion = Assembly.GetAssembly(typeof(Avalonia.AvaloniaLocator))?.GetName()?.Version?.ToString() ?? string.Empty;
            Title = $"Nuget List Demo [avalonia: { avaVersion }]";
        }

        public string Title {get;}
    }

}
