using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeBackend4EdicaoAlura.Migrations
{
    public partial class AddingCategoriasDeDespesas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Despesas");
        }
    }
}
