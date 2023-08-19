using System.Collections.Generic;

namespace InvestmentApp.Models.Helpers;

public class HealthCheck
{
    public List<DataBase> Databases { get; } = new();
}
