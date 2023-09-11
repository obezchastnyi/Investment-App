using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Investors;
using InvestmentApp.V1.DTOs.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/investor")]
[ApiController]
public class InvestorController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<InvestorController> _logger;

    public InvestorController(
        ILogger<InvestorController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Investor>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllInvestors()
    {
        if (this._context.Investor != null)
        {
            return this.Ok(this._context.Investor.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Investor)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Investor), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetInvestor(Guid id)
    {
        if (this._context.Investor != null)
        {
            var investor = this._context.Investor
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (investor != null)
            {
                return this.Ok(investor);
            }

            this._logger.LogError($"{nameof(Investor)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Investor)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateInvestor([FromBody] InvestorDto investor)
    {
        this._context.Investor.Add(new Investor
        {
            Name = investor.Name,
            SurName = investor.SurName,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateInvestor([FromBody] Investor investor)
    {
        if (investor.Id == default)
        {
            return this.BadRequest();
        }

        var foundInvestor = this._context.Investor.SingleOrDefault(p => p.Id == investor.Id);
        if (foundInvestor == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(investor.Name))
        {
            foundInvestor.Name = investor.Name;
        }

        if (!string.IsNullOrEmpty(investor.SurName))
        {
            foundInvestor.SurName = investor.SurName;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateInvestors([FromBody] IEnumerable<Investor> investors)
    {
        var results = investors.Select(this.UpdateInvestor).ToList();

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
    public IActionResult DeleteInvestor(Guid id)
    {
        if (this._context.Investor != null)
        {
            var investor = this._context.Investor.SingleOrDefault(p => p.Id == id);
            if (investor != null)
            {
                this._context.Investor.Remove(investor);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Investor)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Investor)} table is empty.");
        return this.NotFound();
    }
}
