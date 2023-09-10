using System;
using System.Collections.Generic;
using InvestmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<Possibility> Possibility { get; set; }

    public virtual DbSet<Period> Period { get; set; }

    public virtual DbSet<Industry> Industry { get; set; }

    public virtual DbSet<Criteria> Criteria { get; set; }

    public virtual DbSet<IndustryCriteria> IndustryCriteria { get; set; }

    public virtual DbSet<Enterprise> Enterprise { get; set; }

    public virtual DbSet<Project> Project { get; set; }

    private static void BuildBaseModels(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Possibility>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Rate).IsRequired();
            entity.HasIndex(e => e.Rate);
        });

        modelBuilder.Entity<Period>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate);

            entity.Property(e => e.DiscountRate).IsRequired();
            entity.HasIndex(e => e.DiscountRate);

            entity.Property(e => e.RiskFreeDiscountRate).IsRequired();
            entity.HasIndex(e => e.RiskFreeDiscountRate);
        });

        modelBuilder.Entity<Criteria>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<IndustryCriteria>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.IndustrySpecificWeight).IsRequired();
            entity.HasIndex(e => e.IndustrySpecificWeight).IsUnique();

            entity.HasOne(e => e.Industry)
                .WithMany()
                .HasForeignKey(e => e.IndustryId)
                .IsRequired();

            entity.HasOne(e => e.Criteria)
                .WithMany()
                .HasForeignKey(e => e.CriteriaId)
                .IsRequired();
        });

        modelBuilder.Entity<Enterprise>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.BankAccount).IsRequired();
            entity.Property(e => e.TaxNumber).IsRequired();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.StartingInvestmentSum).IsRequired();

            entity.HasOne(e => e.Enterprise)
                .WithMany()
                .HasForeignKey(e => e.EnterpriseId)
                .IsRequired();
        });
    }

    private static Possibility InitPossibilityData(ModelBuilder modelBuilder)
    {
        var possibility = new Possibility { Id = Guid.NewGuid(), Rate = 16, };

        modelBuilder.Entity<Possibility>().HasData(possibility);
        return possibility;
    }

    private static Period InitPeriodsData(ModelBuilder modelBuilder)
    {
        var period = new Period
        {
            Id = Guid.NewGuid(),
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2024, 1, 1),
            DiscountRate = 10,
            RiskFreeDiscountRate = 5,
        };

        modelBuilder.Entity<Period>().HasData(period);
        return period;
    }

    private static Criteria InitCriteriaData(ModelBuilder modelBuilder)
    {
        var criteria = new Criteria { Id = Guid.NewGuid(), Name = "Profitability", };

        modelBuilder.Entity<Criteria>().HasData(criteria);
        return criteria;
    }

    private static Industry InitIndustryData(ModelBuilder modelBuilder)
    {
        var industry = new Industry { Id = Guid.NewGuid(), Name = "Metallurgy", };

        modelBuilder.Entity<Industry>().HasData(industry);
        return industry;
    }

    private static void InitIndustryCriteriaData(ModelBuilder modelBuilder, Industry industry, Criteria criteria)
    {
        modelBuilder.Entity<IndustryCriteria>().HasData(
            new IndustryCriteria
            {
                Id = Guid.NewGuid(),
                IndustryId = industry.Id,
                CriteriaId = criteria.Id,
                IndustrySpecificWeight = 7,
            });
    }

    private static IList<Enterprise> InitEnterpriseData(ModelBuilder modelBuilder)
    {
        var enterprises = new[]
        {
            new Enterprise
            {
                Id = Guid.NewGuid(),
                Name = "T-Shirts Brand",
                Address = "Ukraine, Kharkiv, Kharkiv Region",
                BankAccount = "UA123456789",
                TaxNumber = 987321654,
            },
            new Enterprise
            {
                Id = Guid.NewGuid(),
                Name = "Soccer Club",
                Address = "Ukraine, Kyiv, Kyiv Region",
                BankAccount = "UA987654321",
                TaxNumber = 321987654,
            },
            new Enterprise
            {
                Id = Guid.NewGuid(),
                Name = "IT Company",
                Address = "Ukraine, Dnipro, Dnipro Region",
                BankAccount = "UA123789456",
                TaxNumber = 123987456,
            }
        };

        modelBuilder.Entity<Enterprise>().HasData(enterprises);
        return enterprises;
    }

    private static IList<Project> InitProjectsData(ModelBuilder modelBuilder, IList<Enterprise> enterprises)
    {
        var projects = new[]
        {
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "T-Shirts Collection",
                StartingInvestmentSum = 1000,
                EnterpriseId = enterprises[0].Id,
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Soccer Club Tournament",
                StartingInvestmentSum = 100000000,
                EnterpriseId = enterprises[1].Id,
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "IT Company Hiring Company",
                StartingInvestmentSum = 100000,
                EnterpriseId = enterprises[2].Id,
            }
        };

        modelBuilder.Entity<Project>().HasData(projects);
        return projects;
    }
}
