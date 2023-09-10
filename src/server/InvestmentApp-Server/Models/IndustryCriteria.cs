using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models;

public class IndustryCriteria
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Industry Industry { get; set; }

    public Guid IndustryId { get; set; }

    public Criteria Criteria { get; set; }

    public Guid CriteriaId { get; set; }

    public double IndustrySpecificWeight { get; set; }
}
