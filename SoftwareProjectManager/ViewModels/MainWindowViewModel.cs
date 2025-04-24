using System;
using System.Collections;
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

    public OrgUser user;
    
    
    
    public ObservableCollection<string>? _userProjects { get; private set; }



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
            projectNames = user.ViewProjects();
        }
        catch (Exception e)
        {
            
        }

        foreach (string project in projectNames)
        {
            _userProjects.Add(project);
        }
        
        AddProjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new AddProjectWindow()
                {
                    DataContext = new AddProjectViewModel(this.user)
                };

                desktop.MainWindow.Show();
            }
            
        });
        
        LogOut = ReactiveCommand.Create(() =>
        {
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