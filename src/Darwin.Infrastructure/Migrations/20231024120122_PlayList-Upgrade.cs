using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlayListUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasUser",
                table: "PlayLists");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "PlayLists",
                newName: "IsPublic");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "PlayLists",
                newName: "IsPrivate");

            migrationBuilder.AddColumn<bool>(
                name: "HasUser",
                table: "PlayLists",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
