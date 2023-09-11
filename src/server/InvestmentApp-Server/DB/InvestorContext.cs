using System;
using System.Collections.Generic;
using InvestmentApp.Models.Investors;
using InvestmentApp.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<Investor> Investor { get; set; }

    public virtual DbSet<InvestorProject> InvestorProject { get; set; }

    private static void BuildInvestorModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Investor>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);

            entity.Property(e => e.SurName).IsRequired();
            entity.HasIndex(e => e.SurName);
        });

        modelBuilder.Entity<InvestorProject>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.MinIncomeRate).IsRequired();
            entity.Property(e => e.MaxRiskRate).IsRequired();

            entity.HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .IsRequired();

            entity.HasOne(e => e.Investor)
                .WithMany()
                .HasForeignKey(e => e.InvestorId)
                .IsRequired();
        });
    }

    private static IList<Investor> InitInvestorsData(ModelBuilder modelBuilder)
    {
        var investors = new[]
        {
            new Investor
            {
                Id = Guid.NewGuid(),
                Name = "Fred",
                SurName = "Andrews",
            },
            new Investor
            {
                Id = Guid.NewGuid(),
                Name = "Joe",
                SurName = "Fig",
            },
            new Investor
            {
                Id = Guid.NewGuid(),
                Name = "Jenna",
                SurName = "Pipe",
            }
        };

        modelBuilder.Entity<Investor>().HasData(investors);
        return investors;
    }

    private static void InitInvestorProjectData(
        ModelBuilder modelBuilder, IList<Investor> investors, IList<Project> projects)
    {
        modelBuilder.Entity<InvestorProject>().HasData(
            new InvestorProject
            {
                Id = Guid.NewGuid(),
                InvestorId = investors[0].Id,
                ProjectId = projects[0].Id,
                MinIncomeRate = 10,
                MaxRiskRate = 40,
            },
            new InvestorProject
            {
                Id = Guid.NewGuid(),
                InvestorId = investors[1].Id,
                ProjectId = projects[1].Id,
                MinIncomeRate = 30,
                MaxRiskRate = 60,
            },
            new InvestorProject
            {
                Id = Guid.NewGuid(),
                InvestorId = investors[2].Id,
                ProjectId = projects[2].Id,
                MinIncomeRate = 50,
                MaxRiskRate = 90,
            });
    }
}
