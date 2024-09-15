using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Contentlists.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IsFavoriteAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 11, 39, 24, 157, DateTimeKind.Local).AddTicks(927),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 12, 54, 27, 914, DateTimeKind.Local).AddTicks(5546));

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Playlists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Playlists");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 12, 54, 27, 914, DateTimeKind.Local).AddTicks(5546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 11, 39, 24, 157, DateTimeKind.Local).AddTicks(927));
        }
    }
}
