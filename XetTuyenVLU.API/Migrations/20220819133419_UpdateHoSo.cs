using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateHoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DaGuiMail",
                table: "HoSoTHPT",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaGuiMail",
                table: "HoSoTHPT");
        }
    }
}
