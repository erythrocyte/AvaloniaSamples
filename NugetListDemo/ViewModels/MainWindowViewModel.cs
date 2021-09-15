﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace NugetListDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Title = $"Nuget List Demo v0.1";

            AboutAvaloniaCommand = ReactiveCommand.CreateFromTask(AboutAvalonia);
        }

        public string Title { get; }

        public ReactiveCommand<Unit, Unit> AboutAvaloniaCommand { get; }

        private async Task AboutAvalonia()
        {
            var msBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "Title",
                    ContentMessage = GetContentAboutAvalonia(),
                    Icon = Icon.Info,
                    Style = Style.Windows
                });
            var result = await msBoxStandardWindow.Show();
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
                "More information at: https://avaloniaui.net/",
                Environment.NewLine,
                Environment.NewLine,
                $"Avalonia version { GetAvaloniaVersion() }"
            );
        }
    }

}
