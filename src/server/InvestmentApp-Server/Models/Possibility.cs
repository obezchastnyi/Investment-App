using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models;

public class Possibility
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public double Rate { get; set; }
}

