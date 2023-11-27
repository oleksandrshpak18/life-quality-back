using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_quality_back.Migrations
{
    public partial class TreatmentStrategyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreatmentStrategies",
                columns: table => new
                {
                    TreatmentStrategyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentStrategyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentStrategyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentStrategies", x => x.TreatmentStrategyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreatmentStrategies");
        }
    }
}
