using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models;

public class Enterprise
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string BankAccount { get; set; }

    public long TaxNumber { get; set; }
}
