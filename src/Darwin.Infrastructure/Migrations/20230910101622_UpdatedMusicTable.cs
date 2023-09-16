using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMusicTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicCategories");

            migrationBuilder.DropTable(
                name: "MusicMoods");

            migrationBuilder.CreateTable(
                name: "CategoryMusic",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMusic", x => new { x.CategoriesId, x.MusicsId });
                    table.ForeignKey(
                        name: "FK_CategoryMusic_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMusic_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoodMusic",
                columns: table => new
                {
                    MoodsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodMusic", x => new { x.MoodsId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_MoodMusic_Moods_MoodsId",
                        column: x => x.MoodsId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoodMusic_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMusic_MusicsId",
                table: "CategoryMusic",
                column: "MusicsId");

            migrationBuilder.CreateIndex(
                name: "IX_MoodMusic_MusicId",
                table: "MoodMusic",
                column: "MusicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMusic");

            migrationBuilder.DropTable(
                name: "MoodMusic");

            migrationBuilder.CreateTable(
                name: "MusicCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicCategories_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicMoods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicMoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicMoods_Moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicMoods_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicCategories_CategoryId",
                table: "MusicCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicCategories_MusicId",
                table: "MusicCategories",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicMoods_MoodId",
                table: "MusicMoods",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicMoods_MusicId",
                table: "MusicMoods",
                column: "MusicId");
        }
    }
}
