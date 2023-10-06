using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnetoOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicCategory");

            migrationBuilder.DropTable(
                name: "MusicMood");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Musics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MoodId",
                table: "Musics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Musics_CategoryId",
                table: "Musics",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_MoodId",
                table: "Musics",
                column: "MoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Categories_CategoryId",
                table: "Musics",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Moods_MoodId",
                table: "Musics",
                column: "MoodId",
                principalTable: "Moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Categories_CategoryId",
                table: "Musics");

            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Moods_MoodId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_CategoryId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_MoodId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "MoodId",
                table: "Musics");

            migrationBuilder.CreateTable(
                name: "MusicCategory",
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
                    table.PrimaryKey("PK_MusicCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicCategory_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicMood",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true),
                    MoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicMood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicMood_Moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicMood_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicCategory_CategoryId",
                table: "MusicCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicCategory_MusicId",
                table: "MusicCategory",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicMood_MoodId",
                table: "MusicMood",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicMood_MusicId",
                table: "MusicMood",
                column: "MusicId");
        }
    }
}
