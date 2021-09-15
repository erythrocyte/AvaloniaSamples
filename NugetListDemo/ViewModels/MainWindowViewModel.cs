using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;

namespace NugetListDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var avaVersion = Assembly.GetAssembly(typeof(Avalonia.AvaloniaLocator))?.GetName()?.Version?.ToString() ?? string.Empty;
            Title = $"Nuget List Demo [avalonia: { avaVersion }]";

            AboutAvaloniaCommand = ReactiveCommand.CreateFromTask(AboutAvalonia);
        }

        public string Title { get; }

        public ReactiveCommand<Unit, Unit> AboutAvaloniaCommand { get; }

        [AllowNull]
        public Func<Window> OnGetView;

        private async Task AboutAvalonia()
        {
            // var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
            //     .GetMessageBoxStandardWindow(new MessageBoxStandardParams
            //     {
            //         ButtonDefinitions = ButtonEnum.OkAbort,
            //         ContentTitle = "Title",
            //         ContentMessage = "Message",
            //         Icon = Icon.Plus,
            //         Style = Style.UbuntuLinux
            //     });
            var view = OnGetView?.Invoke();
            // var result = await dialog.ShowAsync(null);

            // if (result != null)
            // {
            //     foreach (var path in result)
            //     {
            //         System.Diagnostics.Debug.WriteLine($"Opened: {path}");
            //     }
            // }
        }
    }

}
