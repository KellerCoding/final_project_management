using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
using src.Models;

namespace SoftwareProjectManager.ViewModels;

public partial class RiskWindowViewModel : ObservableObject
{
    [ObservableProperty] private int projectIDForTable;
    
    [ObservableProperty]
    private ObservableCollection<Risk> risks = new();

    [ObservableProperty]
    private Risk? riskSelected;
    
    [RelayCommand]
    private void AddRisk()
    {
        risks.Add(new Risk(0," "," ",0));
        
    }
    
    [RelayCommand]
    private void RemoveRisk()
    {
        
        if (riskSelected != null)
        {
            int id = riskSelected.id;
            Risk risk = new Risk();
            risks.Remove(riskSelected);
            risk.DeleteRow(id);
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
           if (risk.id == 0) 
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
               cmd.Parameters.AddWithValue("@ID", risk.id);
           }

           cmd.Parameters.AddWithValue("@NAME", risk.Name);
           cmd.Parameters.AddWithValue("@DESCR", risk.Description);
           cmd.Parameters.AddWithValue("@PROJECTID", risk.ProjectId);

           cmd.ExecuteNonQuery();
       }

       transaction.Commit();
   }



    

}