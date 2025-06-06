using System;
using System.IO;
using System.Collections;
using Microsoft.Data.Sqlite;
using SoftwareProjectManager.Models;

namespace src.Models;

public class OrgUser
{
    private int UserID;
    private string username, password;

    public OrgUser(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public OrgUser(int tempID, string tempUsername, string tempPassword)
    {
        UserID = tempID;
        username = tempUsername;
        password = tempPassword;
    }

    public int GetUserID()
    {
        return UserID;
    }

    public string GetUsername()
    {
        return username;
    }

    public string GetPassword()
    {
        return password;
    }

    public void SetUserID(int tempID)
    {
        UserID = tempID;
    }

    public void SetUsername(string tempUsername)
    {
        username = tempUsername;
    }

    public void SetPassword(string tempPassword)
    {
        password = tempPassword;
    }

    //if valid return true
    public bool Login(String dataBasePassword)
    {
        return password.Equals(dataBasePassword);
    } 
    
    public String GetDatabasePassword()
    {
        string dataBasePassword = "";
        var sql = "select PASSWORD from ORGUSER where USERNAME = @USERNAME";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();
           

            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@USERNAME", username);

            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataBasePassword = reader.GetString(0);
                    
                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return dataBasePassword;
    }
    
    public static string GetDatabasePath()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "../../.."));
    
        string dbPath = Path.Combine(projectRoot, "projectDB");
    
        //Console.WriteLine("database at:" + dbPath);
        return dbPath;
    }


    public ArrayList ViewProjects()
    {
        ArrayList tempArrayList = new ArrayList();
        var sql = "select * from PROJECT where USERID = @USERID";
        try
        {
            using var connection = new SqliteConnection($"Data Source=" + GetDatabasePath());
            connection.Open();
           

            using var command = new SqliteCommand(sql,connection);
            command.Parameters.AddWithValue("@USERID", UserID);

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
        }
        return tempArrayList;
    }
    
    //Add a new project to the database
    public void AddProject(Project temp)
    {
        var sql = "INSERT INTO PROJECT (ID, NAME, DESCR, USERID) " +
                  "VALUES (@ID, @NAME, @DESCR, @USERID)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", temp.GetID());
            command.Parameters.AddWithValue("@NAME", temp.GetName());
            command.Parameters.AddWithValue("@DESCR", temp.GetDescription());
            command.Parameters.AddWithValue("@USERID", UserID);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    //Add a new user to data after user creates an account
    public void AddNewUser()
    {
        var sql = "INSERT INTO ORGUSER (ID, USERNAME, PASSWORD) " +
                  "VALUES (@ID, @USERNAME, @PASSWORD)";
        try
        {
            using var connection = new SqliteConnection($"Data Source="+GetDatabasePath());
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", UserID);
            command.Parameters.AddWithValue("@USERNAME", username);
            command.Parameters.AddWithValue("@PASSWORD", password);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
  
  
}