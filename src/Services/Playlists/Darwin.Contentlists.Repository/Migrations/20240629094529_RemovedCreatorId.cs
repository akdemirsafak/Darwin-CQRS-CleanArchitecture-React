using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Contentlists.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCreatorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Playlists");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 12, 45, 29, 37, DateTimeKind.Local).AddTicks(4560),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 12, 30, 50, 717, DateTimeKind.Local).AddTicks(2771));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 29, 12, 30, 50, 717, DateTimeKind.Local).AddTicks(2771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 29, 12, 45, 29, 37, DateTimeKind.Local).AddTicks(4560));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Playlists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
