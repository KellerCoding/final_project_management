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
using SoftwareProjectManager.Models;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class NonFunctionalRequirementsViewModel : ViewModelBase
{
    public ICommand SwitchRequirementsCommand { get; }
    public ICommand HomeCommand { get; }
    Project _project;
    public Project Project { get; }
    
    public string? Title { get; set; }
    public string? ID { get; set; }
    
    private ArrayList reqData = new ArrayList();
    
    private ObservableCollection<Requirement> _requirements = new ObservableCollection<Requirement>();

    public ObservableCollection<Requirement> Requirements
    {
        get => _requirements;
        set => this.RaiseAndSetIfChanged(ref _requirements, value);
    }
    

    public NonFunctionalRequirementsViewModel(Project project)
    {
        Project = project;
        Title = Project.GetName();
        ID = Convert.ToString(Project.GetID());
        
        reqData = project.GetNonFunctionalReqs();

        for (int i = 0; i < reqData.Count - 4; i++)
        {
            if (i % 5 == 0)
            {
                Requirement newReq = new Requirement(Convert.ToInt32(reqData[i]), Convert.ToString(reqData[i+1]), Convert.ToString(reqData[i+2]), Convert.ToInt32(reqData[i+4]));
                Requirements.Add(newReq);
            }
        }


        HomeCommand = ReactiveCommand.Create(() =>
        {
            var mainWindow =
                (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
                ?.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Hide();
            }
                        
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = mainWindow;
            }
        });

        SwitchRequirementsCommand = ReactiveCommand.Create(() =>
        {
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Hide();
            }

                
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new FunctionalRequirementsWindow()
                {
                    DataContext = new FunctionalRequirementsViewModel(project)
                };
                    
                desktop.MainWindow.Show();
            }
        });





    }
    
    // Requires a default constructor
    public NonFunctionalRequirementsViewModel()
    {
        
    }
}