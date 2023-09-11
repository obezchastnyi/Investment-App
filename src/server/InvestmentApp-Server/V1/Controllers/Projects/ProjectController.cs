using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Models.Projects;
using InvestmentApp.V1.DTOs.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers;

[V1]
[Route("v{version:apiVersion}/project")]
[ApiController]
public class ProjectController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(
        ILogger<ProjectController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<Project>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetAllProjects()
    {
        if (this._context.Project != null)
        {
            return this.Ok(this._context.Project
                .Include(e => e.Enterprise)
                .AsNoTracking());
        }

        this._logger.LogError($"{nameof(Project)} table is empty.");
        return this.NotFound();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public IActionResult GetProject(Guid id)
    {
        if (this._context.Project != null)
        {
            var project = this._context.Project
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            if (project != null)
            {
                return this.Ok(project);
            }

            this._logger.LogError($"{nameof(Project)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Project)} table is empty.");
        return this.NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult CreateProject([FromBody] ProjectDto project)
    {
        this._context.Project.Add(new Project
        {
            Name = project.Name,
            StartingInvestmentSum = project.StartingInvestmentSum,
        });

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateProject([FromBody] Project project)
    {
        if (project.Id == default)
        {
            return this.BadRequest();
        }

        var foundProject = this._context.Project.SingleOrDefault(p => p.Id == project.Id);
        if (foundProject == null)
        {
            return this.NotFound();
        }

        if (!string.IsNullOrEmpty(project.Name))
        {
            foundProject.Name = project.Name;
        }

        if (project.StartingInvestmentSum != default)
        {
            foundProject.StartingInvestmentSum = project.StartingInvestmentSum;
        }

        this._context.SaveChanges();
        return this.Ok();
    }

    [HttpPut("all-update")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
    public IActionResult UpdateProjects([FromBody] IEnumerable<Project> projects)
    {
        var results = projects.Select(this.UpdateProject).ToList();

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
    public IActionResult DeleteProject(Guid id)
    {
        if (this._context.Project != null)
        {
            var project = this._context.Project.SingleOrDefault(p => p.Id == id);
            if (project != null)
            {
                this._context.Project.Remove(project);
                this._context.SaveChanges();
                return this.Ok();
            }

            this._logger.LogError($"{nameof(Project)} '{id}' has not been found.");
            return this.NotFound();
        }

        this._logger.LogError($"{nameof(Project)} table is empty.");
        return this.NotFound();
    }
}
