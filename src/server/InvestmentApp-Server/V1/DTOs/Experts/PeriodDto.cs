using System;

namespace InvestmentApp.V1.DTOs.Experts;

public class PeriodDto
{
    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }  // null -> indefinitely

    public double DiscountRate { get; set; }

    public double RiskFreeDiscountRate { get; set; }
}
