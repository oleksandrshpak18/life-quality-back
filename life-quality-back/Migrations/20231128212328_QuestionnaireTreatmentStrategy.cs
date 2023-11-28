using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_quality_back.Migrations
{
    public partial class QuestionnaireTreatmentStrategy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionnaireTreatmentStrategy",
                columns: table => new
                {
                    QuestionnaireTreatmentStrategyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireId = table.Column<int>(type: "int", nullable: false),
                    TreatmentStrategyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireTreatmentStrategy", x => x.QuestionnaireTreatmentStrategyId);
                    table.ForeignKey(
                        name: "FK_QuestionnaireTreatmentStrategy_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "QuestionnaireId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireTreatmentStrategy_TreatmentStrategies_TreatmentStrategyId",
                        column: x => x.TreatmentStrategyId,
                        principalTable: "TreatmentStrategies",
                        principalColumn: "TreatmentStrategyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireTreatmentStrategy_QuestionnaireId",
                table: "QuestionnaireTreatmentStrategy",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireTreatmentStrategy_TreatmentStrategyId",
                table: "QuestionnaireTreatmentStrategy",
                column: "TreatmentStrategyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnaireTreatmentStrategy");
        }
    }
}
