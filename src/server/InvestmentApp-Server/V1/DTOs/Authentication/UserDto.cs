using System;

namespace InvestmentApp.V1.DTOs.Authentication;

public class UserDto : UserAuthenticationDto
{
    public Guid? Id { get; set; }

    public int? RoleId { get; set; }
}
