using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Add_IllnessObserveMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键id"),
                    IllnessObserveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    TotalInput = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "入量合计"),
                    BloodTransfusion = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "输血"),
                    Diet = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "饮食"),
                    Water = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "水"),
                    NasalFeeding = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "鼻饲")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveInput", x => x.Id);
                },
                comment: "病情观察入量表");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveMain",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "traige患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "患者id"),
                    VisitNo = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "就诊号"),
                    ObserveTime = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "时间"),
                    Temperature = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "体温"),
                    HeartRate = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "心率"),
                    Breathing = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "呼吸"),
                    Sbp = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血压BP收缩压"),
                    Sdp = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血压BP舒张压"),
                    SpO2 = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血氧饱和度"),
                    ConsciousnessCode = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "意识编码"),
                    ConsciousnessName = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "意识名称"),
                    BloodSugar = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血糖"),
                    LeftPupilType = table.Column<int>(type: "int", nullable: false, comment: "左瞳类型，0：灵敏，1：迟钝，2：无反应"),
                    LeftPupilSize = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "左瞳大小"),
                    RightPupilType = table.Column<int>(type: "int", nullable: false, comment: "右瞳类型，0：灵敏，1：迟钝，2：无反应"),
                    RightPupilSize = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "右瞳大小"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveMain", x => x.Id);
                },
                comment: "病情观察");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveOther",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键id"),
                    IllnessObserveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    G = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    C = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    S = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    ElectricCardioversion = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "电复律"),
                    TurnOver = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "翻身")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveOther", x => x.Id);
                },
                comment: "病情观察其他");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveOutput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键id"),
                    IllnessObserveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    TotalOutput = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "出量合计"),
                    UrineVolume = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "尿量"),
                    Shit = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "大便"),
                    Vomit = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "呕吐"),
                    Sweat = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "汗液"),
                    SputumSuction = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "吸痰"),
                    VenousInflow = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "静脉入量")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveOutput", x => x.Id);
                },
                comment: "病情观察出量表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingIllnessObserveInput");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveMain");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveOther");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveOutput");
        }
    }
}
