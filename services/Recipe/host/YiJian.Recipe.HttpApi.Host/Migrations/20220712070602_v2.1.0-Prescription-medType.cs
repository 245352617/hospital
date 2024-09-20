using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210PrescriptionmedType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedType",
                table: "RC_Prescription",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                comment: "对应his处方识别（C）、医技序号（Y）");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedType",
                table: "RC_Prescription");
        }
    }
}
