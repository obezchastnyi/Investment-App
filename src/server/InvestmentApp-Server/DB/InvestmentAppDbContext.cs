using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext : DbContext
{
    public InvestmentAppDbContext(DbContextOptions<InvestmentAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildBaseModels(modelBuilder);
        BuildExpertModel(modelBuilder);
        BuildInvestorModel(modelBuilder);
        BuildAuthenticationModel(modelBuilder);

        InitData(modelBuilder);
    }

    private static void InitData(ModelBuilder modelBuilder)
    {
        var criteria = InitCriteriaData(modelBuilder);
        var industry = InitIndustryData(modelBuilder);
        InitIndustryCriteriaData(modelBuilder, industry, criteria);

        var enterprises = InitEnterpriseData(modelBuilder);
        var projects = InitProjectsData(modelBuilder, enterprises);

        var expert = InitExpertsData(modelBuilder);
        InitExpertIndustryData(modelBuilder, expert, industry);

        var investor = InitInvestorsData(modelBuilder);
        InitInvestorProjectData(modelBuilder, investor, projects[0]);

        var possibility = InitPossibilityData(modelBuilder);
        var period = InitPeriodsData(modelBuilder);
        InitExpertProjectsData(modelBuilder, projects, expert, period, possibility);

        var roles = InitUserRolesData(modelBuilder);
        InitUsersData(modelBuilder, expert, investor, roles);
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add InvestmentApp_Migration
 * dotnet ef database update
 */
