using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SoftwareProjectManager.ViewModels;
using SoftwareProjectManager.Views;
using SQLitePCL;

namespace SoftwareProjectManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Batteries.Init();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new LoginWindow()
            {
                DataContext = new LoginWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}