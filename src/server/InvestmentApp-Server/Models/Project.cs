using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models;

public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public double StartingInvestmentSum { get; set; }

    public Enterprise Enterprise { get; set; }

    public Guid EnterpriseId { get; set; }
}
