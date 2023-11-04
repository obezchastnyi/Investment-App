using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Experts;
using InvestmentApp.V1.DTOs.Experts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/period")]
[ApiController]
public class PeriodController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<PeriodController> _logger;

    public PeriodController(
        ILogger<PeriodController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Period>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllPeriods()
    {
        if (this._context.Period != null)
        {
            return this.Ok(this._context.Period.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Period)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Period), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetPeriod(Guid id)
    {
        if (this._context.Period != null)
        {
            var period = this._context.Period
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (period != null)
            {
                return this.Ok(period);
            }

            this._logger.LogError($"{nameof(Period)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Period)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreatePeriod([FromBody] PeriodDto period)
    {
        this._context.Period.Add(new Period
        {
            StartDate = period.StartDate,
            EndDate = period.EndDate,
            DiscountRate = period.DiscountRate,
            RiskFreeDiscountRate = period.RiskFreeDiscountRate,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdatePeriod([FromBody] Period period)
    {
        if (period.Id == default)
        {
            return this.BadRequest();
        }

        var foundPeriod = this._context.Period.FirstOrDefault(p => p.Id == period.Id);
        if (foundPeriod == null)
        {
            return this.NotFound();
        }

        if (period.StartDate != default)
        {
            foundPeriod.StartDate = period.StartDate;
        }

        foundPeriod.EndDate = period.EndDate;

        if (period.DiscountRate != default)
        {
            foundPeriod.DiscountRate = period.DiscountRate;
        }

        if (period.RiskFreeDiscountRate != default)
        {
            foundPeriod.RiskFreeDiscountRate = period.RiskFreeDiscountRate;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdatePeriods([FromBody] IEnumerable<Period> periods)
    {
        var results = periods.Select(this.UpdatePeriod).ToList();

        if (results.FirstOrDefault(r => r.GetType() == typeof(BadRequestResult)) != null)
        {
            return this.BadRequest();
        }

        if (results.FirstOrDefault(r => r.GetType() == typeof(NotFoundResult)) != null)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult DeletePeriod(Guid id)
    {
        if (this._context.Period != null)
        {
            var period = this._context.Period.FirstOrDefault(p => p.Id == id);
            if (period != null)
            {
                this._context.Period.Remove(period);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Period)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Period)} table is empty.");
        return this.NotFound();
    }
}
