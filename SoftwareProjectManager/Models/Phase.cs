namespace src.Models;

public class Phase
{
    private int ID;
    private string Name;
    private string Description;
    private double WeeklyPersonHours;
    private double TotalPersonHours;

    public Phase(int tempID, string tempName, string tempDesc)
    {
        ID = tempID;
        Name = tempName;
        Description = tempDesc;
        WeeklyPersonHours = 0;
        TotalPersonHours = 0;
    }
    //Getters and Setters
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

    public double GetWeeklyPersonHours()
    {
        return WeeklyPersonHours;
    }

    public double GetTotalPersonHours()
    {
        return TotalPersonHours;
    }

    public void AddWeeklyPersonHours(int tempHours)
    {
        TotalPersonHours += tempHours;
    }

    public void AddTotalPersonHours(int tempHours)
    {
        TotalPersonHours += tempHours;
    }
}