using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentApp.Entities;
using InvestmentApp.Models.Authentication;
using InvestmentApp.Models.Experts;
using InvestmentApp.Models.Investors;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApp.DB;

public partial class InvestmentAppDbContext
{
    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserRole> UserRole { get; set; }

    private static void BuildAuthenticationModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserName).IsRequired();
            entity.HasIndex(e => e.UserName).IsUnique();

            entity.Property(e => e.PasswordHash).IsRequired();

            entity.HasOne(e => e.UserRole)
                .WithMany()
                .HasForeignKey(e => e.UserRoleId)
                .IsRequired();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Code).IsRequired();
            entity.HasIndex(e => e.Code).IsUnique();
        });
    }

    private static IEnumerable<UserRole> InitUserRolesData(ModelBuilder modelBuilder)
    {
        var userRoles = Enum.GetValues(typeof(UserRoles))
            .Cast<UserRoles>()
            .ToDictionary(ur => (int)ur, ur => ur.ToString());

        var roles = userRoles
            .Select(role =>
                new UserRole
                {
                    Id = role.Key,
                    Code = role.Value
                })
            .ToList();

        modelBuilder.Entity<UserRole>().HasData(roles);
        return roles;
    }

    private static void InitUsersData(
        ModelBuilder modelBuilder, Expert expert, Investor investor, IEnumerable<UserRole> roles)
    {
        modelBuilder.Entity<User>().HasData(
            // Investor -> OPR
            new User
            {
                Id = investor.Id,
                UserName = $"{investor.SurName}-{investor.Name}",
                UserRoleId = roles.First(r => r.Code == UserRoles.Reader.ToString()).Id,
                // passcode: opr_pass
                PasswordHash = "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256",
            },
            // Expert
            new User
            {
                Id = expert.Id,
                UserName = $"{expert.SurName}-{expert.Name}",
                UserRoleId = roles.First(r => r.Code == UserRoles.Writer.ToString()).Id,
                // passcode: expert_pass
                PasswordHash = "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256",
            },
            // admin -> OFR
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "Admin-OFR",
                UserRoleId = roles.First(r => r.Code == UserRoles.Creator.ToString()).Id,
                // passcode: ofr_pass
                PasswordHash = "1FDF083EC56DCB80969048F198F2E6EB5AF57C27268204F4426825A5911DD1C9:B88C83CFA45F673373E19DA810B5367C:50000:SHA256",
            },
            // Super Admin
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "SuperAdmin",
                UserRoleId = roles.First(r => r.Code == UserRoles.Admin.ToString()).Id,
                // passcode: admin_pass
                PasswordHash = "020FDA3C6F9AA1B774720843FA5E1CF8D24C41C24BE99FCBC50BAB42EADE2C72:07F2DB38E00C991E6EDE4BFD6DED2A5F:50000:SHA256",
            });
    }
}
