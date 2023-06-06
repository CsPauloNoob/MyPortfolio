using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurriculumWebAPI.Infrastructure.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", nullable: true),
                    ExperienciaProfissional = table.Column<string>(type: "TEXT", nullable: true),
                    Habilidades = table.Column<string>(type: "TEXT", nullable: true),
                    SobreMim = table.Column<string>(type: "TEXT", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Instituicao = table.Column<string>(type: "TEXT", nullable: false),
                    Curso = table.Column<string>(type: "TEXT", nullable: false),
                    AnoConclusao = table.Column<int>(type: "INTEGER", nullable: false),
                    CurriculumId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educacao_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educacao_CurriculumId",
                table: "Educacao",
                column: "CurriculumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educacao");

            migrationBuilder.DropTable(
                name: "Curriculum");
        }
    }
}
