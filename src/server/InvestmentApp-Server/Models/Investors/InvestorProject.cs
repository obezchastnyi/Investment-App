using System;
using System.ComponentModel.DataAnnotations.Schema;
using InvestmentApp.Models.Projects;

namespace InvestmentApp.Models.Investors;

public class InvestorProject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Investor Investor { get; set; }

    public Guid InvestorId { get; set; }

    public Project Project { get; set; }

    public Guid ProjectId { get; set; }

    public double MinIncomeRate { get; set; }

    public double MaxRiskRate { get; set; }
}
