using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrDataElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称标题"),
                    IsElement = table.Column<bool>(type: "bit", nullable: false, comment: "是否是元素"),
                    Parent = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "父级级联Id"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "层级，默认=0"),
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
                    table.PrimaryKey("PK_EmrDataElement", x => x.Id);
                },
                comment: "数据元集合根");

            migrationBuilder.CreateTable(
                name: "EmrDataElementDropdown",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataElementItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "元素项Id"),
                    IsAllowMultiple = table.Column<bool>(type: "bit", nullable: false, comment: "允许多选"),
                    IsSortByTime = table.Column<bool>(type: "bit", nullable: false, comment: "数值勾选按照时间排序"),
                    IsDynamicallyLoad = table.Column<bool>(type: "bit", nullable: false, comment: "动态加载下拉列表"),
                    DataSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "来源"),
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
                    table.PrimaryKey("PK_EmrDataElementDropdown", x => x.Id);
                },
                comment: "输入域类型下拉项目");

            migrationBuilder.CreateTable(
                name: "EmrDataElementDropdownItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataElementDropdownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "输入域类型下拉项目Id"),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "文本"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "数值"),
                    ListText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "指定的列表文本"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分组名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrDataElementDropdownItem", x => x.Id);
                },
                comment: "元素下拉项");

            migrationBuilder.CreateTable(
                name: "EmrDataElementItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "编号"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    Watermark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "背景文本"),
                    Tips = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "提示文本"),
                    BeginMargin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "起始边框"),
                    EndMargin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "结尾边框"),
                    FixedWidth = table.Column<int>(type: "int", nullable: false, comment: "固定宽度"),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false, comment: "只读状态"),
                    DataSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "数据源"),
                    BindPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "绑定路径"),
                    CascadeExpression = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "级联表达式"),
                    NumericalExpression = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "数值表达式"),
                    CanModify = table.Column<bool>(type: "bit", nullable: false, comment: "用户可以直接修改内容"),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false, comment: "允许被删除"),
                    InputType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "输入域类型"),
                    NeedVerification = table.Column<bool>(type: "bit", nullable: false, comment: "是否开启校验"),
                    VerificationExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "校验表达式"),
                    DataElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "数据元Id"),
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
                    table.PrimaryKey("PK_EmrDataElementItem", x => x.Id);
                },
                comment: "数据元素");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataElementDropdown_DataElementItemId",
                table: "EmrDataElementDropdown",
                column: "DataElementItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataElementDropdownItem_DataElementDropdownId",
                table: "EmrDataElementDropdownItem",
                column: "DataElementDropdownId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataElementItem_DataElementId",
                table: "EmrDataElementItem",
                column: "DataElementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrDataElement");

            migrationBuilder.DropTable(
                name: "EmrDataElementDropdown");

            migrationBuilder.DropTable(
                name: "EmrDataElementDropdownItem");

            migrationBuilder.DropTable(
                name: "EmrDataElementItem");
        }
    }
}
