using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class AddProjectViewModel : ViewModelBase
{
    public ICommand AddProjectCommand { get; set; }
    
    private string? _projectName;
    private string? _projectDescription;
    private string? _tempId;
    private string? _errorMessage;

    private int idConversion;

    public string ProjectName
    {
        get => _projectName;
        set => _projectName = value;
    }

    public string ProjectDescription
    {
        get => _projectDescription;
        set => _projectDescription = value;
    }

    public string TempId
    {
        get => _tempId;
        set => _tempId = value;
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        
    }

    public AddProjectViewModel()
    {
        
    }

    public AddProjectViewModel(OrgUser user, Window mainWindow)
    {
        AddProjectCommand = ReactiveCommand.Create(() =>
        {
            if (ProjectName.Length > 0 && ProjectDescription.Length > 0 && TempId.Length > 0)
            {
                if (ProjectName.Length <= 64 && ProjectDescription.Length <= 252 && TempId.Length < 10)
                {
                    try
                    {
                        idConversion = int.Parse(TempId);
                        ErrorMessage = "";
                        
                        Project newProject = new Project(idConversion, ProjectName, ProjectDescription);
                        
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
                        
                        // Place this line above the closing code.
                        user.AddProject(newProject);

                        
                    }
                    catch (Exception e)
                    {
                        ErrorMessage = "Please enter a valid Id";
                    }
                }
            }
        });
    }
}