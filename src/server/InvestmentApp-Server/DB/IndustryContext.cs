using System;
using System.Collections.Generic;
using InvestmentApp.Models.Industries;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<Industry> Industry { get; set; }

    public virtual DbSet<Criteria> Criteria { get; set; }

    public virtual DbSet<IndustryCriteria> IndustryCriteria { get; set; }

    private static void BuildIndustryModels(ModelBuilder modelBuilder)
    {
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
            entity.HasIndex(e => e.IndustrySpecificWeight);

            entity.HasOne(e => e.Industry)
                .WithMany()
                .HasForeignKey(e => e.IndustryId)
                .IsRequired();

            entity.HasOne(e => e.Criteria)
                .WithMany()
                .HasForeignKey(e => e.CriteriaId)
                .IsRequired();
        });
    }

    private static IList<Criteria> InitCriteriaData(ModelBuilder modelBuilder)
    {
        var criterias = new[]
        {
            new Criteria { Id = Guid.NewGuid(), Name = "Profitability" },
            new Criteria { Id = Guid.NewGuid(), Name = "Risk" },
            new Criteria { Id = Guid.NewGuid(), Name = "Durability" }
        };

        modelBuilder.Entity<Criteria>().HasData(criterias);
        return criterias;
    }

    private static IList<Industry> InitIndustryData(ModelBuilder modelBuilder)
    {
        var industries = new[]
        {
            new Industry { Id = Guid.NewGuid(), Name = "Science" },
            new Industry { Id = Guid.NewGuid(), Name = "Metallurgy" },
            new Industry { Id = Guid.NewGuid(), Name = "Software Development" }
        };

        modelBuilder.Entity<Industry>().HasData(industries);
        return industries;
    }

    private static void InitIndustryCriteriaData(
        ModelBuilder modelBuilder, IList<Industry> industries, IList<Criteria> criterias)
    {
        modelBuilder.Entity<IndustryCriteria>().HasData(
            new IndustryCriteria
            {
                Id = Guid.NewGuid(),
                IndustryId = industries[0].Id,
                CriteriaId = criterias[0].Id,
                IndustrySpecificWeight = 3,
            },
            new IndustryCriteria
            {
                Id = Guid.NewGuid(),
                IndustryId = industries[1].Id,
                CriteriaId = criterias[1].Id,
                IndustrySpecificWeight = 7,
            },
            new IndustryCriteria
            {
                Id = Guid.NewGuid(),
                IndustryId = industries[2].Id,
                CriteriaId = criterias[2].Id,
                IndustrySpecificWeight = 19,
            });
    }
}
