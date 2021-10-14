using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.BaseWindows.Base;

namespace NugetListDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Title = $"Nuget List Demo { ProgramVersion.Invoke() }";

            AboutAvaloniaCommand = ReactiveCommand.CreateFromTask(AboutAvalonia);
            CloseExecutableCommand = ReactiveCommand.Create(CloseExecutable);
        }

        public string Title { get; }

        public ReactiveCommand<Unit, Unit> AboutAvaloniaCommand { get; }
        public ReactiveCommand<Unit, Unit> CloseExecutableCommand { get; }

        [MaybeNull]
        public Func<IMsBoxWindow<ButtonResult>, Task> OnShowMessageBox;

        [DisallowNull]
        public Action OnCloseExecutable;

        private async Task AboutAvalonia()
        {
            var msBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "About avalonia",
                    ContentMessage = GetContentAboutAvalonia(),
                    ShowInCenter = true,
                    ContentHeader = $"Avalonia version { GetAvaloniaVersion() }",
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Icon = Icon.Info,
                    Style = Style.Windows
                });
            await OnShowMessageBox?.Invoke(msBoxStandardWindow);
        }

        private string GetAvaloniaVersion()
        {
            return Assembly.GetAssembly(typeof(Avalonia.AvaloniaLocator))?.GetName()?.Version?.ToString() ?? string.Empty;
        }

        private string GetContentAboutAvalonia()
        {
            return string.Concat(
                "Avalonia is a cross-platform XAML-based UI framework " + Environment.NewLine +
                "providing a flexible styling system and supporting a wide range of " + Environment.NewLine +
                "Operating Systems such as Windows via .NET Framework and " + Environment.NewLine +
                ".NET Core, Linux via Xorg, macOS",
                Environment.NewLine,
                Environment.NewLine,
                "For more information see the https://avaloniaui.net/"
            );
        }

        private Func<string> ProgramVersion = () =>
        {
            // todo: from resouce file;
            return "v0.1";
        };

        private void CloseExecutable()
        {
            OnCloseExecutable?.Invoke();
        }
    }

}
