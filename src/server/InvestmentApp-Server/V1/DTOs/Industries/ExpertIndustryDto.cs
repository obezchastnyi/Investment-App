using System;

namespace InvestmentApp.V1.DTOs.Industries;

public class ExpertIndustryDto
{
    public Guid ExpertId { get; set; }

    public Guid IndustryId { get; set; }

    public double Rate { get; set; }
}
