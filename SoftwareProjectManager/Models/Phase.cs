namespace src.Models;

public class Phase
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double WeeklyPersonHours { get; set; }
    public double TotalPersonHours { get; set; }

    public Phase(int ID, string name, int weeklyHours, int totalHours)
    {
        this.ID = ID;
        Name = name;
        WeeklyPersonHours = weeklyHours;
        TotalPersonHours = totalHours;
    }

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