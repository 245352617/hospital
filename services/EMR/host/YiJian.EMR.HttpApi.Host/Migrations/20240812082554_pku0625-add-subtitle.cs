using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.EMR.Migrations
{
    public partial class pku0625addsubtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "EmrPatientEmr",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "EmrPatientEmr");
             
        }
    }
}
