using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.Enums;
using NugetListDemo.ViewModels;

namespace NugetListDemo.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public void Subscribe()
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.OnShowMessageBox += ShowMessageBox;
                viewModel.OnCloseExecutable += CloseExecutable;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private Task ShowMessageBox(IMsBoxWindow<ButtonResult> msBoxWindow)
        {
            return msBoxWindow.ShowDialog(this);
        }

        private void CloseExecutable()
        {
            this.Close();
        }
    }
}