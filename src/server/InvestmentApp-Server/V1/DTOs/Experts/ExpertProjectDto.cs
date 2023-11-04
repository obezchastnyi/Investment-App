using System;

namespace InvestmentApp.V1.DTOs.Experts;

public class ExpertProjectDto
{
    public Guid ExpertId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid PeriodId { get; set; }

    public Guid PossibilityId { get; set; }
}
