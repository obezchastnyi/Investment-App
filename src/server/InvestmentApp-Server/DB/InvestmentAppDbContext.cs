using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext : DbContext
{
    public InvestmentAppDbContext(DbContextOptions<InvestmentAppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildProjectModels(modelBuilder);
        BuildIndustryModels(modelBuilder);
        BuildExpertModel(modelBuilder);
        BuildInvestorModel(modelBuilder);
        BuildAuthenticationModel(modelBuilder);

        InitData(modelBuilder);
    }

    private static void InitData(ModelBuilder modelBuilder)
    {
        var criterias = InitCriteriaData(modelBuilder);
        var industries = InitIndustryData(modelBuilder);
        InitIndustryCriteriaData(modelBuilder, industries, criterias);

        var enterprises = InitEnterpriseData(modelBuilder);
        var projects = InitProjectsData(modelBuilder, enterprises);

        var experts = InitExpertsData(modelBuilder);
        InitExpertIndustryData(modelBuilder, experts, industries);

        var investors = InitInvestorsData(modelBuilder);
        InitInvestorProjectData(modelBuilder, investors, projects);

        var possibilities = InitPossibilityData(modelBuilder);
        var periods = InitPeriodsData(modelBuilder);
        InitExpertProjectsData(modelBuilder, projects, experts, periods, possibilities);

        var roles = InitUserRolesData(modelBuilder);
        InitUsersData(modelBuilder, experts, investors, roles);
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add InvestmentApp_Migration
 * dotnet ef database update
 */
