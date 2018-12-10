using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GWC.Web.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BillMonth = table.Column<int>(nullable: true),
                    BillYear = table.Column<int>(nullable: true),
                    Cycle = table.Column<string>(maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    ImportBillingDate = table.Column<DateTime>(nullable: true),
                    BillingEmailCCDate = table.Column<DateTime>(nullable: true),
                    StatementDate = table.Column<DateTime>(nullable: true),
                    FirstCDRDate = table.Column<DateTime>(nullable: true),
                    LoadCDRDate = table.Column<DateTime>(nullable: true),
                    LastCDRDate = table.Column<DateTime>(nullable: true),
                    BillingDays = table.Column<int>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    BlockingDate = table.Column<DateTime>(nullable: true),
                    MessageId = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    StatementMailingDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingCalendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GwcLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogDate = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(maxLength: 50, nullable: true),
                    Logger = table.Column<string>(maxLength: 255, nullable: true),
                    Method = table.Column<string>(maxLength: 255, nullable: true),
                    Message = table.Column<string>(maxLength: 1000, nullable: true),
                    Exception = table.Column<string>(maxLength: 5000, nullable: true),
                    Line = table.Column<string>(maxLength: 20, nullable: true),
                    Data = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GwcLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(maxLength: 100, nullable: true),
                    Editor = table.Column<string>(maxLength: 100, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Volume = table.Column<string>(maxLength: 50, nullable: true),
                    Publisher = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<string>(maxLength: 50, nullable: true),
                    CD = table.Column<int>(nullable: true),
                    PrintPages = table.Column<int>(nullable: true),
                    Advertisement = table.Column<string>(maxLength: 1000, nullable: true),
                    Newspaper = table.Column<int>(nullable: true),
                    ExternalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceId = table.Column<int>(nullable: false),
                    Page = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<string>(maxLength: 50, nullable: true),
                    Comments = table.Column<string>(maxLength: 500, nullable: true),
                    Quote = table.Column<bool>(nullable: true),
                    Hide = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_SourceId",
                table: "Calendars",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingCalendar");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "GwcLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
