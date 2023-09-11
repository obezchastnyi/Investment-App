using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Projects;
using InvestmentApp.V1.DTOs.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/enterprise")]
[ApiController]
public class EnterpriseController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<EnterpriseController> _logger;

    public EnterpriseController(
        ILogger<EnterpriseController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Enterprise>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllEnterprises()
    {
        if (this._context.Enterprise != null)
        {
            return this.Ok(this._context.Enterprise.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Enterprise)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Enterprise), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetEnterprise(Guid id)
    {
        if (this._context.Enterprise != null)
        {
            var enterprise = this._context.Enterprise
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (enterprise != null)
            {
                return this.Ok(enterprise);
            }

            this._logger.LogError($"{nameof(Enterprise)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Enterprise)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateEnterprise([FromBody] EnterpriseDto enterprise)
    {
        this._context.Enterprise.Add(new Enterprise
        {
            Name = enterprise.Name,
            Address = enterprise.Address,
            BankAccount = enterprise.BankAccount,
            TaxNumber = enterprise.TaxNumber,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateEnterprise([FromBody] Enterprise enterprise)
    {
        if (enterprise.Id == default)
        {
            return this.BadRequest();
        }

        var foundEnterprise = this._context.Enterprise.SingleOrDefault(p => p.Id == enterprise.Id);
        if (foundEnterprise == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(enterprise.Name))
        {
            foundEnterprise.Name = enterprise.Name;
        }

        if (!string.IsNullOrEmpty(enterprise.Address))
        {
            foundEnterprise.Address = enterprise.Address;
        }

        if (!string.IsNullOrEmpty(enterprise.BankAccount))
        {
            foundEnterprise.BankAccount = enterprise.BankAccount;
        }

        if (enterprise.TaxNumber != default)
        {
            foundEnterprise.TaxNumber = enterprise.TaxNumber;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateEnterprises([FromBody] IEnumerable<Enterprise> enterprises)
    {
        var results = enterprises.Select(this.UpdateEnterprise).ToList();

        if (results.SingleOrDefault(r => r.GetType() == typeof(BadRequestResult)) != null)
        {
            return this.BadRequest();
        }

        if (results.SingleOrDefault(r => r.GetType() == typeof(NotFoundResult)) != null)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult DeleteEnterprise(Guid id)
    {
        if (this._context.Enterprise != null)
        {
            var enterprise = this._context.Enterprise.SingleOrDefault(p => p.Id == id);
            if (enterprise != null)
            {
                this._context.Enterprise.Remove(enterprise);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Enterprise)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Enterprise)} table is empty.");
        return this.NotFound();
    }
}
