using Microsoft.EntityFrameworkCore.Migrations;

namespace JFJRepresentacoes.Migrations
{
    public partial class addinfouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "autorizacao",
                table: "usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "usuarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "autorizacao",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "nome",
                table: "usuarios");
        }
    }
}
