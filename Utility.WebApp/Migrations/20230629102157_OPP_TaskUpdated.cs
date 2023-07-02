using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Utility.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class OPP_TaskUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OPP_Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OPP_Tasks");
        }
    }
}
