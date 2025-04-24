using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SoftwareProjectManager.ViewModels;
using SoftwareProjectManager.Views;

namespace SoftwareProjectManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new EmployeeWindow()
            {
                DataContext = new EmployeeWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}