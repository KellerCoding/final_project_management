using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Metadata;
using ReactiveUI;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class FunctionalRequirementsViewModel : ViewModelBase
{
    
    public ICommand SwitchRequirementsCommand { get; }
    Project _project;
    public Project Project { get; }
    
    public string? Title { get; set; }
    public string? ID { get; set; }
    
    private ArrayList _requirements = new ArrayList();
    
    public ObservableCollection<Requirement> Requirements { get; }
    

    public FunctionalRequirementsViewModel(Project project)
    {
        Project = project;
        Title = Project.GetName();
        ID = Convert.ToString(Project.GetID());

        _requirements = Project.GetFunctionalReq(Project.GetID());

        foreach (Requirement req in _requirements)
        {
            Requirements.Add(req);
        }

        SwitchRequirementsCommand = ReactiveCommand.Create(() =>
        {
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Hide();
            }

                
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new NonFunctionalRequirementsWindow()
                {
                    DataContext = new NonFunctionalRequirementsViewModel(project)
                };
                    
                desktop.MainWindow.Show();
            }
        });





    }
    
    // Requires a default constructor
    public FunctionalRequirementsViewModel()
    {
        
    }
}