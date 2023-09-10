using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models;

public class Criteria
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
