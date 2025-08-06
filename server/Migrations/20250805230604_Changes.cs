using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drawings_Users_UserId1",
                table: "Drawings");

            migrationBuilder.DropIndex(
                name: "IX_Drawings_UserId1",
                table: "Drawings");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Drawings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Drawings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drawings_UserId1",
                table: "Drawings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Drawings_Users_UserId1",
                table: "Drawings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
