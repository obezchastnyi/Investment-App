using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models.Experts;

public class Expert
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string SurName { get; set; }

    public string MiddleName { get; set; }

    public double CompetenceCoefficient { get; set; }

    public string WorkPlace { get; set; }

    public string Specialty { get; set; }
}
