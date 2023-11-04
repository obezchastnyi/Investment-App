using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Models.Industries;
using InvestmentApp.V1.DTOs.Industries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers.Industries;

public partial class CriteriaController
{
    [HttpGet("all/industry")]
    [ProducesResponseType(typeof(IEnumerable<IndustryCriteria>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllIndustryCriterias()
    {
        if (this._context.IndustryCriteria != null)
        {
            return this.Ok(this._context.IndustryCriteria
                .Include(c => c.Criteria)
                .Include(c => c.Industry)
                .AsNoTracking());
        }

        this._logger.LogError($"{nameof(IndustryCriteria)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("industry/{id:guid}")]
    [ProducesResponseType(typeof(IndustryCriteria), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetIndustryCriteria(Guid id)
    {
        if (this._context.IndustryCriteria != null)
        {
            var criteria = this._context.IndustryCriteria
                .Include(c => c.Criteria)
                .Include(c => c.Industry)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (criteria != null)
            {
                return this.Ok(criteria);
            }

            this._logger.LogError($"{nameof(Criteria)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Criteria)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("industry")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateIndustryCriteria([FromBody] IndustryCriteriaDto criteria)
    {
        this._context.IndustryCriteria.Add(new IndustryCriteria
        {
            IndustryId = criteria.IndustryId,
            CriteriaId = criteria.CriteriaId,
            IndustrySpecificWeight = criteria.IndustrySpecificWeight
        });
        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("industry")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateIndustryCriteria([FromBody] IndustryCriteria criteria)
    {
        if (criteria.Id == default)
        {
            return this.BadRequest();
        }

        var foundCriteria = this._context.IndustryCriteria.FirstOrDefault(p => p.Id == criteria.Id);
        if (foundCriteria == null)
        {
            return this.NotFound();
        }

        if (criteria.IndustryId != default)
        {
            foundCriteria.IndustryId = criteria.IndustryId;
        }

        if (criteria.CriteriaId != default)
        {
            foundCriteria.CriteriaId = criteria.CriteriaId;
        }

        if (criteria.IndustrySpecificWeight != default)
        {
            foundCriteria.IndustrySpecificWeight = criteria.IndustrySpecificWeight;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update/industry")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateIndustryCriterias([FromBody] IEnumerable<IndustryCriteria> criterias)
    {
        var results = criterias.Select(this.UpdateIndustryCriteria).ToList();

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

    [HttpDelete("industry/{id:guid}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult DeleteIndustryCriteria(Guid id)
    {
        if (this._context.IndustryCriteria != null)
        {
            var criteria = this._context.IndustryCriteria.FirstOrDefault(p => p.Id == id);
            if (criteria != null)
            {
                this._context.IndustryCriteria.Remove(criteria);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Criteria)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Criteria)} table is empty.");
        return this.NotFound();
    }
}
