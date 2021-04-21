using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class leppaTuesday_April_20_2021_10_59_01_PMadding_GUID_to_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "ShopItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GUID",
                table: "ShopItems");
        }
    }
}
