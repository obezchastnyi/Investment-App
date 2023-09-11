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
[Route("v{version:apiVersion}/investor/project")]
[ApiController]
public class InvestorProjectController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<InvestorProjectController> _logger;

    public InvestorProjectController(
        ILogger<InvestorProjectController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<InvestorProject>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllInvestorProjects()
    {
        if (this._context.InvestorProject != null)
        {
            return this.Ok(this._context.InvestorProject
                .Include(e => e.Investor)
                .Include(e => e.Project)
                .AsNoTracking());
        }

        this._logger.LogError($"{nameof(InvestorProject)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(InvestorProject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetInvestorProject(Guid id)
    {
        if (this._context.InvestorProject != null)
        {
            var investor = this._context.InvestorProject
                .Include(e => e.Investor)
                .Include(e => e.Project)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (investor != null)
            {
                return this.Ok(investor);
            }

            this._logger.LogError($"{nameof(InvestorProject)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(InvestorProject)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateInvestorProject([FromBody] InvestorProjectDto investorProject)
    {
        this._context.InvestorProject.Add(new InvestorProject
        {
            InvestorId = investorProject.InvestorId,
            ProjectId = investorProject.ProjectId,
            MinIncomeRate = investorProject.MinIncomeRate,
            MaxRiskRate = investorProject.MaxRiskRate,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateInvestorProject([FromBody] InvestorProject investorProject)
    {
        if (investorProject.Id == default)
        {
            return this.BadRequest();
        }

        var foundProject = this._context.InvestorProject.SingleOrDefault(p => p.Id == investorProject.Id);
        if (foundProject == null)
        {
            return this.NotFound();
        }

        if (investorProject.InvestorId != default)
        {
            foundProject.InvestorId = investorProject.InvestorId;
        }

        if (investorProject.ProjectId != default)
        {
            foundProject.ProjectId = investorProject.ProjectId;
        }

        if (investorProject.MinIncomeRate != default)
        {
            foundProject.MinIncomeRate = investorProject.MinIncomeRate;
        }

        if (investorProject.MaxRiskRate != default)
        {
            foundProject.MaxRiskRate = investorProject.MaxRiskRate;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateInvestorProjects([FromBody] IEnumerable<InvestorProject> investorProjects)
    {
        var results = investorProjects.Select(this.UpdateInvestorProject).ToList();

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
    public IActionResult DeleteInvestorProject(Guid id)
    {
        if (this._context.InvestorProject != null)
        {
            var investorProject = this._context.InvestorProject.SingleOrDefault(p => p.Id == id);
            if (investorProject != null)
            {
                this._context.InvestorProject.Remove(investorProject);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(InvestorProject)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(InvestorProject)} table is empty.");
        return this.NotFound();
    }
}
