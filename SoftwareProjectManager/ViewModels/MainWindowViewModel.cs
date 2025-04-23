using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Xml;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand LogOut { get; }
    public ICommand AddProjectCommand { get; }
    public ICommand EnableProjectCommand { get; }
    private string? _userName;
    public string? UserName
    {
        get => _userName;
        set => _userName = value;
    }

    private string? _newProjectName;
    public string? NewProjectName { get => _newProjectName; set => _newProjectName = value; }
    
    public bool _isAddingProject;
    public bool IsAddingProject { get => _isAddingProject; private set => this.RaiseAndSetIfChanged(ref _isAddingProject, value); }
    
    public ObservableCollection<Project>? _userProjects { get; private set; }

    
    
    public MainWindowViewModel()
    {
        _userName = "User";
        
        UserName = _userName;

        var userProjects = new List<Project>
        {
            /* Delete this, keeping for reference
             
            new Project("Software Project Manager \n", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager \n", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager", "XAXML"),
            new Project("Operating Systems", "C#"),
            new Project("Software Project Manager", "XAXML"),
            new Project("Operating Systems", "C#"),
            */
        };
        _userProjects = new ObservableCollection<Project>(userProjects);


        EnableProjectCommand = ReactiveCommand.Create(() =>
        {
            IsAddingProject = true;
        });
        
        AddProjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            // Include Project
            IsAddingProject = false;
            //_userProjects.Add(new Project(NewProjectName, "XAXML"));
        });
        
        LogOut = ReactiveCommand.Create(() =>
        {
            /*
            Console.WriteLine("LogOut successful");
            var window = new LoginWindow();
            window.Show();
                
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                Console.WriteLine(mainWindow.Title);
                mainWindow.Close();
            }
            */
            
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                Console.WriteLine(mainWindow.Title);
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
    }
    
    public MainWindowViewModel(string _userName)
    {
        UserName = _userName;

        var userProjects = new List<Project>
        {
            // See default constructor
        };
        _userProjects = new ObservableCollection<Project>(userProjects);
        
        AddProjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var newWindow = new LoginWindow();
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            
            await newWindow.ShowDialog(mainWindow);
            
        });
        
        LogOut = ReactiveCommand.Create(() =>
        {
            /*
            Console.WriteLine("LogOut successful");
            var window = new LoginWindow();
            window.Show();

            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                Console.WriteLine(mainWindow.Title);
                mainWindow.Close();
            }
            */
            
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow != null)
            {
                Console.WriteLine(mainWindow.Title);
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
    }
}