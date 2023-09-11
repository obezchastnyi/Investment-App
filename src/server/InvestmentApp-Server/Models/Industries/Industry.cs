using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models.Industries;

public class Industry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
