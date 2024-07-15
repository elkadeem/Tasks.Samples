using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Sample.AnimalsMigrationsTPH
{
    /// <inheritdoc />
    public partial class AddAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    FavoriteAnimalId = table.Column<int>(type: "int", nullable: true),
                    Vet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FavoriteToy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Animals_FavoriteAnimalId",
                        column: x => x.FavoriteAnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HumanPet",
                columns: table => new
                {
                    HumansId = table.Column<int>(type: "int", nullable: false),
                    PetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanPet", x => new { x.HumansId, x.PetsId });
                    table.ForeignKey(
                        name: "FK_HumanPet_Animals_HumansId",
                        column: x => x.HumansId,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HumanPet_Animals_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_FavoriteAnimalId",
                table: "Animals",
                column: "FavoriteAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_HumanPet_PetsId",
                table: "HumanPet",
                column: "PetsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumanPet");

            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
