using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
using ReactiveUI;
using SoftwareProjectManager.Models;
using SoftwareProjectManager.Views;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public partial class RiskWindowViewModel : ViewModelBase
{
    
    public ICommand HomeCommand { get; }
    public ICommand AddRiskCommand { get; }
    private int _projectIDForTable;

    public int ProjectIDForTable
    {
        get => _projectIDForTable;
        set => this.RaiseAndSetIfChanged(ref _projectIDForTable, value);
    }
    
    private ArrayList riskData = new ArrayList();
    
    private ObservableCollection<Risk> _risks = new ObservableCollection<Risk>();
    public ObservableCollection<Risk> Risks { get => _risks; set => this.RaiseAndSetIfChanged(ref _risks, value); }

    private Risk? _riskSelected;
    public Risk? RiskSelected { get => _riskSelected; set => this.RaiseAndSetIfChanged(ref _riskSelected, value); }
    
    
    [RelayCommand]
    private void RemoveRisk()
    {
        try
        {
            int id = RiskSelected.GetID();
            Risk risk = new Risk();
            Risks.Remove(RiskSelected);
            //risk.DeleteRow(id);
            using var connection = new SqliteConnection($"Data Source={Risk.GetDatabasePath()}");
            connection.Open();

            var sql = "DELETE FROM RISK WHERE ID = @ID";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            
        }
    }

    [RelayCommand]
    private void tableByProjectID()
    {
        Risk risk = new Risk();
        Risks = risk.RetrieveRisksById(ProjectIDForTable);
    }
    
   public RiskWindowViewModel()
   {
       Risk risk = new Risk();
       Risks = risk.RetrieveRisks();
   }

   public RiskWindowViewModel(Project project)
   {
       riskData = project.GetRisks();
       for (int i = 0; i < riskData.Count - 3; i++)
       {
           if (i % 4 == 0)
           {
               Risk newRisk = new Risk(Convert.ToInt32(riskData[i]), Convert.ToString(riskData[i + 1]), 
                   Convert.ToString(riskData[i + 2]), Convert.ToInt32(riskData[i + 3]));
               Risks.Add(newRisk);
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

       AddRiskCommand = ReactiveCommand.Create(() =>
       {
           var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            
           if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
           {
               desktop.MainWindow = new AddRiskWindow()
               {
                   DataContext = new AddRiskViewModel(Risks, project)
               };

               desktop.MainWindow.Show();
           }
       });
   }
   
   
   [RelayCommand]
   private void SaveChanges()
   {
       
       using var connection = new SqliteConnection($"Data Source={Risk.GetDatabasePath()}");
       connection.Open();
       using var transaction = connection.BeginTransaction();
        
       foreach (var risk in Risks)
       {
           
           if (!risk.isProjectIDValid(risk.ProjectId)) return;
           
           
            //because primary key does not auto increment
               Random rnd = new Random();
               int id = rnd.Next(1, 10_000_001);
           
           var cmd = connection.CreateCommand();
           if (risk.GetID() == 0) 
           {
               cmd.CommandText = @"
                INSERT INTO RISK (id,NAME, DESCR, PROJECTID) 
                VALUES (@ID,@NAME, @DESCR, @PROJECTID)";
               cmd.Parameters.AddWithValue("@ID", id);
              
           }
           else 
           {
               cmd.CommandText = @"
                UPDATE RISK 
                SET NAME = @NAME, DESCR = @DESCR, PROJECTID = @PROJECTID 
                WHERE ID = @ID";
               cmd.Parameters.AddWithValue("@ID", risk.GetID());
           }

           cmd.Parameters.AddWithValue("@NAME", risk.Name);
           cmd.Parameters.AddWithValue("@DESCR", risk.Description);
           cmd.Parameters.AddWithValue("@PROJECTID", risk.ProjectId);

           cmd.ExecuteNonQuery();
       }

       transaction.Commit();
   }
   

}