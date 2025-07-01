using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSD_WinformsApp.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackupFiles",
                columns: table => new
                {
                    BackupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackupFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupFiles", x => x.BackupId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackupFiles");
        }
    }
}
