using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuturoDoTrabalho.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataMatricula",
                table: "Matriculas",
                newName: "DataInscricao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInscricao",
                table: "Matriculas",
                newName: "DataMatricula");
        }
    }
}
