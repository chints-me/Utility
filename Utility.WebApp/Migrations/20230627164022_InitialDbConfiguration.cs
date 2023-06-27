using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Utility.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OPP_Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPP_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OPP_Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OPP_ParentTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OPP_ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPP_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OPP_Tasks_OPP_Projects_OPP_ProjectId",
                        column: x => x.OPP_ProjectId,
                        principalTable: "OPP_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OPP_Tasks_OPP_ProjectId",
                table: "OPP_Tasks",
                column: "OPP_ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OPP_Tasks");

            migrationBuilder.DropTable(
                name: "OPP_Projects");
        }
    }
}
