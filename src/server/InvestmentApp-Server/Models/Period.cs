using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models;

public class Period
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }  // null -> indefinitely

    public double DiscountRate { get; set; }

    public double RiskFreeDiscountRate { get; set; }
}
