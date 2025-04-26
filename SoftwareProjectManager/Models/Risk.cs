using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Data.Sqlite;
using SQLitePCL;
namespace src.Models;

public class Risk
{
    public int ID {get; set;}
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ObservableCollection<Risk> RisksListCollection { get; set; } = new();
    public Risk(int tempId,string tempName, string tempDescription, int tempProjectId)
    {
        ID = tempId;
        Name = tempName;
        Description = tempDescription;
        ProjectId = tempProjectId;
    }

    public Risk()
    {
        
    }
    public int GetID()
    {
        return ID;
    }

    public string GetName()
    {
        return Name;
    }

    public string GetDescription()
    {
        return Description;
    }

    public void SetID(int temp)
    {
        ID = temp;
    }

    public void SetName(string temp)
    {
        Name = temp;
    }

    public void SetDescription(string temp)
    {
        Description = temp;
    }
    
    
  

    public void CreateListByID(int projectId)
    {
        // first check to see if projectID is valid because it will crash due to fk constraint
        if (!isProjectIDValid(projectId)) return;
        
        //TODO add where ProjectID later
        var sql = "SELECT ID, NAME, DESCR, PROJECTID FROM RISK where PROJECTID = @PROJECTID";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection); 
            command.Parameters.AddWithValue("@PROJECTID", projectId);


            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ID = reader.GetInt32(0);
                Name = reader.GetString(1);
                Description = reader.GetString(2);
                ProjectId = reader.GetInt16(3);
                //Console.WriteLine(id + " " + Name  + " " +  Description + " " +  ProjectID);
                RisksListCollection.Add(new Risk(ID,Name,Description,ProjectId));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public void CreateList()
    {
        var sql = "SELECT ID, NAME, DESCR, PROJECTID FROM RISK";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection); 
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ID = reader.GetInt32(0);
                Name = reader.GetString(1);
                Description = reader.GetString(2);
                ProjectId = reader.GetInt16(3);
                RisksListCollection.Add(new Risk(ID,Name,Description,ProjectId));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //function returns false if ID does not exist
    public bool isProjectIDValid(int id)
    {
        using var connection = new SqliteConnection($"Data Source=" + GetDatabasePath());
        connection.Open();
        var sql = "SELECT ID FROM PROJECT WHERE ID = @ID";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@ID", id);
        using var reader = command.ExecuteReader();
        
        return reader.Read();
    }
    
    
    
/*may not need
    public void AddList(int id,string name,string description,int projectId)
    {
        var sql = "INSERT INTO RISK (ID, NAME, DESCR, PROJECTID) VALUES (@ID, @NAME, @DESCR, @PROJECTID)";

        try
        {
            using var connection = new SqliteConnection($"Data Source=" + GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@NAME", name);
            command.Parameters.AddWithValue("@DESCR", description);
            command.Parameters.AddWithValue("@PROJECTID", projectId);

            int rowsAffected = command.ExecuteNonQuery();
        }
        catch (SqliteException exception)
        {
            Console.WriteLine($"SQLite error: {exception.Message}");
        }

    }
    */


    public void DeleteRow(int Id)
    {
        using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            var sql = "DELETE FROM RISK WHERE ID = @ID";
            using var command = new SqliteCommand(sql, connection, transaction);
            command.Parameters.AddWithValue("@ID", Id);
        
            command.ExecuteNonQuery();
        
            transaction.Commit();
        
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    
    }
   
    public static string GetDatabasePath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "../../.."));
    
        string dbPath = Path.Combine(projectRoot, "projectDB");
    
        //Console.WriteLine("database at:" + dbPath);
        return dbPath;
    }

    public ObservableCollection<Risk> RetrieveRisks()
    {
        CreateList();
        return RisksListCollection;
    }

    public ObservableCollection<Risk> RetrieveRisksById(int id)
    {
        CreateListByID(id);
        return RisksListCollection;
    }

   

}