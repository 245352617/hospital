using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class v2286prescribe_curstor_rule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_PrescribeCustomRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "医嘱名称"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "每次剂量（急诊的）"),
                    DefaultDosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "默认规格剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位（急诊的）"),
                    Unpack = table.Column<int>(type: "int", nullable: false, comment: "门诊拆分属性 0=最小单位总量取整 1=包装单位总量取整 2=最小单位每次取整 3=包装单位每次取整 4=最小单位可拆分"),
                    BigPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "包装价格"),
                    BigPackFactor = table.Column<int>(type: "int", nullable: false, comment: "大包装系数(拆零系数)"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "包装单位"),
                    SmallPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "小包装单价"),
                    SmallPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "小包装单位"),
                    SmallPackFactor = table.Column<int>(type: "int", nullable: false, comment: "小包装系数(拆零系数)"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "包装规格")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_PrescribeCustomRule", x => x.Id);
                },
                comment: "自定义规则药品一次剂量名单 (自己维护)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_PrescribeCustomRule");
        }
    }
}
