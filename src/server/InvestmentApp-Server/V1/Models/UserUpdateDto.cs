using System;

namespace InvestmentApp.V1.Models;

public class UserUpdateDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public int? RoleId { get; set; }
}
