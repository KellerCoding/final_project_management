using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SoftwareProjectManager.Models;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public class AddRiskViewModel : ViewModelBase
{
    public ICommand AddRiskCommand { get; set; }
    
    private string? _riskName = string.Empty;
    private string? _riskDescription = string.Empty;
    private string? _tempId = string.Empty;
    private string? _errorMessage = string.Empty;

    private int idConversion;

    public string? RiskName
    {
        get => _riskName;
        set => this.RaiseAndSetIfChanged(ref _riskName, value);
    }

    public string? RiskDescription
    {
        get => _riskDescription;
        set => this.RaiseAndSetIfChanged(ref _riskDescription, value);
    }

    public string? TempId
    {
        get => _tempId;
        set => this.RaiseAndSetIfChanged(ref _tempId, value);
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public AddRiskViewModel()
    {
        
    }

    public AddRiskViewModel(ObservableCollection<Risk> risks, Project project)
    {
        AddRiskCommand = ReactiveCommand.Create(() =>
        {
            if (RiskName.Length > 0 && RiskDescription.Length > 0 && TempId.Length > 0)
            {
                if (RiskName.Length <= 64 && RiskDescription.Length <= 252 && TempId.Length < 10)
                {
                    try
                    {
                        idConversion = int.Parse(TempId);
                        ErrorMessage = "";
                        Console.WriteLine(idConversion);
                        
                        
                        Risk newRisk = new Risk(idConversion, RiskName, RiskDescription, project.GetID());
                        project.AddRisk(newRisk);
                        risks.Add(newRisk);

                        Console.WriteLine(newRisk.GetID());
                        
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