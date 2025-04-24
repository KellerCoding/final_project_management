using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SoftwareProjectManager.ViewModels;

namespace SoftwareProjectManager.Views;

public partial class EmployeeWindow : Window
{
    public EmployeeWindow()
    {
        InitializeComponent();
        
        var vm = new EmployeeWindowViewModel();
        DataContext = vm;
        
    }
}