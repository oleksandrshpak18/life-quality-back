using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_quality_back.Migrations
{
    public partial class addDescriptionToQuestionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionnaireDescription",
                table: "Questionnaires",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionnaireDescription",
                table: "Questionnaires");
        }
    }
}
