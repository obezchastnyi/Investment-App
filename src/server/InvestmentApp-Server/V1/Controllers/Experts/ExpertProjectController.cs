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
[Route("v{version:apiVersion}/expert/project")]
[ApiController]
public class ExpertProjectController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<ExpertProjectController> _logger;

    public ExpertProjectController(
        ILogger<ExpertProjectController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<ExpertProject>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllExpertProjects()
    {
        if (this._context.ExpertProject != null)
        {
            return this.Ok(this._context.ExpertProject
                .Include(e => e.Expert)
                .Include(e => e.Project)
                .AsNoTracking());
        }

        this._logger.LogError($"{nameof(ExpertProject)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ExpertProject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetExpertProject(Guid id)
    {
        if (this._context.ExpertProject != null)
        {
            var expert = this._context.ExpertProject
                .Include(e => e.Expert)
                .Include(e => e.Project)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            if (expert != null)
            {
                return this.Ok(expert);
            }

            this._logger.LogError($"{nameof(ExpertProject)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(ExpertProject)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateExpertProject([FromBody] ExpertProjectDto expertProject)
    {
        this._context.ExpertProject.Add(new ExpertProject
        {
            ExpertId = expertProject.ExpertId,
            ProjectId = expertProject.ProjectId,
            PeriodId = expertProject.PeriodId,
            PossibilityId = expertProject.PossibilityId,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateExpertProject([FromBody] ExpertProject expertProject)
    {
        if (expertProject.Id == default)
        {
            return this.BadRequest();
        }

        var foundProject = this._context.ExpertProject.FirstOrDefault(p => p.Id == expertProject.Id);
        if (foundProject == null)
        {
            return this.NotFound();
        }

        if (expertProject.ExpertId != default)
        {
            foundProject.ExpertId = expertProject.ExpertId;
        }

        if (expertProject.ProjectId != default)
        {
            foundProject.ProjectId = expertProject.ProjectId;
        }

        if (expertProject.PeriodId != default)
        {
            foundProject.PeriodId = expertProject.PeriodId;
        }

        if (expertProject.PossibilityId != default)
        {
            foundProject.PossibilityId = expertProject.PossibilityId;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateExpertProjects([FromBody] IEnumerable<ExpertProject> expertProjects)
    {
        var results = expertProjects.Select(this.UpdateExpertProject).ToList();

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
    public IActionResult DeleteExpertProject(Guid id)
    {
        if (this._context.ExpertProject != null)
        {
            var expertProject = this._context.ExpertProject.FirstOrDefault(p => p.Id == id);
            if (expertProject != null)
            {
                this._context.ExpertProject.Remove(expertProject);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(expertProject)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(ExpertProject)} table is empty.");
        return this.NotFound();
    }
}
