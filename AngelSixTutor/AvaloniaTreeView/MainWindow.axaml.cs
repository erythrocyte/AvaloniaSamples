using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;

namespace AvaloniaTreeView;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TopLevel_OnOpened(object? sender, EventArgs e)
    {
        var items = new List<TreeViewItem>();
        int max_count = 3, count = 0;
        foreach (var drive in Directory.GetLogicalDrives())
        {
            if (count > max_count)
                break;

            var item = new TreeViewItem();
            item.Header = drive;
            item.Name = drive;
            // Template = new ControlTemplate
            // {
            //     Content = drive
            // };

            item.Items = new List<TreeViewItem> { null };
            item.IsExpanded = true;


            items.Add(item);

            count++;
        }

        FolderView.Items = items;
    }
}