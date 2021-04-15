using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class leppaWednesday_April_14_2021_11_57_07_PMadding_shop_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgAltText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    isSold = table.Column<bool>(type: "bit", nullable: false),
                    PurchasedById = table.Column<long>(type: "bigint", nullable: true),
                    BlockAddressOfPurchase = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopItems_AspNetUsers_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_PurchasedById",
                table: "ShopItems",
                column: "PurchasedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopItems");
        }
    }
}
