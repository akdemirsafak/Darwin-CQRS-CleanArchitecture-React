using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Contentlists.Repository.Migrations
{
    /// <inheritdoc />
    public partial class HasQueryIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 12, 54, 27, 914, DateTimeKind.Local).AddTicks(5546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 12, 45, 29, 37, DateTimeKind.Local).AddTicks(4560));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 12, 45, 29, 37, DateTimeKind.Local).AddTicks(4560),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 12, 54, 27, 914, DateTimeKind.Local).AddTicks(5546));
        }
    }
}
