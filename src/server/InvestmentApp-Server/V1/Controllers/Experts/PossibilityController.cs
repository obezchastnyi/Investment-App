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
[Route("v{version:apiVersion}/possibility")]
[ApiController]
public class PossibilityController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<PossibilityController> _logger;

    public PossibilityController(
        ILogger<PossibilityController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Possibility>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllPossibilities()
    {
        if (this._context.Possibility != null)
        {
            var result = this._context.Possibility
                .AsNoTracking()
                .ToList();
            return this.Ok(result);
        }

        this._logger.LogError($"{nameof(Possibility)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Possibility), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetPossibility(Guid id)
    {
        if (this._context.Possibility != null)
        {
            var possibility = this._context.Possibility
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (possibility != null)
            {
                return this.Ok(possibility);
            }

            this._logger.LogError($"{nameof(Possibility)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Possibility)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreatePossibility([FromBody] PossibilityDto possibility)
    {
        this._context.Possibility.Add(new Possibility
        {
            Rate = possibility.Rate,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdatePossibility([FromBody] Possibility possibility)
    {
        if (possibility.Id == default)
        {
            return this.BadRequest();
        }

        var foundPossibility = this._context.Possibility.FirstOrDefault(p => p.Id == possibility.Id);
        if (foundPossibility == null)
        {
            return this.NotFound();
        }

        if (possibility.Rate != default)
        {
            foundPossibility.Rate = possibility.Rate;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdatePossibilities([FromBody] IEnumerable<Possibility> possibilities)
    {
        var results = possibilities.Select(this.UpdatePossibility);

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
    public IActionResult DeletePossibility(Guid id)
    {
        if (this._context.Possibility != null)
        {
            var possibility = this._context.Possibility.FirstOrDefault(p => p.Id == id);
            if (possibility != null)
            {
                this._context.Possibility.Remove(possibility);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Possibility)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Possibility)} table is empty.");
        return this.NotFound();
    }
}
