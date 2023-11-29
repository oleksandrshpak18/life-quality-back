using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_quality_back.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_Results_ResultsId",
                table: "PatientAnswers");

            migrationBuilder.DropIndex(
                name: "IX_PatientAnswers_ResultsId",
                table: "PatientAnswers");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "PatientAnswers");

            migrationBuilder.DropColumn(
                name: "ResultsId",
                table: "PatientAnswers");

            migrationBuilder.CreateTable(
                name: "ResultsPatientAnswer",
                columns: table => new
                {
                    ResultsPatientAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultsId = table.Column<int>(type: "int", nullable: false),
                    PatientAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultsPatientAnswer", x => x.ResultsPatientAnswerId);
                    table.ForeignKey(
                        name: "FK_ResultsPatientAnswer_PatientAnswers_PatientAnswerId",
                        column: x => x.PatientAnswerId,
                        principalTable: "PatientAnswers",
                        principalColumn: "PatientAnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultsPatientAnswer_Results_ResultsId",
                        column: x => x.ResultsId,
                        principalTable: "Results",
                        principalColumn: "ResultsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultsPatientAnswer_PatientAnswerId",
                table: "ResultsPatientAnswer",
                column: "PatientAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultsPatientAnswer_ResultsId",
                table: "ResultsPatientAnswer",
                column: "ResultsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultsPatientAnswer");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "PatientAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResultsId",
                table: "PatientAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientAnswers_ResultsId",
                table: "PatientAnswers",
                column: "ResultsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAnswers_Results_ResultsId",
                table: "PatientAnswers",
                column: "ResultsId",
                principalTable: "Results",
                principalColumn: "ResultsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
