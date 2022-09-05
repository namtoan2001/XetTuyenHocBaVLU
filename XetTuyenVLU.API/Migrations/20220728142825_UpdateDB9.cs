using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoSungHocBa");

            migrationBuilder.DropTable(
                name: "DieuChinhThongTin");

            migrationBuilder.CreateTable(
                name: "LoaiHinhThuc",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHinhThuc = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHinhThuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThoiGian",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiId = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoiGian", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoaiHinhThuc");

            migrationBuilder.DropTable(
                name: "ThoiGian");

            migrationBuilder.CreateTable(
                name: "BoSungHocBa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    TrangThaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoSungHocBa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DieuChinhThongTin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    TrangThaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieuChinhThongTin", x => x.ID);
                });
        }
    }
}
