using System;
using System.Collections;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Data.Sqlite;
using SoftwareProjectManager.ViewModels;
using SoftwareProjectManager.Views;
using src.Models;
using Tmds.DBus.Protocol;

namespace SoftwareProjectManager.Models;

public class Project
{
    //Attributes
    private int ID;
    public string Name { get; set; }

    private string Description;
    /*
    //Team members & manager
    private Employee ProjectManager { get; set; }
    private ArrayList TeamMembers = new ArrayList();
    //Requirements and Risks
    private ArrayList FunctionalRequirements = new ArrayList();
    private ArrayList NonFunctionalRequirements = new ArrayList();
    private ArrayList RiskList = new ArrayList();
    private SqliteConnection sqliteConnection;*/
    
    public Project(int tempID, string tempName, string tempDescription)
    {
        ID = tempID;
        Name = tempName;
        Description = tempDescription;
    }

    public void ViewProject(Project tempProject)
    {
                
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new FunctionalRequirementsWindow()
            {
                DataContext = new FunctionalRequirementsViewModel(tempProject)
            };
                    
            desktop.MainWindow.Show();
        }
    }
    
    public void ViewRisks(Project tempProject)
    {
                
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new RiskWindowView()
            {
                DataContext = new RiskWindowViewModel(tempProject)
            };
                    
            desktop.MainWindow.Show();
        }
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
    
    //Fetching relevant data from database for front-end
    //SQL method to avoid redundancy for getting info from tables using project id
    ArrayList DatabaseCall(string sql)
    {
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@PROJECTID",ID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    //Pretty much the same method with new parameter for the relevant ID
    ArrayList DatabaseCall(string sql, int tempID)
    {
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@ID",tempID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    //Fetch one row from table, returns arraylist with 1? element, takes in an ID from the user
    public ArrayList GetTeamMember(int tempID)
    {
        string sql = "Select * from EMPLOYEE where ID = @ID";
        return DatabaseCall(sql, tempID);
    }
    public ArrayList GetPhase(int tempID)
    {
        string sql = "Select * from PHASE where ID = @ID";
        return DatabaseCall(sql, tempID);
    }
    
    public ArrayList GetFunctionalReq(int tempID)
    {
        string sql = "Select * from FREQUIREMENT where ID = @ID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@ID",tempID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(2));
                    tempArrayList.Add(reader.GetString(3));
                    tempArrayList.Add(reader.GetString(4));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    public ArrayList GetNonFunctionalReq(int tempID)
    {
        string sql = "Select * from NFREQUIREMENT where ID = @ID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@ID",tempID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(2));
                    tempArrayList.Add(reader.GetString(3));
                    tempArrayList.Add(reader.GetString(4));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    public ArrayList GetRisk(int tempID)
    {
        string sql = "Select * from RISK where ID = @ID";
        return DatabaseCall(sql, tempID);
    }
    //Fetch all related data
    public ArrayList GetTeamMembers()
    {
        var sql = "Select * from EMPLOYEE where PROJECTID = @PROJECTID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@PROJECTID",ID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(2));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;;
    }
    public ArrayList GetPhases()
    {
        var sql = "Select * from PHASE where PROJECTID = @PROJECTID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@PROJECTID",ID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(3));
                    tempArrayList.Add(reader.GetString(4));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    public ArrayList GetFunctionalReqs()
    {
        var sql = "Select * from FREQUIREMENT where PROJECTID = @PROJECTID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@PROJECTID",ID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(2));
                    tempArrayList.Add(reader.GetString(3));
                    tempArrayList.Add(reader.GetString(4));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    public ArrayList GetNonFunctionalReqs()
    {
        var sql = "Select * from NFRequirement where PROJECTID = @PROJECTID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@PROJECTID",ID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(2));
                    tempArrayList.Add(reader.GetString(3));
                    tempArrayList.Add(reader.GetString(4));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    public ArrayList GetRisks()
    {
        var sql = "Select * from RISK where PROJECTID = @PROJECTID";
        ArrayList tempArrayList = new ArrayList();
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
            
            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@PROJECTID",ID);
            
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tempArrayList.Add(reader.GetString(0));
                    tempArrayList.Add(reader.GetString(1));
                    tempArrayList.Add(reader.GetString(2));
                    tempArrayList.Add(reader.GetString(3));

                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return tempArrayList;
    }
    
    //Inserting values into the database
    public void AddEmployee(Employee temp)
    {
        var sql = "INSERT INTO EMPLOYEE (ID, NAME, JOB_TITLE, PROJECTID) " +
                  "VALUES (@ID, @NAME, @JOB_TITLE, @PROJECTID)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@PROJECTID", ID);
            command.Parameters.AddWithValue("@ID", temp.GetID());
            command.Parameters.AddWithValue("@NAME", temp.GetName());
            command.Parameters.AddWithValue("@JOB_TITLE", temp.GetJobTitle());

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void AddPhase(Phase temp)
    {
        var sql = "INSERT INTO PHASE (ID, NAME, DESCR, WEEKLYPERSONHOURS, TOTALPERSONHOURS, PROJECTID) " +
                  "VALUES (@ID, @NAME, @DESCR, @WEEKLYPERSONHOURS, @TOTALPERSONHOURS, @PROJECTID)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@PROJECTID", ID);
            command.Parameters.AddWithValue("@ID", temp.GetID());
            command.Parameters.AddWithValue("@NAME", temp.GetName());
            command.Parameters.AddWithValue("@DESCR", temp.GetDescription());
            //can't pass a double??
            command.Parameters.Add("@WEEKLYPERSONHOURS", 0);
            command.Parameters.Add("@TOTALPERSONHOURS", 0);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    void AddFunctionalReq(Requirement temp)
    {
        var sql = "INSERT INTO FREQUIREMENT (ID, NAME, DESCR, STATUS, PRIORITY, PROJECTID) " +
                  "VALUES (@ID, @NAME, @DESCR, @STATUS, @PRIOIRTY, @PROJECTID)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@PROJECTID", ID);
            command.Parameters.AddWithValue("@ID", temp.GetID());
            command.Parameters.AddWithValue("@NAME", temp.GetName());
            command.Parameters.AddWithValue("@DESCR", temp.GetDescription());
            command.Parameters.AddWithValue("@STATUS", temp.GetFinished());
            command.Parameters.AddWithValue("@PRIORITY", temp.GetPriority());

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    void AddNonFunctionalReq(Requirement temp)
    {
        var sql = "INSERT INTO NFREQUIREMENT (ID, NAME, DESCR, STATUS, PRIORITY, PROJECTID) " +
                  "VALUES (@ID,@NAME, @DESCR, @STATUS, @PRIOIRTY, @PROJECTID)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@PROJECTID", ID);
            command.Parameters.AddWithValue("@ID", temp.GetID());
            command.Parameters.AddWithValue("@NAME", temp.GetName());
            command.Parameters.AddWithValue("@DESCR", temp.GetDescription());
            command.Parameters.AddWithValue("@STATUS", temp.GetFinished());
            command.Parameters.AddWithValue("@PRIORITY", temp.GetPriority());

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddRisk(Risk temp)
    {
        var sql = "INSERT INTO RISK (ID, NAME, DESCR, PROJECTID) " +
                  "VALUES (@ID, @NAME, @DESCR, @PROJECTID)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@PROJECTID", ID);
            command.Parameters.AddWithValue("@ID", temp.GetID());
            command.Parameters.AddWithValue("@NAME", temp.GetName());
            command.Parameters.AddWithValue("@DESCR", temp.GetDescription());

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Updating data in tables
    //Update Requirement status & priority
    //Update Phase WeeklyHours & TotalHours

    void UpdateTable(string sql, int tempID)
    {
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", tempID);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    void UpdateTable(string sql, int tempID, int tempStatus)
    {
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", tempID);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    void MarkFunctionalReqComplete(int FunctionalReqID)
    {
        var sql = "UPDATE FREQUIREMENT SET STATUS SET STATUS = 1 WHERE ID = @ID";
        UpdateTable(sql, FunctionalReqID);
    }

    void MarkNonFunctionalReqComplete(int NonFunctionalReqID)
    {
        var sql = "UPDATE NFREQUIREMENT SET STATUS SET STATUS = 1 WHERE ID = @ID";
        UpdateTable(sql, NonFunctionalReqID);
    }

    void ChangeFunctionalReqPriority(int FuntionalReqID, int tempPriority)
    {
        var sql = "UPDATE FREQUIREMENT SET PRIORITY = @PRIORITY WHERE ID = @ID";
        UpdateTable(sql, FuntionalReqID, tempPriority);
    }
    void ChangeNonFunctionalReqPriority(int FuntionalReqID, int tempPriority)
    {
        var sql = "UPDATE NFREQUIREMENT SET PRIORITY = @PRIORITY WHERE ID = @ID";
        UpdateTable(sql, FuntionalReqID, tempPriority);
    }
    
    public void UpdatePhaseWeeklyHours(int PhaseID, double tempHours)
    {
        var sql = "UPDATE PHASE SET WEEKLYPERSONHOURS = @WEEKLYHOURS WHERE ID = @ID";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", PhaseID);
            command.Parameters.AddWithValue("@WEEKLYHOURS", tempHours);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public void UpdatePhaseTotalHours(int PhaseID, double tempHours)
    {
        var sql = "UPDATE PHASE SET TOTALPERSONHOURS = @TOTALHOURS WHERE ID = @ID";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", PhaseID);
            command.Parameters.AddWithValue("@TOTALHOURS", tempHours);

            command.ExecuteNonQuery();
            connection.Close();
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
}