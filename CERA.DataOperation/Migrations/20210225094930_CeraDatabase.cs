using Microsoft.EntityFrameworkCore.Migrations;

namespace CERA.DataOperation.Migrations
{
    public partial class CeraDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "tbl_Subscription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_Subscription",
                table: "tbl_Subscription",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_Subscription",
                table: "tbl_Subscription");

            migrationBuilder.RenameTable(
                name: "tbl_Subscription",
                newName: "Subscriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "SubscriptionId");
        }
    }
}
