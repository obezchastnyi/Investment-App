using System;
using InvestmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public class InvestmentAppDbContext : DbContext
{
    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserRole> UserRole { get; set; }

    public virtual DbSet<Portfolio> Portfolio { get; set; }

    public InvestmentAppDbContext(DbContextOptions<InvestmentAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserName).IsRequired();
            entity.HasIndex(e => e.UserName).IsUnique();

            entity.Property(e => e.PasswordHash).IsRequired();

            entity.HasOne<UserRole>()
                .WithMany()
                .HasForeignKey(e => e.UserRoleId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Code).IsRequired();
            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.Sum).IsRequired();
        });

        InitUsersData(modelBuilder);
        InitUserRolesData(modelBuilder);
        InitPortfoliosData(modelBuilder);
    }

    private static void InitUsersData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "OPR",
                UserRoleId = 2,
                // passcode: opr_pass
                PasswordHash = "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256",
            },
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "OFR",
                UserRoleId = 4,
                // passcode: ofr_pass
                PasswordHash = "1FDF083EC56DCB80969048F198F2E6EB5AF57C27268204F4426825A5911DD1C9:B88C83CFA45F673373E19DA810B5367C:50000:SHA256",
            },
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "Expert",
                UserRoleId = 3,
                // passcode: expert_pass
                PasswordHash = "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256",
            },
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "UserAdmin",
                UserRoleId = 1,
                // passcode: admin_pass
                PasswordHash = "020FDA3C6F9AA1B774720843FA5E1CF8D24C41C24BE99FCBC50BAB42EADE2C72:07F2DB38E00C991E6EDE4BFD6DED2A5F:50000:SHA256",
            });
    }

    private static void InitUserRolesData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {
                Id = 1,
                Code = "Admin",
            },
            new UserRole
            {
                Id = 2,
                Code = "Reader",
            },
            new UserRole
            {
                Id = 3,
                Code = "Writer",
            },
            new UserRole
            {
                Id = 4,
                Code = "Creator",
            });
    }

    private static void InitPortfoliosData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Portfolio>().HasData(
            new Portfolio
            {
                Id = Guid.NewGuid(),
                Name = "T-Shirts",
                Sum = 10000,
            },
            new Portfolio
            {
                Id = Guid.NewGuid(),
                Name = "Soccer Club",
                Sum = 100000000,
            },
            new Portfolio
            {
                Id = Guid.NewGuid(),
                Name = "IT Company",
                Sum = 1000000,
            });
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add InvestmentApp_Migration
 * dotnet ef database update
 */
