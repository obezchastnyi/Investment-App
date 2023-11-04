using System;

namespace InvestmentApp.V1.DTOs.Investors;

public class InvestorProjectDto
{
    public Guid InvestorId { get; set; }

    public Guid ProjectId { get; set; }

    public double MinIncomeRate { get; set; }

    public double MaxRiskRate { get; set; }
}
