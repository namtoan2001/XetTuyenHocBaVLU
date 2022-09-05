using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class AddVaiTroTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaVaiTro",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "VaiTro",
                table: "TaiKhoan");

            migrationBuilder.AddColumn<int>(
                name: "VaiTroId",
                table: "TaiKhoan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaiTro");

            migrationBuilder.DropColumn(
                name: "VaiTroId",
                table: "TaiKhoan");

            migrationBuilder.AlterColumn<double>(
                name: "ID",
                table: "TruongTHPT",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Id",
                table: "TP_QH_PX",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "MA_TONGIAO",
                table: "TonGiao",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "MA_TINHTP",
                table: "TinhTP",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "MaVaiTro",
                table: "TaiKhoan",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VaiTro",
                table: "TaiKhoan",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "ID",
                table: "Nganh",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "MA_DANTOC",
                table: "DanToc",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);
        }
    }
}
