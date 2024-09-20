using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddSMSTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sms_DutyTelephone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Sort = table.Column<int>(nullable: false, comment: "序号"),
                    Dept = table.Column<string>(maxLength: 50, nullable: true, comment: "科室"),
                    No = table.Column<string>(maxLength: 20, nullable: true, comment: "手机编号"),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true, comment: "手机号码"),
                    GreenRoads = table.Column<string>(maxLength: 200, nullable: true, comment: "关联绿通"),
                    IsEnabled = table.Column<bool>(nullable: false, comment: "是否开启绿通消息"),
                    TagSettingId = table.Column<Guid>(nullable: false, comment: "标签设置Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms_DutyTelephone", x => x.Id);
                },
                comment: "值班电话");

            migrationBuilder.CreateTable(
                name: "Sms_TagSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true, comment: "标签名称"),
                    IsSendMessage = table.Column<bool>(nullable: false, comment: "是否发送短信")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms_TagSettings", x => x.Id);
                },
                comment: "标签管理");

            migrationBuilder.CreateTable(
                name: "Sms_TextMessageRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TextMessage = table.Column<string>(nullable: true, comment: "短信消息"),
                    SendTime = table.Column<DateTime>(nullable: false, comment: "短信发送时间"),
                    TaskInfoId = table.Column<Guid>(nullable: false, comment: "任务单Id"),
                    TaskInfoNum = table.Column<string>(maxLength: 50, nullable: true, comment: "任务单号"),
                    SendToPhone = table.Column<string>(nullable: true, comment: "发送到的手机号码"),
                    Response = table.Column<string>(nullable: true, comment: "短信发送响应")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms_TextMessageRecord", x => x.Id);
                },
                comment: "短信消息记录");

            migrationBuilder.CreateTable(
                name: "Sms_TextMessageTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true, comment: "模板内容"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除，0=否，1=是"),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true, comment: "乐观锁"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms_TextMessageTemplate", x => x.Id);
                },
                comment: "短信模板");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sms_DutyTelephone");

            migrationBuilder.DropTable(
                name: "Sms_TagSettings");

            migrationBuilder.DropTable(
                name: "Sms_TextMessageRecord");

            migrationBuilder.DropTable(
                name: "Sms_TextMessageTemplate");
        }
    }
}
