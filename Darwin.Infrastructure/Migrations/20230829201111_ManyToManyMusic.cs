using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Musics",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MusicCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true)
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
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<long>(type: "bigint", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicCategories");

            migrationBuilder.DropTable(
                name: "MusicMoods");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Musics",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
