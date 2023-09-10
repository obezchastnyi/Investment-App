using System;
using System.Linq;
using System.Text.Json.Serialization;
using InvestmentApp.DB;
using InvestmentApp.Interfaces;
using InvestmentApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InvestmentApp;

public class Startup
{
    private IConfiguration Configuration { get; }

    private readonly bool _enableSwagger;

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        this._enableSwagger = (this.Configuration["EnableSwagger"]?
            .Equals("true", StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        AspNetInitService(services);
        AddApiVersioning(services);
        services.AddCors();

        this.AddDbServices(services);

        services.AddTransient<IPasswordManager, PasswordManager>();

        if (this._enableSwagger)
        {
            services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>()
                .AddSwaggerGen();
        }
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IApiVersionDescriptionProvider apiDescriptionProvider)
    {
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseRouting();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        if (this._enableSwagger)
        {
            ConfigureSwagger(app, apiDescriptionProvider);
        }

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });

        app.UseResponseCaching();
    }

    private static void AspNetInitService(IServiceCollection services)
    {
        services.AddMvcCore()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddDataAnnotations()
            .AddApiExplorer()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
                options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(context.ModelState);
            });
    }

    private void AddDbServices(IServiceCollection services)
    {
        var pgConnectionString = this.Configuration.GetConnectionString("PostgresConnection");
        var pgVersionString = this.Configuration.GetConnectionString("PostgresVersion");
        services.AddDbServices<InvestmentAppDbContext>(pgConnectionString, pgVersionString);
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    private static void ConfigureSwagger(IApplicationBuilder app, IApiVersionDescriptionProvider apiDescriptionProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // build a swagger endpoint for each discovered API version
            foreach (var description in apiDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }

            app.Map("/swagger/versions_info", builder => builder.Run(async context =>
                await context.Response.WriteAsync(
                    string.Join(Environment.NewLine, options.ConfigObject.Urls.Select(
                        descriptor => $"{descriptor.Name} {descriptor.Url}")))));
        });
    }
}
