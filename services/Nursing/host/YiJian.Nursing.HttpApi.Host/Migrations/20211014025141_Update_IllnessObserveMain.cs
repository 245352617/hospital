using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class Update_IllnessObserveMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IllnessObserveId",
                table: "NursingIllnessObserveOutput",
                newName: "IllnessObserveMainId");

            migrationBuilder.RenameColumn(
                name: "IllnessObserveId",
                table: "NursingIllnessObserveOther",
                newName: "IllnessObserveMainId");

            migrationBuilder.RenameColumn(
                name: "IllnessObserveId",
                table: "NursingIllnessObserveInput",
                newName: "IllnessObserveMainId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveOutput_IllnessObserveMainId",
                table: "NursingIllnessObserveOutput",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveOther_IllnessObserveMainId",
                table: "NursingIllnessObserveOther",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveInput_IllnessObserveMainId",
                table: "NursingIllnessObserveInput",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NursingIllnessObserveInput_NursingIllnessObserveMain_IllnessObserveMainId",
                table: "NursingIllnessObserveInput",
                column: "IllnessObserveMainId",
                principalTable: "NursingIllnessObserveMain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NursingIllnessObserveOther_NursingIllnessObserveMain_IllnessObserveMainId",
                table: "NursingIllnessObserveOther",
                column: "IllnessObserveMainId",
                principalTable: "NursingIllnessObserveMain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NursingIllnessObserveOutput_NursingIllnessObserveMain_IllnessObserveMainId",
                table: "NursingIllnessObserveOutput",
                column: "IllnessObserveMainId",
                principalTable: "NursingIllnessObserveMain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NursingIllnessObserveInput_NursingIllnessObserveMain_IllnessObserveMainId",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropForeignKey(
                name: "FK_NursingIllnessObserveOther_NursingIllnessObserveMain_IllnessObserveMainId",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropForeignKey(
                name: "FK_NursingIllnessObserveOutput_NursingIllnessObserveMain_IllnessObserveMainId",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropIndex(
                name: "IX_NursingIllnessObserveOutput_IllnessObserveMainId",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropIndex(
                name: "IX_NursingIllnessObserveOther_IllnessObserveMainId",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropIndex(
                name: "IX_NursingIllnessObserveInput_IllnessObserveMainId",
                table: "NursingIllnessObserveInput");

            migrationBuilder.RenameColumn(
                name: "IllnessObserveMainId",
                table: "NursingIllnessObserveOutput",
                newName: "IllnessObserveId");

            migrationBuilder.RenameColumn(
                name: "IllnessObserveMainId",
                table: "NursingIllnessObserveOther",
                newName: "IllnessObserveId");

            migrationBuilder.RenameColumn(
                name: "IllnessObserveMainId",
                table: "NursingIllnessObserveInput",
                newName: "IllnessObserveId");
        }
    }
}
