using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/userRole")]
[ApiController]
public class UserRoleController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<UserRoleController> _logger;

    public UserRoleController(
        ILogger<UserRoleController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<UserRole>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllUserRoles()
    {
        if (this._context.UserRole != null)
        {
            return this.Ok(this._context.UserRole.AsNoTracking());
        }

        this._logger.LogError($"{nameof(UserRole)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserRole), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetUserRole(int id)
    {
        if (this._context.UserRole != null)
        {
            var role = this._context.UserRole.SingleOrDefault(r => r.Id == id);
            if (role != null)
            {
                return this.Ok(role);
            }

            this._logger.LogError($"{nameof(UserRole)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(UserRole)} table is empty.");
        return this.NotFound();
    }
}
