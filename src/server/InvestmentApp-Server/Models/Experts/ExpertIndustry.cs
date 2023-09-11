using System;
using System.ComponentModel.DataAnnotations.Schema;
using InvestmentApp.Models.Industries;

namespace InvestmentApp.Models.Experts;

public class ExpertIndustry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Industry Industry { get; set; }

    public Guid IndustryId { get; set; }

    public Expert Expert { get; set; }

    public Guid ExpertId { get; set; }

    public double Rate { get; set; }
}

