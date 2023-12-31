﻿using System;
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
[Route("v{version:apiVersion}/criteria")]
[ApiController]
public partial class CriteriaController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<CriteriaController> _logger;

    public CriteriaController(
        ILogger<CriteriaController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Criteria>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllCriterias()
    {
        if (this._context.Criteria != null)
        {
            return this.Ok(this._context.Criteria.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Criteria)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Criteria), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetCriteria(Guid id)
    {
        if (this._context.Criteria != null)
        {
            var criteria = this._context.Criteria
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

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateCriteria([FromBody] CriteriaDto criteria)
    {
        this._context.Criteria.Add(new Criteria { Name = criteria.Name });
        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateCriteria([FromBody] Criteria criteria)
    {
        if (criteria.Id == default)
        {
            return this.BadRequest();
        }

        var foundCriteria = this._context.Criteria.FirstOrDefault(p => p.Id == criteria.Id);
        if (foundCriteria == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(criteria.Name))
        {
            foundCriteria.Name = criteria.Name;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateCriterias([FromBody] IEnumerable<Criteria> criterias)
    {
        var results = criterias.Select(this.UpdateCriteria).ToList();

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

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult DeleteCriteria(Guid id)
    {
        if (this._context.Criteria != null)
        {
            var criteria = this._context.Criteria.FirstOrDefault(p => p.Id == id);
            if (criteria != null)
            {
                this._context.Criteria.Remove(criteria);
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
