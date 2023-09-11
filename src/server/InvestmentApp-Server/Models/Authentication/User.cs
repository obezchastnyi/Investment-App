using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentApp.Models.Authentication;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public virtual string UserName { get; set; }

    public string PasswordHash { get; set; }

    public UserRole UserRole { get; set; }

    public int UserRoleId { get; set; }
}
