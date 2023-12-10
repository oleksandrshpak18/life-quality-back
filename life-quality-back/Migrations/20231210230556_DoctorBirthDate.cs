using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_quality_back.Migrations
{
    public partial class DoctorBirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Doctors",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Doctors");
        }
    }
}
