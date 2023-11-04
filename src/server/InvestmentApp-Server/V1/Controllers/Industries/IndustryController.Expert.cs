using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Models.Experts;
using InvestmentApp.V1.DTOs.Industries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers.Industries;

public partial class IndustryController
{
    [HttpGet("expert/all")]
    [ProducesResponseType(typeof(IEnumerable<ExpertIndustry>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllExpertIndustries()
    {
        if (this._context.ExpertIndustry != null)
        {
            return this.Ok(this._context.ExpertIndustry.AsNoTracking());
        }

        this._logger.LogError($"{nameof(ExpertIndustry)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("expert/{id:guid}")]
    [ProducesResponseType(typeof(ExpertIndustry), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetExpertIndustry(Guid id)
    {
        if (this._context.ExpertIndustry != null)
        {
            var industry = this._context.ExpertIndustry
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (industry != null)
            {
                return this.Ok(industry);
            }

            this._logger.LogError($"{nameof(ExpertIndustry)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(ExpertIndustry)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("expert")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateExpertIndustry([FromBody] ExpertIndustryDto industry)
    {
        this._context.ExpertIndustry.Add(new ExpertIndustry
        {
            ExpertId = industry.ExpertId,
            IndustryId = industry.IndustryId,
            Rate = industry.Rate,
        });
        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("expert")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateExpertIndustry([FromBody] ExpertIndustry industry)
    {
        if (industry.Id == default)
        {
            return this.BadRequest();
        }

        var foundIndustry = this._context.ExpertIndustry.FirstOrDefault(p => p.Id == industry.Id);
        if (foundIndustry == null)
        {
            return this.NotFound();
        }

        if (industry.ExpertId != default)
        {
            foundIndustry.ExpertId = industry.ExpertId;
        }

        if (industry.IndustryId != default)
        {
            foundIndustry.IndustryId = industry.IndustryId;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("expert/all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateExpertIndustries([FromBody] IEnumerable<ExpertIndustry> industries)
    {
        var results = industries.Select(this.UpdateExpertIndustry).ToList();

        if (results.FirstOrDefault(r => r.GetType() == typeof(BadRequestResult)) != null)
        {
            return this.BadRequest();
        }

        if (results.FirstOrDefault(r => r.GetType() == typeof(NotFoundResult)) != null)
        {
            return this.NotFound();
        }

        return this.Ok();
    }

    [HttpDelete("expert/{id:guid}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult DeleteExpertIndustry(Guid id)
    {
        if (this._context.ExpertIndustry != null)
        {
            var industry = this._context.ExpertIndustry.FirstOrDefault(p => p.Id == id);
            if (industry != null)
            {
                this._context.ExpertIndustry.Remove(industry);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(ExpertIndustry)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(ExpertIndustry)} table is empty.");
        return this.NotFound();
    }
}
