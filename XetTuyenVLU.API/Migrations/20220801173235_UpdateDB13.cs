using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhuongAn",
                table: "HoSoTHPT",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhuongAn",
                table: "HoSoTHPT");
        }
    }
}
