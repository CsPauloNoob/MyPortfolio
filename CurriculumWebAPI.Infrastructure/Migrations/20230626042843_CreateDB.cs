using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurriculumWebAPI.Infrastructure.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                name: "Formacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Instituicao = table.Column<string>(type: "TEXT", nullable: false),
                    Curso = table.Column<string>(type: "TEXT", nullable: false),
                    AnoConclusao = table.Column<int>(type: "INTEGER", nullable: false),
                    CurriculumId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formacao_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formacao_CurriculumId",
                table: "Formacao",
                column: "CurriculumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formacao");

            migrationBuilder.DropTable(
                name: "Curriculum");
        }
    }
}
