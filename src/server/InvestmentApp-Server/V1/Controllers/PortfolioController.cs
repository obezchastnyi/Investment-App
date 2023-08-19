using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models;
using InvestmentApp.V1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/portfolio")]
[ApiController]
public class PortfolioController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<PortfolioController> _logger;

    public PortfolioController(
        ILogger<PortfolioController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Portfolio>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllPortfolios()
    {
        if (this._context.Portfolio != null)
        {
            return this.Ok(this._context.Portfolio.AsNoTracking());
        }

        this._logger.LogError($"{nameof(Portfolio)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetPortfolio(Guid id)
    {
        if (this._context.Portfolio != null)
        {
            var portfolio = this._context.Portfolio.SingleOrDefault(p => p.Id == id);
            if (portfolio != null)
            {
                return this.Ok(portfolio);
            }

            this._logger.LogError($"{nameof(Portfolio)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Portfolio)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreatePortfolio([FromBody] NewPortfolioDto portfolio)
    {
        this._context.Portfolio.Add(new Portfolio
        {
            Name = portfolio.Name,
            Sum = portfolio.Sum,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdatePortfolio([FromBody] PortfolioDto portfolioDto)
    {
        if (!portfolioDto.Id.HasValue)
        {
            return this.BadRequest();
        }

        var portfolio = this._context.Portfolio.SingleOrDefault(p => p.Id == portfolioDto.Id);
        if (portfolio == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(portfolioDto.Name))
        {
            portfolio.Name = portfolioDto.Name;
        }

        if (portfolioDto.Sum != default)
        {
            portfolio.Sum = portfolioDto.Sum;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdatePortfolios([FromBody] IEnumerable<PortfolioDto> portfoliosDto)
    {
        var results = portfoliosDto.Select(this.UpdatePortfolio).ToList();

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
    public IActionResult DeletePortfolio(Guid id)
    {
        if (this._context.Portfolio != null)
        {
            var portfolio = this._context.Portfolio.SingleOrDefault(p => p.Id == id);
            if (portfolio != null)
            {
                this._context.Portfolio.Remove(portfolio);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Portfolio)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Portfolio)} table is empty.");
        return this.NotFound();
    }
}
