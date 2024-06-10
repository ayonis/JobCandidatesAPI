using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Task.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    First_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Last_name = table.Column<string>(type: "varchar(300)", nullable: false),
                    Phone_number = table.Column<string>(type: "varchar(50)", nullable: true),
                    Time_interval_call = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LinkedIn_profile_URL = table.Column<string>(type: "varchar(2000)", nullable: true),
                    GitHub_profile_URL = table.Column<string>(type: "varchar(2000)", nullable: true),
                    comment = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
