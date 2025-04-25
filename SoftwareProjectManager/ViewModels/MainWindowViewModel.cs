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
using Avalonia.Logging;
using ReactiveUI;
using SoftwareProjectManager.Models;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand LogOut { get; }
    public ICommand AddProjectCommand { get; }
    public ICommand ViewProjectCommand { get; }
    private string? _userName;
    public string? UserName
    {
        get => _userName;
        set => _userName = value;
    }

    public OrgUser user;
    
    
    
    private ObservableCollection<Project>? _userProjects = new ObservableCollection<Project>();

    public ObservableCollection<Project>? UserProjects
    {
        get => _userProjects;
        set => _userProjects = value;
    }



    public MainWindowViewModel()
    {
        // Default Required
    }
    public MainWindowViewModel(OrgUser user)
    {
        this.user = user;
        UserName = user.GetUsername();
        ArrayList projectNames = new ArrayList();

        try
        {
            UserProjects.Add(new Project(5, "temp", "temp"));
            UserProjects.Add(new Project(6, "temp2", "temp2"));
            UserProjects.Add(new Project(7, "temp3", "temp3"));
            //projectNames = user.ViewProjects();
        }
        catch (Exception e)
        {

        }

        foreach (string project in projectNames)
        {
            try
            {
                _userProjects.Add(new Project(0, project, project));
            }
            catch (Exception e)
            {
                
            }
        }
        
        AddProjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new AddProjectWindow()
                {
                    DataContext = new AddProjectViewModel(this.user, mainWindow)
                };

                desktop.MainWindow.Show();
            }

        });
        
        LogOut = ReactiveCommand.Create(() =>
        {
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Hide();
            }

                
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new LoginWindow()
                {
                    DataContext = new LoginWindowViewModel()
                };
                    
                desktop.MainWindow.Show();
            }
        });

        ViewProjectCommand = ReactiveCommand.Create((Project project) =>
        {
            Console.WriteLine(project.GetID());
        });
        
    }

    private void ViewProject(Project project)
    {
        Console.WriteLine(project.GetID());
    }
}