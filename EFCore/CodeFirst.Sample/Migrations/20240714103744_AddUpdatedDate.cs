using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Sample.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "HR",
                table: "Employees",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 14, 13, 37, 44, 59, DateTimeKind.Local).AddTicks(8883));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "HR",
                table: "Employees");
        }
    }
}
