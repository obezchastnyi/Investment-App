using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Industries;
using InvestmentApp.V1.DTOs.Industries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers.Industries;

[V1]
[Route("v{version:apiVersion}/industry")]
[ApiController]
public class IndustryController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<IndustryController> _logger;

    public IndustryController(
        ILogger<IndustryController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Industry>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllIndustries()
    {
        if (this._context.Industry != null)
        {
            return this.Ok(this._context.Industry.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Industry)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Industry), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetIndustry(Guid id)
    {
        if (this._context.Industry != null)
        {
            var industry = this._context.Industry
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (industry != null)
            {
                return this.Ok(industry);
            }

            this._logger.LogError($"{nameof(Industry)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Industry)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateIndustry([FromBody] IndustryDto industry)
    {
        this._context.Industry.Add(new Industry { Name = industry.Name });
        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateIndustry([FromBody] Industry industry)
    {
        if (industry.Id == default)
        {
            return this.BadRequest();
        }

        var foundIndustry = this._context.Industry.SingleOrDefault(p => p.Id == industry.Id);
        if (foundIndustry == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(industry.Name))
        {
            foundIndustry.Name = industry.Name;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateIndustries([FromBody] IEnumerable<Industry> industries)
    {
        var results = industries.Select(this.UpdateIndustry).ToList();

        if (results.SingleOrDefault(r => r.GetType() == typeof(BadRequestResult)) != null)
        {
            return this.BadRequest();
        }

        if (results.SingleOrDefault(r => r.GetType() == typeof(NotFoundResult)) != null)
        {
            return this.NotFound();
        }

        return this.Ok();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult DeleteIndustry(Guid id)
    {
        if (this._context.Industry != null)
        {
            var industry = this._context.Industry.SingleOrDefault(p => p.Id == id);
            if (industry != null)
            {
                this._context.Industry.Remove(industry);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Industry)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Industry)} table is empty.");
        return this.NotFound();
    }
}
