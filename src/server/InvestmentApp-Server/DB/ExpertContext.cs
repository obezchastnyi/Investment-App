using System;
using System.Collections.Generic;
using InvestmentApp.Models;
using InvestmentApp.Models.Experts;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<Expert> Expert { get; set; }

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

    private static Expert InitExpertsData(ModelBuilder modelBuilder)
    {
        var expert = new Expert
        {
            Id = Guid.NewGuid(),
            Name = "Alex",
            SurName = "Samson",
            MiddleName = "John",
            Specialty = "Math",
            WorkPlace = "Science Academy",
            CompetenceCoefficient = 3,
        };

        modelBuilder.Entity<Expert>().HasData(expert);
        return expert;
    }

    private static void InitExpertIndustryData(ModelBuilder modelBuilder, Expert expert, Industry industry)
    {
        var expertIndustry = new ExpertIndustry
        {
            Id = Guid.NewGuid(),
            IndustryId = industry.Id,
            ExpertId = expert.Id,
        };

        modelBuilder.Entity<ExpertIndustry>().HasData(expertIndustry);
    }

    private static void InitExpertProjectsData(ModelBuilder modelBuilder,
        IList<Project> projects, Expert expert, Period period, Possibility possibility)
    {
        modelBuilder.Entity<ExpertProject>().HasData(
            new ExpertProject
            {
                Id = Guid.NewGuid(),
                ExpertId = expert.Id,
                ProjectId = projects[0].Id,
                PeriodId = period.Id,
                PossibilityId = possibility.Id,
                CashFlowRate = 740,
            },
            new ExpertProject
            {
                Id = Guid.NewGuid(),
                ExpertId = expert.Id,
                ProjectId = projects[1].Id,
                PeriodId = period.Id,
                PossibilityId = possibility.Id,
                CashFlowRate = 346,
            },
            new ExpertProject
            {
                Id = Guid.NewGuid(),
                ExpertId = expert.Id,
                ProjectId = projects[2].Id,
                PeriodId = period.Id,
                PossibilityId = possibility.Id,
                CashFlowRate = 914,
            });
    }
}
