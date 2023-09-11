using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestmentAppServer.Migrations
{
    /// <inheritdoc />
    public partial class InvestmentApp_InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    BankAccount = table.Column<string>(type: "text", nullable: false),
                    TaxNumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    CompetenceCoefficient = table.Column<double>(type: "double precision", nullable: false),
                    WorkPlace = table.Column<string>(type: "text", nullable: true),
                    Specialty = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DiscountRate = table.Column<double>(type: "double precision", nullable: false),
                    RiskFreeDiscountRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Possibility",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Possibility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartingInvestmentSum = table.Column<double>(type: "double precision", nullable: false),
                    EnterpriseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Enterprise_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertIndustry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IndustryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpertId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertIndustry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertIndustry_Expert_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Expert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertIndustry_Industry_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndustryCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IndustryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IndustrySpecificWeight = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndustryCriteria_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndustryCriteria_Industry_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    UserRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpertId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    PossibilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashFlowRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertProject_Expert_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Expert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertProject_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertProject_Possibility_PossibilityId",
                        column: x => x.PossibilityId,
                        principalTable: "Possibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestorProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    MinIncomeRate = table.Column<double>(type: "double precision", nullable: false),
                    MaxRiskRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestorProject_Investor_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestorProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Criteria",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01f45a5e-fa67-4236-8335-996267b3a5c4"), "Durability" },
                    { new Guid("3f406c04-1edb-4ee3-8d50-04f182e27e3f"), "Risk" },
                    { new Guid("79a4fdb6-5753-492c-b209-ccee9978d3a9"), "Profitability" }
                });

            migrationBuilder.InsertData(
                table: "Enterprise",
                columns: new[] { "Id", "Address", "BankAccount", "Name", "TaxNumber" },
                values: new object[,]
                {
                    { new Guid("8171d63b-ab82-4a4f-9157-82e5d36b9ebd"), "Ukraine, Kharkiv, Kharkiv Region", "UA123456789", "T-Shirts Brand", 987321654L },
                    { new Guid("aade6a14-a45a-40a9-93a9-9cd1f81fda2c"), "Ukraine, Kyiv, Kyiv Region", "UA987654321", "Soccer Club", 321987654L },
                    { new Guid("f797b583-bbe5-4d78-92a9-8730ca274faf"), "Ukraine, Dnipro, Dnipro Region", "UA123789456", "IT Company", 123987456L }
                });

            migrationBuilder.InsertData(
                table: "Expert",
                columns: new[] { "Id", "CompetenceCoefficient", "MiddleName", "Name", "Specialty", "SurName", "WorkPlace" },
                values: new object[,]
                {
                    { new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"), 3.0, "John", "Alex", "Math", "Samson", "Science Academy" },
                    { new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"), 5.0, "James", "Harry", "Welding", "Potter", "Pipe Industry" },
                    { new Guid("51268369-dabf-42c8-b31d-803729eb1666"), 9.0, "Markus", "Ray", "IT Software Engineer", "Philips", "Amazon" }
                });

            migrationBuilder.InsertData(
                table: "Industry",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1f2ccc2d-ae35-483f-a237-fe23f556e6cd"), "Software Development" },
                    { new Guid("a3bb9b9c-883f-43f0-bfc8-27a42bf53cc2"), "Science" },
                    { new Guid("a45131dd-0dce-4a84-8e10-7f6c396a723b"), "Metallurgy" }
                });

            migrationBuilder.InsertData(
                table: "Investor",
                columns: new[] { "Id", "Name", "SurName" },
                values: new object[,]
                {
                    { new Guid("0c26b67f-be4c-4cd6-ad91-786af8ed9460"), "Fred", "Andrews" },
                    { new Guid("9cf3b6cf-6802-4fc8-a6f0-9a4f44477fd0"), "Joe", "Fig" },
                    { new Guid("fbd832a5-901e-425d-ab07-982483c854fe"), "Jenna", "Pipe" }
                });

            migrationBuilder.InsertData(
                table: "Period",
                columns: new[] { "Id", "DiscountRate", "EndDate", "RiskFreeDiscountRate", "StartDate" },
                values: new object[,]
                {
                    { new Guid("5740207b-9feb-468d-9f6f-a318f5cc8548"), 2.0, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), 3.0, new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)) },
                    { new Guid("eeef44b4-1d94-448d-b048-cfc8e70ea600"), 10.0, new DateTimeOffset(new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)), 15.0, new DateTimeOffset(new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -4, 0, 0, 0)) },
                    { new Guid("f7a7438e-5532-4363-b672-660447241a0b"), 17.0, new DateTimeOffset(new DateTime(2033, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), 21.0, new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Possibility",
                columns: new[] { "Id", "Rate" },
                values: new object[,]
                {
                    { new Guid("0d838c63-b364-466c-86f4-98e26a951f4b"), 8.0 },
                    { new Guid("aa625f05-1b1f-47fa-b105-4de6ec523bff"), 3.0 },
                    { new Guid("ce0cb774-04d6-4f16-b7a2-a3b001afcf79"), 13.0 }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Reader" },
                    { 3, "Writer" },
                    { 4, "Creator" }
                });

            migrationBuilder.InsertData(
                table: "ExpertIndustry",
                columns: new[] { "Id", "ExpertId", "IndustryId", "Rate" },
                values: new object[,]
                {
                    { new Guid("05b59c27-38ac-4e05-9038-3d90c30c14e1"), new Guid("51268369-dabf-42c8-b31d-803729eb1666"), new Guid("1f2ccc2d-ae35-483f-a237-fe23f556e6cd"), 0.0 },
                    { new Guid("48c3bcea-0e12-40ad-ad8d-9ba3f60c1b54"), new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"), new Guid("a3bb9b9c-883f-43f0-bfc8-27a42bf53cc2"), 0.0 },
                    { new Guid("74725262-a1eb-4ab8-894f-dbcc04b5ecb3"), new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"), new Guid("a45131dd-0dce-4a84-8e10-7f6c396a723b"), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "IndustryCriteria",
                columns: new[] { "Id", "CriteriaId", "IndustryId", "IndustrySpecificWeight" },
                values: new object[,]
                {
                    { new Guid("263c1887-c99f-4e5a-820f-49e1cc7c8686"), new Guid("3f406c04-1edb-4ee3-8d50-04f182e27e3f"), new Guid("a45131dd-0dce-4a84-8e10-7f6c396a723b"), 7.0 },
                    { new Guid("3f1dbda0-99ec-47db-b026-5edbc69fb97c"), new Guid("01f45a5e-fa67-4236-8335-996267b3a5c4"), new Guid("1f2ccc2d-ae35-483f-a237-fe23f556e6cd"), 19.0 },
                    { new Guid("935055fc-efd0-4e53-9bcf-f1ce1676dafc"), new Guid("79a4fdb6-5753-492c-b209-ccee9978d3a9"), new Guid("a3bb9b9c-883f-43f0-bfc8-27a42bf53cc2"), 3.0 }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "EnterpriseId", "Name", "StartingInvestmentSum" },
                values: new object[,]
                {
                    { new Guid("508f7b09-e707-4301-9d91-4351e3eb3ee5"), new Guid("8171d63b-ab82-4a4f-9157-82e5d36b9ebd"), "T-Shirts Collection", 1000.0 },
                    { new Guid("9fddd156-7999-40ff-a1f4-a83f4f30f478"), new Guid("aade6a14-a45a-40a9-93a9-9cd1f81fda2c"), "Soccer Club Tournament", 100000000.0 },
                    { new Guid("c2898f6e-c407-4a9a-9635-65da791c88f7"), new Guid("f797b583-bbe5-4d78-92a9-8730ca274faf"), "IT Company Hiring Company", 100000.0 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "PasswordHash", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"), "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256", "Samson-Alex", 3 },
                    { new Guid("0c26b67f-be4c-4cd6-ad91-786af8ed9460"), "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256", "Andrews-Fred", 2 },
                    { new Guid("18ce42df-5f39-4fed-b13a-4a38328898b3"), "1FDF083EC56DCB80969048F198F2E6EB5AF57C27268204F4426825A5911DD1C9:B88C83CFA45F673373E19DA810B5367C:50000:SHA256", "Admin-OFR", 4 },
                    { new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"), "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256", "Potter-Harry", 3 },
                    { new Guid("51268369-dabf-42c8-b31d-803729eb1666"), "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256", "Philips-Ray", 3 },
                    { new Guid("710c47be-b1c7-4a57-a684-2407b1f7affd"), "020FDA3C6F9AA1B774720843FA5E1CF8D24C41C24BE99FCBC50BAB42EADE2C72:07F2DB38E00C991E6EDE4BFD6DED2A5F:50000:SHA256", "SuperAdmin", 1 },
                    { new Guid("9cf3b6cf-6802-4fc8-a6f0-9a4f44477fd0"), "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256", "Fig-Joe", 2 },
                    { new Guid("fbd832a5-901e-425d-ab07-982483c854fe"), "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256", "Pipe-Jenna", 2 }
                });

            migrationBuilder.InsertData(
                table: "ExpertProject",
                columns: new[] { "Id", "CashFlowRate", "ExpertId", "PeriodId", "PossibilityId", "ProjectId" },
                values: new object[,]
                {
                    { new Guid("483745d2-7f92-46e5-a6fd-d99d24811c01"), 914.0, new Guid("51268369-dabf-42c8-b31d-803729eb1666"), new Guid("f7a7438e-5532-4363-b672-660447241a0b"), new Guid("ce0cb774-04d6-4f16-b7a2-a3b001afcf79"), new Guid("c2898f6e-c407-4a9a-9635-65da791c88f7") },
                    { new Guid("5f6b94f9-fcef-4dce-82db-b0925fdd6e24"), 346.0, new Guid("2099f967-2c60-43c1-a7b1-55eeedb26306"), new Guid("eeef44b4-1d94-448d-b048-cfc8e70ea600"), new Guid("0d838c63-b364-466c-86f4-98e26a951f4b"), new Guid("9fddd156-7999-40ff-a1f4-a83f4f30f478") },
                    { new Guid("8c3acdb4-14fb-4d5e-a171-4469f9bd8102"), 740.0, new Guid("075920b4-da0b-42fc-80ad-3c03ecbfe7d2"), new Guid("5740207b-9feb-468d-9f6f-a318f5cc8548"), new Guid("aa625f05-1b1f-47fa-b105-4de6ec523bff"), new Guid("508f7b09-e707-4301-9d91-4351e3eb3ee5") }
                });

            migrationBuilder.InsertData(
                table: "InvestorProject",
                columns: new[] { "Id", "InvestorId", "MaxRiskRate", "MinIncomeRate", "ProjectId" },
                values: new object[,]
                {
                    { new Guid("05023d13-48a2-4339-a848-7977146bc6a0"), new Guid("fbd832a5-901e-425d-ab07-982483c854fe"), 90.0, 50.0, new Guid("c2898f6e-c407-4a9a-9635-65da791c88f7") },
                    { new Guid("b5e09f17-f870-408c-9b04-1a0e4298f395"), new Guid("0c26b67f-be4c-4cd6-ad91-786af8ed9460"), 40.0, 10.0, new Guid("508f7b09-e707-4301-9d91-4351e3eb3ee5") },
                    { new Guid("ecc25fa1-f32f-4c4f-92fd-cf1b0e1832a4"), new Guid("9cf3b6cf-6802-4fc8-a6f0-9a4f44477fd0"), 60.0, 30.0, new Guid("9fddd156-7999-40ff-a1f4-a83f4f30f478") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criteria_Name",
                table: "Criteria",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprise_Name",
                table: "Enterprise",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expert_Name",
                table: "Expert",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Expert_SurName",
                table: "Expert",
                column: "SurName");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertIndustry_ExpertId",
                table: "ExpertIndustry",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertIndustry_IndustryId",
                table: "ExpertIndustry",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProject_ExpertId",
                table: "ExpertProject",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProject_PeriodId",
                table: "ExpertProject",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProject_PossibilityId",
                table: "ExpertProject",
                column: "PossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProject_ProjectId",
                table: "ExpertProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Industry_Name",
                table: "Industry",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndustryCriteria_CriteriaId",
                table: "IndustryCriteria",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryCriteria_IndustryId",
                table: "IndustryCriteria",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryCriteria_IndustrySpecificWeight",
                table: "IndustryCriteria",
                column: "IndustrySpecificWeight");

            migrationBuilder.CreateIndex(
                name: "IX_Investor_Name",
                table: "Investor",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Investor_SurName",
                table: "Investor",
                column: "SurName");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorProject_InvestorId",
                table: "InvestorProject",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorProject_ProjectId",
                table: "InvestorProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Period_DiscountRate",
                table: "Period",
                column: "DiscountRate");

            migrationBuilder.CreateIndex(
                name: "IX_Period_RiskFreeDiscountRate",
                table: "Period",
                column: "RiskFreeDiscountRate");

            migrationBuilder.CreateIndex(
                name: "IX_Possibility_Rate",
                table: "Possibility",
                column: "Rate");

            migrationBuilder.CreateIndex(
                name: "IX_Project_EnterpriseId",
                table: "Project",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Name",
                table: "Project",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Code",
                table: "UserRole",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertIndustry");

            migrationBuilder.DropTable(
                name: "ExpertProject");

            migrationBuilder.DropTable(
                name: "IndustryCriteria");

            migrationBuilder.DropTable(
                name: "InvestorProject");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Expert");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropTable(
                name: "Possibility");

            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Investor");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Enterprise");
        }
    }
}
