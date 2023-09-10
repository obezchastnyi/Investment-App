using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models.Experts;

public class ExpertProject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Expert Expert { get; set; }

    public Guid ExpertId { get; set; }

    public Project Project { get; set; }

    public Guid ProjectId { get; set; }

    public Period Period { get; set; }

    public Guid PeriodId { get; set; }

    public Possibility Possibility { get; set; }

    public Guid PossibilityId { get; set; }

    public double CashFlowRate { get; set; }
}
