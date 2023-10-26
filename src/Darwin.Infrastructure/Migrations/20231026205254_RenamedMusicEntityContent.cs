using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedMusicEntityContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMusic");

            migrationBuilder.DropTable(
                name: "MoodMusic");

            migrationBuilder.DropTable(
                name: "MusicPlayList");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Lyrics = table.Column<string>(type: "text", nullable: true),
                    IsUsable = table.Column<bool>(type: "boolean", nullable: false),
                    AgeRateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_AgeRates_AgeRateId",
                        column: x => x.AgeRateId,
                        principalTable: "AgeRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryContent",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryContent", x => new { x.CategoriesId, x.ContentsId });
                    table.ForeignKey(
                        name: "FK_CategoryContent_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryContent_Contents_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentMood",
                columns: table => new
                {
                    ContentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoodsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentMood", x => new { x.ContentsId, x.MoodsId });
                    table.ForeignKey(
                        name: "FK_ContentMood_Contents_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentMood_Moods_MoodsId",
                        column: x => x.MoodsId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentPlayList",
                columns: table => new
                {
                    ContentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayListsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPlayList", x => new { x.ContentsId, x.PlayListsId });
                    table.ForeignKey(
                        name: "FK_ContentPlayList_Contents_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentPlayList_PlayLists_PlayListsId",
                        column: x => x.PlayListsId,
                        principalTable: "PlayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryContent_ContentsId",
                table: "CategoryContent",
                column: "ContentsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMood_MoodsId",
                table: "ContentMood",
                column: "MoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPlayList_PlayListsId",
                table: "ContentPlayList",
                column: "PlayListsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_AgeRateId",
                table: "Contents",
                column: "AgeRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryContent");

            migrationBuilder.DropTable(
                name: "ContentMood");

            migrationBuilder.DropTable(
                name: "ContentPlayList");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgeRateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    IsUsable = table.Column<bool>(type: "boolean", nullable: false),
                    Lyrics = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musics_AgeRates_AgeRateId",
                        column: x => x.AgeRateId,
                        principalTable: "AgeRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    MusicsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodMusic", x => new { x.MoodsId, x.MusicsId });
                    table.ForeignKey(
                        name: "FK_MoodMusic_Moods_MoodsId",
                        column: x => x.MoodsId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoodMusic_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicPlayList",
                columns: table => new
                {
                    MusicsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayListsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlayList", x => new { x.MusicsId, x.PlayListsId });
                    table.ForeignKey(
                        name: "FK_MusicPlayList_Musics_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPlayList_PlayLists_PlayListsId",
                        column: x => x.PlayListsId,
                        principalTable: "PlayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMusic_MusicsId",
                table: "CategoryMusic",
                column: "MusicsId");

            migrationBuilder.CreateIndex(
                name: "IX_MoodMusic_MusicsId",
                table: "MoodMusic",
                column: "MusicsId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlayList_PlayListsId",
                table: "MusicPlayList",
                column: "PlayListsId");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_AgeRateId",
                table: "Musics",
                column: "AgeRateId");
        }
    }
}
