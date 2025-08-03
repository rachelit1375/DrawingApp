using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Drawings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: 123,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Drawings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier");
        }
    }
}
