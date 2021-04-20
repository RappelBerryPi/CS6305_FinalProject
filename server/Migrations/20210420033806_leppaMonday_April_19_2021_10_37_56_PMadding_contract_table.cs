using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class leppaMonday_April_19_2021_10_37_56_PMadding_contract_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractDeployments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ByteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeploymentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDeployments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDeployments");
        }
    }
}
