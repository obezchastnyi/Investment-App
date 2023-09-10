using System;
using InvestmentApp.Models;
using InvestmentApp.Models.Investors;
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

    private static Investor InitInvestorsData(ModelBuilder modelBuilder)
    {
        var investor = new Investor
        {
            Id = Guid.NewGuid(),
            Name = "Fred",
            SurName = "Andrews",
        };

        modelBuilder.Entity<Investor>().HasData(investor);
        return investor;
    }

    private static void InitInvestorProjectData(ModelBuilder modelBuilder, Investor investor, Project project)
    {
        var investorProject = new InvestorProject
        {
            Id = Guid.NewGuid(),
            InvestorId = investor.Id,
            ProjectId = project.Id,
            MinIncomeRate = 30,
            MaxRiskRate = 60,
        };

        modelBuilder.Entity<InvestorProject>().HasData(investorProject);
    }
}
