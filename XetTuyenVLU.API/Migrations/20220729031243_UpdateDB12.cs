using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThoiGian");

            migrationBuilder.CreateTable(
                name: "LichTrinh",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiId = table.Column<int>(type: "int", nullable: false),
                    HinhThucId = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichTrinh", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LichTrinh");

            migrationBuilder.CreateTable(
                name: "ThoiGian",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhThucId = table.Column<int>(type: "int", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    TrangThaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoiGian", x => x.ID);
                });
        }
    }
}
