using System;
using System.Collections.Generic;
using InvestmentApp.Models.Experts;
using InvestmentApp.Models.Industries;
using InvestmentApp.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<Expert> Expert { get; set; }

    public virtual DbSet<Possibility> Possibility { get; set; }

    public virtual DbSet<Period> Period { get; set; }

    public virtual DbSet<ExpertIndustry> ExpertIndustry { get; set; }

    public virtual DbSet<ExpertProject> ExpertProject { get; set; }

    private static void BuildExpertModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expert>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);

            entity.Property(e => e.SurName).IsRequired();
            entity.HasIndex(e => e.SurName);

            entity.Property(e => e.CompetenceCoefficient).IsRequired();

            entity.Property(e => e.MiddleName);
            entity.Property(e => e.WorkPlace);
            entity.Property(e => e.Specialty);
        });

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

        modelBuilder.Entity<ExpertIndustry>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Rate).IsRequired();

            entity.HasOne(e => e.Expert)
                .WithMany()
                .HasForeignKey(e => e.ExpertId)
                .IsRequired();

            entity.HasOne(e => e.Industry)
                .WithMany()
                .HasForeignKey(e => e.IndustryId)
                .IsRequired();
        });

        modelBuilder.Entity<ExpertProject>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CashFlowRate).IsRequired();

            entity.HasOne(e => e.Expert)
                .WithMany()
                .HasForeignKey(e => e.ExpertId)
                .IsRequired();

            entity.HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .IsRequired();

            entity.HasOne(e => e.Period)
                .WithMany()
                .HasForeignKey(e => e.PeriodId)
                .IsRequired();

            entity.HasOne(e => e.Possibility)
                .WithMany()
                .HasForeignKey(e => e.PossibilityId)
                .IsRequired();
        });
    }

    private static IList<Expert> InitExpertsData(ModelBuilder modelBuilder)
    {
        var experts = new[]
        {
            new Expert
            {
                Id = Guid.NewGuid(),
                Name = "Alex",
                SurName = "Samson",
                MiddleName = "John",
                Specialty = "Math",
                WorkPlace = "Science Academy",
                CompetenceCoefficient = 3,
            },
            new Expert
            {
                Id = Guid.NewGuid(),
                Name = "Harry",
                SurName = "Potter",
                MiddleName = "James",
                Specialty = "Welding",
                WorkPlace = "Pipe Industry",
                CompetenceCoefficient = 5,
            },
            new Expert
            {
                Id = Guid.NewGuid(),
                Name = "Ray",
                SurName = "Philips",
                MiddleName = "Markus",
                Specialty = "IT Software Engineer",
                WorkPlace = "Amazon",
                CompetenceCoefficient = 9,
            }
        };

        modelBuilder.Entity<Expert>().HasData(experts);
        return experts;
    }

    private static IList<Possibility> InitPossibilityData(ModelBuilder modelBuilder)
    {
        var possibilities = new[]
        {
            new Possibility { Id = Guid.NewGuid(), Rate = 3 },
            new Possibility { Id = Guid.NewGuid(), Rate = 8 },
            new Possibility { Id = Guid.NewGuid(), Rate = 13 }
        };

        modelBuilder.Entity<Possibility>().HasData(possibilities);
        return possibilities;
    }

    private static IList<Period> InitPeriodsData(ModelBuilder modelBuilder)
    {
        var periods = new[]
        {
            new Period
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2024, 1, 1),
                DiscountRate = 2,
                RiskFreeDiscountRate = 3,
            },
            new Period
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2023, 4, 15),
                EndDate = new DateTime(2026, 8, 31),
                DiscountRate = 10,
                RiskFreeDiscountRate = 15,
            },
            new Period
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2033, 12, 31),
                DiscountRate = 17,
                RiskFreeDiscountRate = 21,
            }
        };

        modelBuilder.Entity<Period>().HasData(periods);
        return periods;
    }

    private static void InitExpertIndustryData(
        ModelBuilder modelBuilder, IList<Expert> experts, IList<Industry> industries)
    {
        modelBuilder.Entity<ExpertIndustry>().HasData(
            new ExpertIndustry
            {
                Id = Guid.NewGuid(),
                IndustryId = industries[0].Id,
                ExpertId = experts[0].Id,
            },
            new ExpertIndustry
            {
                Id = Guid.NewGuid(),
                IndustryId = industries[1].Id,
                ExpertId = experts[1].Id,
            },
            new ExpertIndustry
            {
                Id = Guid.NewGuid(),
                IndustryId = industries[2].Id,
                ExpertId = experts[2].Id,
            });
    }

    private static void InitExpertProjectsData(ModelBuilder modelBuilder,
        IList<Project> projects, IList<Expert> experts, IList<Period> periods, IList<Possibility> possibilities)
    {
        modelBuilder.Entity<ExpertProject>().HasData(
            new ExpertProject
            {
                Id = Guid.NewGuid(),
                ExpertId = experts[0].Id,
                ProjectId = projects[0].Id,
                PeriodId = periods[0].Id,
                PossibilityId = possibilities[0].Id,
                CashFlowRate = 740,
            },
            new ExpertProject
            {
                Id = Guid.NewGuid(),
                ExpertId = experts[1].Id,
                ProjectId = projects[1].Id,
                PeriodId = periods[1].Id,
                PossibilityId = possibilities[1].Id,
                CashFlowRate = 346,
            },
            new ExpertProject
            {
                Id = Guid.NewGuid(),
                ExpertId = experts[2].Id,
                ProjectId = projects[2].Id,
                PeriodId = periods[2].Id,
                PossibilityId = possibilities[2].Id,
                CashFlowRate = 914,
            });
    }
}
