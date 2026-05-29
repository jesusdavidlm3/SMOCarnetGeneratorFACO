using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using CarnetGeneratorFACO.ViewModels;
using CarnetGeneratorFACO.Views;
using QuestPDF.Infrastructure;

namespace CarnetGeneratorFACO;

public partial class App : Application
{
    public override void Initialize()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}