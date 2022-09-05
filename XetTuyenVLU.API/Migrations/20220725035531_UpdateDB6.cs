using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrangThaiId",
                table: "ThongBao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiId",
                table: "DieuChinhThongTin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiId",
                table: "BoSungHocBa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiId",
                table: "ThongBao");

            migrationBuilder.DropColumn(
                name: "TrangThaiId",
                table: "DieuChinhThongTin");

            migrationBuilder.DropColumn(
                name: "TrangThaiId",
                table: "BoSungHocBa");
        }
    }
}
