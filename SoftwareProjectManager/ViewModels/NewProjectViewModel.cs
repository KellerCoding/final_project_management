using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SoftwareProjectManager.Views;
namespace SoftwareProjectManager.ViewModels;

public class NewProjectViewModel
{
    private string? _newProjectTitle;
    
    public string? NewProjectTitle 
    {
        get => _newProjectTitle;
        set => _newProjectTitle = value;
    }
    
    public ICommand PassStringCommand { get; set; }

    public NewProjectViewModel()
    {
        PassStringCommand = ReactiveCommand.Create(() =>
        {
            return _newProjectTitle;
        });
    }

    public NewProjectViewModel(Window owner)
    {
        PassStringCommand = ReactiveCommand.Create(() =>
        {
            return _newProjectTitle;
        });
    }
}