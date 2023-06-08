using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurriculumWebAPI.Infrastructure.Migrations
{
    public partial class FixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educacao_Curriculum_CurriculumId",
                table: "Educacao");

            migrationBuilder.DropIndex(
                name: "IX_Educacao_CurriculumId",
                table: "Educacao");

            migrationBuilder.DropColumn(
                name: "CurriculumId",
                table: "Educacao");

            migrationBuilder.AddColumn<int>(
                name: "EducacaoId",
                table: "Curriculum",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_EducacaoId",
                table: "Curriculum",
                column: "EducacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculum_Educacao_EducacaoId",
                table: "Curriculum",
                column: "EducacaoId",
                principalTable: "Educacao",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Educacao_EducacaoId",
                table: "Curriculum");

            migrationBuilder.DropIndex(
                name: "IX_Curriculum_EducacaoId",
                table: "Curriculum");

            migrationBuilder.DropColumn(
                name: "EducacaoId",
                table: "Curriculum");

            migrationBuilder.AddColumn<int>(
                name: "CurriculumId",
                table: "Educacao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educacao_CurriculumId",
                table: "Educacao",
                column: "CurriculumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educacao_Curriculum_CurriculumId",
                table: "Educacao",
                column: "CurriculumId",
                principalTable: "Curriculum",
                principalColumn: "Id");
        }
    }
}
