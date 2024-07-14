using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Sample.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeChildAndCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatetionDate",
                schema: "HR",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "EmployeeChildren",
                schema: "HR",
                columns: table => new
                {
                    Serial = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeChildren", x => new { x.Serial, x.EmployeeId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeChildren",
                schema: "HR");

            migrationBuilder.DropColumn(
                name: "CreatetionDate",
                schema: "HR",
                table: "Employees");
        }
    }
}
