using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using InvestmentApp.Models.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using static InvestmentApp.Constants;

namespace InvestmentApp.Controllers;

[Controller]
[Route("/[controller]/[action]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpGet]
    [Route("")]
    [Route("/")]
    public IActionResult Home()
    {
        var assembly = Assembly.GetEntryAssembly();
        var info = $"{assembly?.GetName().Name}: {FileVersionInfo.GetVersionInfo(assembly?.Location ?? string.Empty).FileVersion}";
        return this.Ok(info);
    }

    [HttpGet]
    [Route("/health")]
    public IActionResult Health()
    {
        var status = UnhealthyStatus;
        using (var connection = new NpgsqlConnection(this._configuration.GetConnectionString("PostgresConnection")))
        {
            try
            {
                connection.Open();
                if (connection.State.ToString() == "Open")
                {
                    status = HealthyStatus;
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Unable to open connection to DB: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        var healthCheck = new HealthCheck();
        healthCheck.Databases.Add(new DataBase(
            InvestmentAppDB,
            PostgreSql,
            this._configuration.GetConnectionString("PostgresVersion"),
            status));

        this._logger.LogInformation($"{InvestmentAppDB} status: {status}");
        return this.Json(healthCheck);
    }

    [Route("/Home/Error")]
    public IActionResult Error()
    {
        var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

        var message = $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
        this._logger.LogError(message);

        return new ObjectResult(new
            { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier })
            { StatusCode = (int)HttpStatusCode.BadRequest };
    }
}
