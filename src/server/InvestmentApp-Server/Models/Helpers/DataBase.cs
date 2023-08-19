namespace InvestmentApp.Models.Helpers;

public class DataBase
{
    public DataBase(string name, string dbSystem, string version, string status)
    {
        this.Name = name;
        this.DbSystem = dbSystem;
        this.Version = version;
        this.Status = status;
    }

    public string Name { get; }

    public string DbSystem { get; }

    public string Version { get; }

    public string Status { get; }
}
