using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class UpdateDB8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Dot",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Dot");
        }
    }
}
