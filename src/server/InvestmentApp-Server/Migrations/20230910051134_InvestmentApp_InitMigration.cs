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
                values: new object[] { new Guid("6a537539-3c57-4908-b36d-6594c9cb3dd0"), "Profitability" });

            migrationBuilder.InsertData(
                table: "Enterprise",
                columns: new[] { "Id", "Address", "BankAccount", "Name", "TaxNumber" },
                values: new object[,]
                {
                    { new Guid("0e641783-4755-4d6b-bd29-8f1219a592a4"), "Ukraine, Dnipro, Dnipro Region", "UA123789456", "IT Company", 123987456L },
                    { new Guid("72627cfc-4faf-49cf-a9d4-0066d2a4fad9"), "Ukraine, Kyiv, Kyiv Region", "UA987654321", "Soccer Club", 321987654L },
                    { new Guid("785cbb0b-5a68-4a80-a184-64672e2a4f64"), "Ukraine, Kharkiv, Kharkiv Region", "UA123456789", "T-Shirts Brand", 987321654L }
                });

            migrationBuilder.InsertData(
                table: "Expert",
                columns: new[] { "Id", "CompetenceCoefficient", "MiddleName", "Name", "Specialty", "SurName", "WorkPlace" },
                values: new object[] { new Guid("6eccd119-f439-4190-af2a-0ae86f1ba775"), 3.0, "John", "Alex", "Math", "Samson", "Science Academy" });

            migrationBuilder.InsertData(
                table: "Industry",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("070bf07f-d429-4041-a4fb-833980bc2784"), "Metallurgy" });

            migrationBuilder.InsertData(
                table: "Investor",
                columns: new[] { "Id", "Name", "SurName" },
                values: new object[] { new Guid("efcb778c-285f-4844-b184-c697c1b39c07"), "Fred", "Andrews" });

            migrationBuilder.InsertData(
                table: "Period",
                columns: new[] { "Id", "DiscountRate", "EndDate", "RiskFreeDiscountRate", "StartDate" },
                values: new object[] { new Guid("d86b00c7-8029-4db8-a662-fc5e812e2fb1"), 10.0, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), 5.0, new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Possibility",
                columns: new[] { "Id", "Rate" },
                values: new object[] { new Guid("b150ee68-de97-4b98-b71a-bc8e01dc1a52"), 16.0 });

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
                values: new object[] { new Guid("1ce932ce-cbc2-4bb6-bf5b-221cb2c8d926"), new Guid("6eccd119-f439-4190-af2a-0ae86f1ba775"), new Guid("070bf07f-d429-4041-a4fb-833980bc2784"), 0.0 });

            migrationBuilder.InsertData(
                table: "IndustryCriteria",
                columns: new[] { "Id", "CriteriaId", "IndustryId", "IndustrySpecificWeight" },
                values: new object[] { new Guid("ab218829-e291-45ec-bc23-36e0b9bf14d1"), new Guid("6a537539-3c57-4908-b36d-6594c9cb3dd0"), new Guid("070bf07f-d429-4041-a4fb-833980bc2784"), 7.0 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "EnterpriseId", "Name", "StartingInvestmentSum" },
                values: new object[,]
                {
                    { new Guid("2915a0f7-db73-4eb7-9337-636ca5108b87"), new Guid("785cbb0b-5a68-4a80-a184-64672e2a4f64"), "New T-Shirts Collection", 1000.0 },
                    { new Guid("7868f125-9068-42a9-833e-86e75bf9931a"), new Guid("72627cfc-4faf-49cf-a9d4-0066d2a4fad9"), "New Soccer Club Tournament", 100000000.0 },
                    { new Guid("e6d96611-5e32-47c7-a878-18fcd71af6f4"), new Guid("0e641783-4755-4d6b-bd29-8f1219a592a4"), "IT Company Hiring Company", 100000.0 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "PasswordHash", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { new Guid("0777899e-aeba-4b8c-a578-c918e8c4e962"), "020FDA3C6F9AA1B774720843FA5E1CF8D24C41C24BE99FCBC50BAB42EADE2C72:07F2DB38E00C991E6EDE4BFD6DED2A5F:50000:SHA256", "SuperAdmin", 1 },
                    { new Guid("6281aee8-8774-4e33-a616-bb1b06d175c9"), "1FDF083EC56DCB80969048F198F2E6EB5AF57C27268204F4426825A5911DD1C9:B88C83CFA45F673373E19DA810B5367C:50000:SHA256", "Admin", 4 },
                    { new Guid("6eccd119-f439-4190-af2a-0ae86f1ba775"), "518F612F0FFA27F8182A40AB87F4CDA9BBFA3A843D38088E512258DFA5297873:16A87429F8C22D68E6E1048D2439AEC0:50000:SHA256", "Samson-Alex", 3 },
                    { new Guid("efcb778c-285f-4844-b184-c697c1b39c07"), "4A3AC69954D82B1156AAD57E83C86B434E33B57AE9DF31F4D25A8A2000AD0FC4:CFE528ADCEB6D77A0BB1CE8F90ECDA1F:50000:SHA256", "Andrews-Fred", 2 }
                });

            migrationBuilder.InsertData(
                table: "ExpertProject",
                columns: new[] { "Id", "CashFlowRate", "ExpertId", "PeriodId", "PossibilityId", "ProjectId" },
                values: new object[,]
                {
                    { new Guid("5422b496-0d52-4ef4-8031-02da7eefe63a"), 346.0, new Guid("6eccd119-f439-4190-af2a-0ae86f1ba775"), new Guid("d86b00c7-8029-4db8-a662-fc5e812e2fb1"), new Guid("b150ee68-de97-4b98-b71a-bc8e01dc1a52"), new Guid("7868f125-9068-42a9-833e-86e75bf9931a") },
                    { new Guid("8ab82f5d-3c47-40c3-803f-6a9b7a6332a0"), 740.0, new Guid("6eccd119-f439-4190-af2a-0ae86f1ba775"), new Guid("d86b00c7-8029-4db8-a662-fc5e812e2fb1"), new Guid("b150ee68-de97-4b98-b71a-bc8e01dc1a52"), new Guid("2915a0f7-db73-4eb7-9337-636ca5108b87") },
                    { new Guid("9289308b-8cd0-4ec4-8498-f1124ee0fcd2"), 914.0, new Guid("6eccd119-f439-4190-af2a-0ae86f1ba775"), new Guid("d86b00c7-8029-4db8-a662-fc5e812e2fb1"), new Guid("b150ee68-de97-4b98-b71a-bc8e01dc1a52"), new Guid("e6d96611-5e32-47c7-a878-18fcd71af6f4") }
                });

            migrationBuilder.InsertData(
                table: "InvestorProject",
                columns: new[] { "Id", "InvestorId", "MaxRiskRate", "MinIncomeRate", "ProjectId" },
                values: new object[] { new Guid("9447dcb6-bc76-4ced-aa5f-0b5792c1aacf"), new Guid("efcb778c-285f-4844-b184-c697c1b39c07"), 60.0, 30.0, new Guid("2915a0f7-db73-4eb7-9337-636ca5108b87") });

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
                column: "IndustrySpecificWeight",
                unique: true);

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
