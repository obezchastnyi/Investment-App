﻿// <auto-generated />
using System;
using InvestmentApp.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestmentAppServer.Migrations
{
    [DbContext(typeof(InvestmentAppDbContext))]
    partial class InvestmentAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InvestmentApp.Models.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Sum")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Portfolio");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f3a078fc-ec37-4446-89c1-90d81b394103"),
                            Name = "T-Shirts",
                            Sum = 10
                        },
                        new
                        {
                            Id = new Guid("c4a3499e-739a-4cf3-8ac8-869e0a185996"),
                            Name = "Soccer Club",
                            Sum = 10
                        },
                        new
                        {
                            Id = new Guid("2c3fda82-2fe0-4b68-bddd-2772dddec7cb"),
                            Name = "IT Company",
                            Sum = 10
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.HasIndex("UserRoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2cb7b742-6b7a-4199-a045-11f45b9deef7"),
                            PasswordHash = "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256",
                            UserName = "OPR",
                            UserRoleId = 2
                        },
                        new
                        {
                            Id = new Guid("346249dc-6bd1-4615-a2e4-f6b12cfb8296"),
                            PasswordHash = "1FDF083EC56DCB80969048F198F2E6EB5AF57C27268204F4426825A5911DD1C9:B88C83CFA45F673373E19DA810B5367C:50000:SHA256",
                            UserName = "OFR",
                            UserRoleId = 4
                        },
                        new
                        {
                            Id = new Guid("bb3c015c-4d28-4734-9e22-d55a2e358df0"),
                            PasswordHash = "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256",
                            UserName = "Expert",
                            UserRoleId = 3
                        },
                        new
                        {
                            Id = new Guid("1b79109e-d55d-4f4d-8361-7995ed7f378c"),
                            PasswordHash = "020FDA3C6F9AA1B774720843FA5E1CF8D24C41C24BE99FCBC50BAB42EADE2C72:07F2DB38E00C991E6EDE4BFD6DED2A5F:50000:SHA256",
                            UserName = "Admin",
                            UserRoleId = 1
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Code = "Reader"
                        },
                        new
                        {
                            Id = 3,
                            Code = "Writer"
                        },
                        new
                        {
                            Id = 4,
                            Code = "Creator"
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.User", b =>
                {
                    b.HasOne("InvestmentApp.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
