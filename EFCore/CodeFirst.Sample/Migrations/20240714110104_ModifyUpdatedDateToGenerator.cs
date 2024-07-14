using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Sample.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUpdatedDateToGenerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE View HR.vEmployees
                AS SELECT * FROM HR.Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP View HR.vEmployees");
        }
    }
}
