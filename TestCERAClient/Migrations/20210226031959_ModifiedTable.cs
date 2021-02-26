using Microsoft.EntityFrameworkCore.Migrations;

namespace CERAAPI.Migrations
{
    public partial class ModifiedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "tbl_CloudPlugIns");

            migrationBuilder.RenameColumn(
                name: "FullyQualifiedName",
                table: "tbl_CloudPlugIns",
                newName: "FullyQualifiedClassName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullyQualifiedClassName",
                table: "tbl_CloudPlugIns",
                newName: "FullyQualifiedName");

            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "tbl_CloudPlugIns",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
