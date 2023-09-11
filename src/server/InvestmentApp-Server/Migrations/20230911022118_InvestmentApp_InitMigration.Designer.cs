﻿// <auto-generated />
using System;
using InvestmentApp.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestmentAppServer.Migrations
{
    [DbContext(typeof(InvestmentAppDbContext))]
    [Migration("20230911022118_InvestmentApp_InitMigration")]
    partial class InvestmentApp_InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InvestmentApp.Models.Authentication.User", b =>
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
                            Id = new Guid("0c26b67f-be4c-4cd6-ad91-786af8ed9460"),
                            PasswordHash = "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256",
                            UserName = "Andrews-Fred",
                            UserRoleId = 2
                        },
                        new
                        {
                            Id = new Guid("9cf3b6cf-6802-4fc8-a6f0-9a4f44477fd0"),
                            PasswordHash = "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256",
                            UserName = "Fig-Joe",
                            UserRoleId = 2
                        },
                        new
                        {
                            Id = new Guid("fbd832a5-901e-425d-ab07-982483c854fe"),
                            PasswordHash = "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256",
                            UserName = "Pipe-Jenna",
                            UserRoleId = 2
                        },
                        new
                        {
                            Id = new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"),
                            PasswordHash = "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256",
                            UserName = "Samson-Alex",
                            UserRoleId = 3
                        },
                        new
                        {
                            Id = new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"),
                            PasswordHash = "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256",
                            UserName = "Potter-Harry",
                            UserRoleId = 3
                        },
                        new
                        {
                            Id = new Guid("51268369-dabf-42c8-b31d-803729eb1666"),
                            PasswordHash = "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256",
                            UserName = "Philips-Ray",
                            UserRoleId = 3
                        },
                        new
                        {
                            Id = new Guid("18ce42df-5f39-4fed-b13a-4a38328898b3"),
                            PasswordHash = "1FDF083EC56DCB80969048F198F2E6EB5AF57C27268204F4426825A5911DD1C9:B88C83CFA45F673373E19DA810B5367C:50000:SHA256",
                            UserName = "Admin-OFR",
                            UserRoleId = 4
                        },
                        new
                        {
                            Id = new Guid("710c47be-b1c7-4a57-a684-2407b1f7affd"),
                            PasswordHash = "020FDA3C6F9AA1B774720843FA5E1CF8D24C41C24BE99FCBC50BAB42EADE2C72:07F2DB38E00C991E6EDE4BFD6DED2A5F:50000:SHA256",
                            UserName = "SuperAdmin",
                            UserRoleId = 1
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Authentication.UserRole", b =>
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

            modelBuilder.Entity("InvestmentApp.Models.Experts.Expert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CompetenceCoefficient")
                        .HasColumnType("double precision");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Specialty")
                        .HasColumnType("text");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkPlace")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("SurName");

                    b.ToTable("Expert");

                    b.HasData(
                        new
                        {
                            Id = new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"),
                            CompetenceCoefficient = 3.0,
                            MiddleName = "John",
                            Name = "Alex",
                            Specialty = "Math",
                            SurName = "Samson",
                            WorkPlace = "Science Academy"
                        },
                        new
                        {
                            Id = new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"),
                            CompetenceCoefficient = 5.0,
                            MiddleName = "James",
                            Name = "Harry",
                            Specialty = "Welding",
                            SurName = "Potter",
                            WorkPlace = "Pipe Industry"
                        },
                        new
                        {
                            Id = new Guid("51268369-dabf-42c8-b31d-803729eb1666"),
                            CompetenceCoefficient = 9.0,
                            MiddleName = "Markus",
                            Name = "Ray",
                            Specialty = "IT Software Engineer",
                            SurName = "Philips",
                            WorkPlace = "Amazon"
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Experts.ExpertIndustry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExpertId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IndustryId")
                        .HasColumnType("uuid");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.HasIndex("IndustryId");

                    b.ToTable("ExpertIndustry");

                    b.HasData(
                        new
                        {
                            Id = new Guid("48c3bcea-0e12-40ad-ad8d-9ba3f60c1b54"),
                            ExpertId = new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"),
                            IndustryId = new Guid("a3bb9b9c-883f-43f0-bfc8-27a42bf53cc2"),
                            Rate = 0.0
                        },
                        new
                        {
                            Id = new Guid("74725262-a1eb-4ab8-894f-dbcc04b5ecb3"),
                            ExpertId = new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"),
                            IndustryId = new Guid("a45131dd-0dce-4a84-8e10-7f6c396a723b"),
                            Rate = 0.0
                        },
                        new
                        {
                            Id = new Guid("05b59c27-38ac-4e05-9038-3d90c30c14e1"),
                            ExpertId = new Guid("51268369-dabf-42c8-b31d-803729eb1666"),
                            IndustryId = new Guid("1f2ccc2d-ae35-483f-a237-fe23f556e6cd"),
                            Rate = 0.0
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Experts.ExpertProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CashFlowRate")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ExpertId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PeriodId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PossibilityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("PossibilityId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ExpertProject");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8c3acdb4-14fb-4d5e-a171-4469f9bd8102"),
                            CashFlowRate = 740.0,
                            ExpertId = new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"),
                            PeriodId = new Guid("5740207b-9feb-468d-9f6f-a318f5cc8548"),
                            PossibilityId = new Guid("aa625f05-1b1f-47fa-b105-4de6ec523bff"),
                            ProjectId = new Guid("508f7b09-e707-4301-9d91-4351e3eb3ee5")
                        },
                        new
                        {
                            Id = new Guid("5f6b94f9-fcef-4dce-82db-b0925fdd6e24"),
                            CashFlowRate = 346.0,
                            ExpertId = new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"),
                            PeriodId = new Guid("eeef44b4-1d94-448d-b048-cfc8e70ea600"),
                            PossibilityId = new Guid("0d838c63-b364-466c-86f4-98e26a951f4b"),
                            ProjectId = new Guid("9fddd156-7999-40ff-a1f4-a83f4f30f478")
                        },
                        new
                        {
                            Id = new Guid("483745d2-7f92-46e5-a6fd-d99d24811c01"),
                            CashFlowRate = 914.0,
                            ExpertId = new Guid("51268369-dabf-42c8-b31d-803729eb1666"),
                            PeriodId = new Guid("f7a7438e-5532-4363-b672-660447241a0b"),
                            PossibilityId = new Guid("ce0cb774-04d6-4f16-b7a2-a3b001afcf79"),
                            ProjectId = new Guid("c2898f6e-c407-4a9a-9635-65da791c88f7")
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Experts.Period", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("DiscountRate")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("RiskFreeDiscountRate")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DiscountRate");

                    b.HasIndex("RiskFreeDiscountRate");

                    b.ToTable("Period");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5740207b-9feb-468d-9f6f-a318f5cc8548"),
                            DiscountRate = 2.0,
                            EndDate = new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)),
                            RiskFreeDiscountRate = 3.0,
                            StartDate = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0))
                        },
                        new
                        {
                            Id = new Guid("eeef44b4-1d94-448d-b048-cfc8e70ea600"),
                            DiscountRate = 10.0,
                            EndDate = new DateTimeOffset(new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)),
                            RiskFreeDiscountRate = 15.0,
                            StartDate = new DateTimeOffset(new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = new Guid("f7a7438e-5532-4363-b672-660447241a0b"),
                            DiscountRate = 17.0,
                            EndDate = new DateTimeOffset(new DateTime(2033, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)),
                            RiskFreeDiscountRate = 21.0,
                            StartDate = new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Experts.Possibility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Rate");

                    b.ToTable("Possibility");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa625f05-1b1f-47fa-b105-4de6ec523bff"),
                            Rate = 3.0
                        },
                        new
                        {
                            Id = new Guid("0d838c63-b364-466c-86f4-98e26a951f4b"),
                            Rate = 8.0
                        },
                        new
                        {
                            Id = new Guid("ce0cb774-04d6-4f16-b7a2-a3b001afcf79"),
                            Rate = 13.0
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Industries.Criteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Criteria");

                    b.HasData(
                        new
                        {
                            Id = new Guid("79a4fdb6-5753-492c-b209-ccee9978d3a9"),
                            Name = "Profitability"
                        },
                        new
                        {
                            Id = new Guid("3f406c04-1edb-4ee3-8d50-04f182e27e3f"),
                            Name = "Risk"
                        },
                        new
                        {
                            Id = new Guid("01f45a5e-fa67-4236-8335-996267b3a5c4"),
                            Name = "Durability"
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Industries.Industry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Industry");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a3bb9b9c-883f-43f0-bfc8-27a42bf53cc2"),
                            Name = "Science"
                        },
                        new
                        {
                            Id = new Guid("a45131dd-0dce-4a84-8e10-7f6c396a723b"),
                            Name = "Metallurgy"
                        },
                        new
                        {
                            Id = new Guid("1f2ccc2d-ae35-483f-a237-fe23f556e6cd"),
                            Name = "Software Development"
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Industries.IndustryCriteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CriteriaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IndustryId")
                        .HasColumnType("uuid");

                    b.Property<double>("IndustrySpecificWeight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CriteriaId");

                    b.HasIndex("IndustryId");

                    b.HasIndex("IndustrySpecificWeight");

                    b.ToTable("IndustryCriteria");

                    b.HasData(
                        new
                        {
                            Id = new Guid("935055fc-efd0-4e53-9bcf-f1ce1676dafc"),
                            CriteriaId = new Guid("79a4fdb6-5753-492c-b209-ccee9978d3a9"),
                            IndustryId = new Guid("a3bb9b9c-883f-43f0-bfc8-27a42bf53cc2"),
                            IndustrySpecificWeight = 3.0
                        },
                        new
                        {
                            Id = new Guid("263c1887-c99f-4e5a-820f-49e1cc7c8686"),
                            CriteriaId = new Guid("3f406c04-1edb-4ee3-8d50-04f182e27e3f"),
                            IndustryId = new Guid("a45131dd-0dce-4a84-8e10-7f6c396a723b"),
                            IndustrySpecificWeight = 7.0
                        },
                        new
                        {
                            Id = new Guid("3f1dbda0-99ec-47db-b026-5edbc69fb97c"),
                            CriteriaId = new Guid("01f45a5e-fa67-4236-8335-996267b3a5c4"),
                            IndustryId = new Guid("1f2ccc2d-ae35-483f-a237-fe23f556e6cd"),
                            IndustrySpecificWeight = 19.0
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Investors.Investor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("SurName");

                    b.ToTable("Investor");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c26b67f-be4c-4cd6-ad91-786af8ed9460"),
                            Name = "Fred",
                            SurName = "Andrews"
                        },
                        new
                        {
                            Id = new Guid("9cf3b6cf-6802-4fc8-a6f0-9a4f44477fd0"),
                            Name = "Joe",
                            SurName = "Fig"
                        },
                        new
                        {
                            Id = new Guid("fbd832a5-901e-425d-ab07-982483c854fe"),
                            Name = "Jenna",
                            SurName = "Pipe"
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Investors.InvestorProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<double>("MaxRiskRate")
                        .HasColumnType("double precision");

                    b.Property<double>("MinIncomeRate")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InvestorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("InvestorProject");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5e09f17-f870-408c-9b04-1a0e4298f395"),
                            InvestorId = new Guid("0c26b67f-be4c-4cd6-ad91-786af8ed9460"),
                            MaxRiskRate = 40.0,
                            MinIncomeRate = 10.0,
                            ProjectId = new Guid("508f7b09-e707-4301-9d91-4351e3eb3ee5")
                        },
                        new
                        {
                            Id = new Guid("ecc25fa1-f32f-4c4f-92fd-cf1b0e1832a4"),
                            InvestorId = new Guid("9cf3b6cf-6802-4fc8-a6f0-9a4f44477fd0"),
                            MaxRiskRate = 60.0,
                            MinIncomeRate = 30.0,
                            ProjectId = new Guid("9fddd156-7999-40ff-a1f4-a83f4f30f478")
                        },
                        new
                        {
                            Id = new Guid("05023d13-48a2-4339-a848-7977146bc6a0"),
                            InvestorId = new Guid("fbd832a5-901e-425d-ab07-982483c854fe"),
                            MaxRiskRate = 90.0,
                            MinIncomeRate = 50.0,
                            ProjectId = new Guid("c2898f6e-c407-4a9a-9635-65da791c88f7")
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Projects.Enterprise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TaxNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Enterprise");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8171d63b-ab82-4a4f-9157-82e5d36b9ebd"),
                            Address = "Ukraine, Kharkiv, Kharkiv Region",
                            BankAccount = "UA123456789",
                            Name = "T-Shirts Brand",
                            TaxNumber = 987321654L
                        },
                        new
                        {
                            Id = new Guid("aade6a14-a45a-40a9-93a9-9cd1f81fda2c"),
                            Address = "Ukraine, Kyiv, Kyiv Region",
                            BankAccount = "UA987654321",
                            Name = "Soccer Club",
                            TaxNumber = 321987654L
                        },
                        new
                        {
                            Id = new Guid("f797b583-bbe5-4d78-92a9-8730ca274faf"),
                            Address = "Ukraine, Dnipro, Dnipro Region",
                            BankAccount = "UA123789456",
                            Name = "IT Company",
                            TaxNumber = 123987456L
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EnterpriseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("StartingInvestmentSum")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Project");

                    b.HasData(
                        new
                        {
                            Id = new Guid("508f7b09-e707-4301-9d91-4351e3eb3ee5"),
                            EnterpriseId = new Guid("8171d63b-ab82-4a4f-9157-82e5d36b9ebd"),
                            Name = "T-Shirts Collection",
                            StartingInvestmentSum = 1000.0
                        },
                        new
                        {
                            Id = new Guid("9fddd156-7999-40ff-a1f4-a83f4f30f478"),
                            EnterpriseId = new Guid("aade6a14-a45a-40a9-93a9-9cd1f81fda2c"),
                            Name = "Soccer Club Tournament",
                            StartingInvestmentSum = 100000000.0
                        },
                        new
                        {
                            Id = new Guid("c2898f6e-c407-4a9a-9635-65da791c88f7"),
                            EnterpriseId = new Guid("f797b583-bbe5-4d78-92a9-8730ca274faf"),
                            Name = "IT Company Hiring Company",
                            StartingInvestmentSum = 100000.0
                        });
                });

            modelBuilder.Entity("InvestmentApp.Models.Authentication.User", b =>
                {
                    b.HasOne("InvestmentApp.Models.Authentication.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("InvestmentApp.Models.Experts.ExpertIndustry", b =>
                {
                    b.HasOne("InvestmentApp.Models.Experts.Expert", "Expert")
                        .WithMany()
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentApp.Models.Industries.Industry", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expert");

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("InvestmentApp.Models.Experts.ExpertProject", b =>
                {
                    b.HasOne("InvestmentApp.Models.Experts.Expert", "Expert")
                        .WithMany()
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentApp.Models.Experts.Period", "Period")
                        .WithMany()
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentApp.Models.Experts.Possibility", "Possibility")
                        .WithMany()
                        .HasForeignKey("PossibilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentApp.Models.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expert");

                    b.Navigation("Period");

                    b.Navigation("Possibility");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("InvestmentApp.Models.Industries.IndustryCriteria", b =>
                {
                    b.HasOne("InvestmentApp.Models.Industries.Criteria", "Criteria")
                        .WithMany()
                        .HasForeignKey("CriteriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentApp.Models.Industries.Industry", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Criteria");

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("InvestmentApp.Models.Investors.InvestorProject", b =>
                {
                    b.HasOne("InvestmentApp.Models.Investors.Investor", "Investor")
                        .WithMany()
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentApp.Models.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Investor");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("InvestmentApp.Models.Projects.Project", b =>
                {
                    b.HasOne("InvestmentApp.Models.Projects.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");
                });
#pragma warning restore 612, 618
        }
    }
}