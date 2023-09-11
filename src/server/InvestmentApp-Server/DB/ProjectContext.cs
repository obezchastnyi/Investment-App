using System;
using System.Collections.Generic;
using InvestmentApp.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<Enterprise> Enterprise { get; set; }

    public virtual DbSet<Project> Project { get; set; }

    private static void BuildProjectModels(ModelBuilder modelBuilder)
    {
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
