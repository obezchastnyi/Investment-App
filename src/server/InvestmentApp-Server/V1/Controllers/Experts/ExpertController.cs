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
[Route("v{version:apiVersion}/expert")]
[ApiController]
public class ExpertController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<ExpertController> _logger;

    public ExpertController(
        ILogger<ExpertController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Expert>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllExperts()
    {
        if (this._context.Expert != null)
        {
            return this.Ok(this._context.Expert.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Expert)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Expert), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetExpert(Guid id)
    {
        if (this._context.Expert != null)
        {
            var expert = this._context.Expert
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (expert != null)
            {
                return this.Ok(expert);
            }

            this._logger.LogError($"{nameof(Expert)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Expert)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateExpert([FromBody] ExpertDto expert)
    {
        this._context.Expert.Add(new Expert
        {
            Name = expert.Name,
            SurName = expert.SurName,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateExpert([FromBody] Expert expert)
    {
        if (expert.Id == default)
        {
            return this.BadRequest();
        }

        var foundExpert = this._context.Expert.FirstOrDefault(p => p.Id == expert.Id);
        if (foundExpert == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(expert.Name))
        {
            foundExpert.Name = expert.Name;
        }

        if (!string.IsNullOrEmpty(expert.SurName))
        {
            foundExpert.SurName = expert.SurName;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateExperts([FromBody] IEnumerable<Expert> experts)
    {
        var results = experts.Select(this.UpdateExpert).ToList();

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
    public IActionResult DeleteExpert(Guid id)
    {
        if (this._context.Expert != null)
        {
            var expert = this._context.Expert.FirstOrDefault(p => p.Id == id);
            if (expert != null)
            {
                this._context.Expert.Remove(expert);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Expert)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Expert)} table is empty.");
        return this.NotFound();
    }
}
