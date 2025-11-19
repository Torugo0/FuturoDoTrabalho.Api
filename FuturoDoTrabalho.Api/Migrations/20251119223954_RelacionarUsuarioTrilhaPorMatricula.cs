using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuturoDoTrabalho.Api.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarUsuarioTrilhaPorMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInscricao",
                table: "Matriculas",
                newName: "DataMatricula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataMatricula",
                table: "Matriculas",
                newName: "DataInscricao");
        }
    }
}
