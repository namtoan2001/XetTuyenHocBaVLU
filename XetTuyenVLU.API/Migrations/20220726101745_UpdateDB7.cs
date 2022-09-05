using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Footer",
                table: "ThongBao");

            migrationBuilder.DropColumn(
                name: "MaDot",
                table: "ThongBao");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ThongBao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ThongBao",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Footer",
                table: "ThongBao",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaDot",
                table: "ThongBao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
