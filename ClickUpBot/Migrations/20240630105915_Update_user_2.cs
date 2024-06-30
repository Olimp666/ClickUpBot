using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickUpBot.Migrations
{
    /// <inheritdoc />
    public partial class Update_user_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "Users",
                newName: "DefaultTeamId");

            migrationBuilder.AddColumn<string>(
                name: "DefaultListId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSpaceId",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultListId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DefaultSpaceId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DefaultTeamId",
                table: "Users",
                newName: "ListId");
        }
    }
}
