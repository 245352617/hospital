using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace YiJian.Nursing.Migrations
{
    public partial class delete_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingIllnessObserveHeader");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveInput");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveOther");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveOutput");

            migrationBuilder.DropTable(
                name: "NursingRecipeExecRefund");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveMain");

            migrationBuilder.DropColumn(
                name: "CombineStatus",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "LastStatus",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "NursingStatus",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "NursingStatus",
                table: "NursingRecipe");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "NursingTreat",
                type: "nvarchar(max)",
                nullable: true,
                comment: "项目类型名称");

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "NursingTreat",
                type: "nvarchar(max)",
                nullable: true,
                comment: "项目类型");

            migrationBuilder.AlterColumn<decimal>(
                name: "RecieveQty",
                table: "NursingOwnMedicine",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "领量(数量)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "领量(数量)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingOwnMedicine",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "每次剂量");

            migrationBuilder.CreateTable(
                name: "NursingClinicalEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者信息主键"),
                    EventCategoryCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "事件分类编码"),
                    EventCategory = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "事件分类"),
                    HappenTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "发生时间"),
                    UpDownFlagCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "上下标编码"),
                    UpDownFlag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "上下标"),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "事件描述"),
                    NurseCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护士账号"),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护士名字"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingClinicalEvent", x => x.Id);
                },
                comment: "临床事件表");

            migrationBuilder.CreateTable(
                name: "NursingTemperature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者信息主键"),
                    MeasureDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "测量日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingTemperature", x => x.Id);
                },
                comment: "体温单表");

            migrationBuilder.CreateTable(
                name: "NursingTemperatureDynamic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemperatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "体温表主键"),
                    TemperatureRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "体温记录表主键"),
                    PropertyCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "属性字段"),
                    PropertyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "属性名称"),
                    PropertyValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "属性值"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    ExtralFlag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "额外标记"),
                    NurseCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护士账号"),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护士名字"),
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
                    table.PrimaryKey("PK_NursingTemperatureDynamic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingTemperatureDynamic_NursingTemperature_TemperatureId",
                        column: x => x.TemperatureId,
                        principalTable: "NursingTemperature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "体温表动态属性");

            migrationBuilder.CreateTable(
                name: "NursingTemperatureRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemperatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "体温表主键"),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理记录表主键"),
                    TimePoint = table.Column<int>(type: "int", nullable: false, comment: "测量时间点"),
                    MeasureTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "测量时间"),
                    Temperature = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "体温（单位℃）"),
                    RetestTemperature = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "复测体温（单位℃）"),
                    CoolingWay = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "降温方式"),
                    MeasurePosition = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "测量位置"),
                    Pulse = table.Column<int>(type: "int", nullable: true, comment: "脉搏P(次/min)"),
                    Breathing = table.Column<int>(type: "int", nullable: true, comment: "呼吸(次/min)"),
                    HeartRate = table.Column<int>(type: "int", nullable: true, comment: "心率(次/min)"),
                    SystolicPressure = table.Column<int>(type: "int", nullable: true, comment: "血压BP收缩压(mmHg)"),
                    DiastolicPressure = table.Column<int>(type: "int", nullable: true, comment: "血压BP舒张压(mmHg)"),
                    PainDegree = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "疼痛程度"),
                    Consciousness = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "意识"),
                    NurseCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护士账号"),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护士名字"),
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
                    table.PrimaryKey("PK_NursingTemperatureRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingTemperatureRecord_NursingTemperature_TemperatureId",
                        column: x => x.TemperatureId,
                        principalTable: "NursingTemperature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "体温表记录");

            migrationBuilder.CreateIndex(
                name: "IX_NursingTemperatureDynamic_TemperatureId",
                table: "NursingTemperatureDynamic",
                column: "TemperatureId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingTemperatureRecord_TemperatureId",
                table: "NursingTemperatureRecord",
                column: "TemperatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingClinicalEvent");

            migrationBuilder.DropTable(
                name: "NursingTemperatureDynamic");

            migrationBuilder.DropTable(
                name: "NursingTemperatureRecord");

            migrationBuilder.DropTable(
                name: "NursingTemperature");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "NursingTreat");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "NursingTreat");

            migrationBuilder.AddColumn<int>(
                name: "CombineStatus",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "操作后状态");

            migrationBuilder.AddColumn<int>(
                name: "LastStatus",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "操作前状态");

            migrationBuilder.AddColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医嘱执行状态");

            migrationBuilder.AddColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RecieveQty",
                table: "NursingOwnMedicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "领量(数量)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "领量(数量)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingOwnMedicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "每次剂量");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false, comment: "等级，1:一级，2：二级"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "项目编码"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "项目名称"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaxEarlyWarning = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最大值预警"),
                    MaxValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最大值"),
                    MinEarlyWarning = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最小值预警"),
                    MinValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最小值"),
                    ParentCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "父级编码"),
                    Unit = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "单位")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveHeader", x => x.Id);
                },
                comment: "病情观察头部");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveMain",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodSugar = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血糖"),
                    Breathing = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "呼吸"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ConsciousnessCode = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "意识编码"),
                    ConsciousnessName = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "意识名称"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeartRate = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "心率"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeftPupilSize = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "左瞳大小"),
                    LeftPupilType = table.Column<int>(type: "int", nullable: false, comment: "左瞳类型，0：灵敏，1：迟钝，2：无反应"),
                    ObserveTime = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "时间"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "traige患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "患者id"),
                    RightPupilSize = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "右瞳大小"),
                    RightPupilType = table.Column<int>(type: "int", nullable: false, comment: "右瞳类型，0：灵敏，1：迟钝，2：无反应"),
                    Sbp = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血压BP收缩压"),
                    Sdp = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血压BP舒张压"),
                    SpO2 = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血氧饱和度"),
                    Temperature = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "体温"),
                    VisitNo = table.Column<int>(type: "int", nullable: false, comment: "就诊号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveMain", x => x.Id);
                },
                comment: "病情观察");

            migrationBuilder.CreateTable(
                name: "NursingRecipeExecRefund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "审批时间"),
                    ConfirmmerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审批人编码"),
                    ConfirmmerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "审批人名称"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "关联医嘱执行表编号"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsWithDrawn = table.Column<bool>(type: "bit", nullable: false, comment: "是否退药退费"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请护士编码"),
                    NurseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请护士名称"),
                    NursingRefundStatus = table.Column<int>(type: "int", nullable: false, comment: "退药退费状态"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "病人标识"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "原因"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    RefundQty = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    RefundType = table.Column<int>(type: "int", nullable: false, comment: "退药退费类型"),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "申请时间"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExecRefund", x => x.Id);
                },
                comment: "医嘱执行退款退费表");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodTransfusion = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "输血"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Diet = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "饮食"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IllnessObserveMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NasalFeeding = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "鼻饲"),
                    TotalInput = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "入量合计"),
                    Water = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "水")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveInput", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingIllnessObserveInput_NursingIllnessObserveMain_IllnessObserveMainId",
                        column: x => x.IllnessObserveMainId,
                        principalTable: "NursingIllnessObserveMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病情观察入量表");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveOther",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ElectricCardioversion = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "电复律"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    G = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    IllnessObserveMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    S = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    TurnOver = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "翻身")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveOther", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingIllnessObserveOther_NursingIllnessObserveMain_IllnessObserveMainId",
                        column: x => x.IllnessObserveMainId,
                        principalTable: "NursingIllnessObserveMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病情观察其他");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveOutput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IllnessObserveMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Shit = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "大便"),
                    SputumSuction = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "吸痰"),
                    Sweat = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "汗液"),
                    TotalOutput = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "出量合计"),
                    UrineVolume = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "尿量"),
                    VenousInflow = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "静脉入量"),
                    Vomit = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "呕吐")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveOutput", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingIllnessObserveOutput_NursingIllnessObserveMain_IllnessObserveMainId",
                        column: x => x.IllnessObserveMainId,
                        principalTable: "NursingIllnessObserveMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病情观察出量表");

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveInput_IllnessObserveMainId",
                table: "NursingIllnessObserveInput",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveOther_IllnessObserveMainId",
                table: "NursingIllnessObserveOther",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveOutput_IllnessObserveMainId",
                table: "NursingIllnessObserveOutput",
                column: "IllnessObserveMainId",
                unique: true);
        }
    }
}
