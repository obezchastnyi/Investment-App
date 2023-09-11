using System;

namespace InvestmentApp.V1.DTOs.Industries;

public class IndustryCriteriaDto
{
    public Guid IndustryId { get; set; }

    public Guid CriteriaId { get; set; }

    public double IndustrySpecificWeight { get; set; }
}
