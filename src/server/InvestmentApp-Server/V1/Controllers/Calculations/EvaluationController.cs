﻿using System;
using InvestmentApp.Attributes;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InvestmentApp.V1.Controllers.Calculations;

[V1]
[Route("v{version:apiVersion}/evaluation")]
[ApiController]
public class EvaluationController : BaseController
{
    private readonly InvestmentAppDbContext _context;
    private readonly ILogger<EvaluationController> _logger;

    public EvaluationController(
        ILogger<EvaluationController> logger, InvestmentAppDbContext context, IPasswordManager passwordManager)
        : base(context, passwordManager)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    public IActionResult Evaluate()
    {
        var rand = new Random();
        return this.Ok(rand.Next(12345678));
    }
}
