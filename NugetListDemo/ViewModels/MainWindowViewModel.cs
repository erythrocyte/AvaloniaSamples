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
using System.Threading;
using System.Collections.Generic;
using NuGet.Protocol.Core.Types;
using NuGet.Common;
using NuGet.Configuration;
using System.Linq;
using System.Reactive.Linq;

namespace NugetListDemo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // In ReactiveUI, this is the syntax to declare a read-write property
        // that will notify Observers, as well as WPF, that a property has 
        // changed. If we declared this as a normal property, we couldn't tell 
        // when it has changed!
        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        }

        // Here's the interesting part: In ReactiveUI, we can take IObservables
        // and "pipe" them to a Property - whenever the Observable yields a new
        // value, we will notify ReactiveObject that the property has changed.
        // 
        // To do this, we have a class called ObservableAsPropertyHelper - this
        // class subscribes to an Observable and stores a copy of the latest value.
        // It also runs an action whenever the property changes, usually calling
        // ReactiveObject's RaisePropertyChanged.
        private readonly ObservableAsPropertyHelper<IEnumerable<NugetDetailsViewModel>> _searchResults;
        public IEnumerable<NugetDetailsViewModel> SearchResults => _searchResults.Value;

        // Here, we want to create a property to represent when the application 
        // is performing a search (i.e. when to show the "spinner" control that 
        // lets the user know that the app is busy). We also declare this property
        // to be the result of an Observable (i.e. its value is derived from 
        // some other property)
        private readonly ObservableAsPropertyHelper<bool> _isAvailable;
        public bool IsAvailable => _isAvailable.Value;

        public MainWindowViewModel()
        {
            Title = $"Nuget List Demo { ProgramVersion.Invoke() }";

            AboutAvaloniaCommand = ReactiveCommand.CreateFromTask(AboutAvalonia);
            CloseExecutableCommand = ReactiveCommand.Create(CloseExecutable);

            _searchResults = this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Where(term => !string.IsNullOrWhiteSpace(term))
                .SelectMany(SearchNuGetPackages)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.SearchResults);

            // We subscribe to the "ThrownExceptions" property of our OAPH, where ReactiveUI 
            // marshals any exceptions that are thrown in SearchNuGetPackages method. 
            // See the "Error Handling" section for more information about this.
            _searchResults.ThrownExceptions.Subscribe(error => { /* Handle errors here */ });

            // A helper method we can use for Visibility or Spinners to show if results are available.
            // We get the latest value of the SearchResults and make sure it's not null.
            _isAvailable = this
                .WhenAnyValue(x => x.SearchResults)
                .Select(searchResults => searchResults != null)
                .ToProperty(this, x => x.IsAvailable);
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

        private async Task<IEnumerable<NugetDetailsViewModel>> SearchNuGetPackages(
    string term, CancellationToken token)
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3()); // Add v3 API support
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var source = new SourceRepository(packageSource, providers);
            ILogger logger = NullLogger.Instance;

            var filter = new SearchFilter(false);
            var resource = await source.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);
            var metadata = await resource.SearchAsync(term, filter, 0, 10, logger, token).ConfigureAwait(false);
            return metadata.Select(x => new NugetDetailsViewModel(x));
        }
    }

}
