using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AfterIAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PlayLists");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PlayLists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PlayLists");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AspNetUsers",
                newName: "CreatedOnUtc");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "PlayLists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                table: "PlayLists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "Moods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                table: "Moods",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "Contents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                table: "Contents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "PlayLists");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                table: "PlayLists");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                table: "Categories");




            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "AspNetUsers",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "PlayLists",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedAt",
                table: "PlayLists",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "PlayLists",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "Moods",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedAt",
                table: "Moods",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "Moods",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "Contents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedAt",
                table: "Contents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "Contents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeletedAt",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "Categories",
                type: "bigint",
                nullable: true);
        }
    }
}
