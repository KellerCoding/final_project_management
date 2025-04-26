using Avalonia.Controls;
using SoftwareProjectManager.ViewModels;

namespace SoftwareProjectManager.Views;

public partial class ProductivityWindow : Window
{
    public ProductivityWindow()
    {
        InitializeComponent();
        var vm = new ProductivityWindowViewModel();
        DataContext = vm;
    }
}