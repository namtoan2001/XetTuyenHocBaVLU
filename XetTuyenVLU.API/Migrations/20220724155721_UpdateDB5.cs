using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongBaoTrungTuyen");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "Dot",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "Dot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "DieuChinhThongTin",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "DieuChinhThongTin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "BoSungHocBa",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "BoSungHocBa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoaiThongBao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThongBao = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThongBao", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Footer = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    LoaiThongBaoId = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoaiThongBao");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "Dot");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "Dot");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "DieuChinhThongTin");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "DieuChinhThongTin");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "BoSungHocBa");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "BoSungHocBa");

            migrationBuilder.CreateTable(
                name: "ThongBaoTrungTuyen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HanNhapHoc = table.Column<DateTime>(type: "datetime", nullable: false),
                    HanNopHocPhi = table.Column<DateTime>(type: "datetime", nullable: false),
                    HanNopThuTuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    SoTienHocPhi = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoTrungTuyen", x => x.ID);
                });
        }
    }
}
