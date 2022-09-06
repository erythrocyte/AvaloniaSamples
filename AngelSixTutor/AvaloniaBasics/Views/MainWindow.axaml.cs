using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.CodeAnalysis.Text;

namespace AvaloniaBasics.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine("Test output");
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{this.DescriptionText.Text}");
        }

        private void ResetButton_OnClick(object? sender, RoutedEventArgs e)
        {
            this.WeldCheckbox.IsChecked = this.AssemblyCheckbox.IsChecked = this.PlasmaCheckbox.IsChecked =
                this.LaserCheckbox.IsChecked = this.PurchaseCheckbox.IsChecked =
                    this.LatheCheckbox.IsChecked = this.DrillCheckbox.IsChecked = this.FoldCheckbox.IsChecked =
                        this.RollCheckbox.IsChecked = this.SawCheckbox.IsChecked = false;
        }

        private void Checkbox_OnChecked(object? sender, RoutedEventArgs e)
        {
            if (sender is CheckBox ch)
                LengthText.Text += ch.Content;
        }
        
        private void SupplierNameText_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (MassText != null)
                this.MassText.Text = SupplierNameText.Text;
        }
    }
}