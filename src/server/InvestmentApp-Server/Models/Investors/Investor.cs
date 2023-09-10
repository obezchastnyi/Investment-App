using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models.Investors;

public class Investor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string SurName { get; set; }
}
