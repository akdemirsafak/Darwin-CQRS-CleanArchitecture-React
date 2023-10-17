using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAgeRateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgeRateId",
                table: "Musics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AgeRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musics_AgeRateId",
                table: "Musics",
                column: "AgeRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_AgeRates_AgeRateId",
                table: "Musics",
                column: "AgeRateId",
                principalTable: "AgeRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_AgeRates_AgeRateId",
                table: "Musics");

            migrationBuilder.DropTable(
                name: "AgeRates");

            migrationBuilder.DropIndex(
                name: "IX_Musics_AgeRateId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "AgeRateId",
                table: "Musics");
        }
    }
}
