using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reactive;
using Avalonia.Media.Imaging;
using NuGet.Protocol.Core.Types;
using NugetListDemo.ViewModels;
using ReactiveUI;

public class NugetDetailsViewModel : ViewModelBase
{
    private readonly IPackageSearchMetadata _metadata;
    private readonly Uri _defaultUrl;

    public NugetDetailsViewModel(IPackageSearchMetadata metadata)
    {
        _metadata = metadata;
        _defaultUrl = new Uri("https://git.io/fAlfh");
        OpenPageCommand = ReactiveCommand.Create(() =>
        {
            Process.Start(new ProcessStartInfo(this.ProjectUrl.ToString())
            {
                UseShellExecute = true
            });
        });
    }

    public Uri IconUrl => _metadata.IconUrl ?? _defaultUrl;
    public string Description => _metadata.Description;
    public Uri ProjectUrl => _metadata.ProjectUrl;
    public string Title => _metadata.Title;

    public Bitmap NugetImage => GetNugetImage();

    // ReactiveCommand allows us to execute logic without exposing any of the 
    // implementation details with the View. The generic parameters are the 
    // input into the command and its output. In our case we don't have any 
    // input or output so we use Unit which in Reactive speak means a void type.
    public ReactiveCommand<Unit, Unit> OpenPageCommand { get; }

    private Bitmap GetNugetImage()
    {
        using (var webClient = new WebClient())
        {
            byte[] imageBytes = webClient.DownloadData(IconUrl);
            using (var stream = new MemoryStream(imageBytes))
            {
                return IconUrl == null ? null : new Bitmap(stream);
            }
        }
    }
}