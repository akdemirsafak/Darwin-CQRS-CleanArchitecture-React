using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Darwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class asdas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodMusic_Musics_MusicsId",
                table: "MoodMusic");

            migrationBuilder.RenameColumn(
                name: "MusicsId",
                table: "MoodMusic",
                newName: "MusicId");

            migrationBuilder.RenameIndex(
                name: "IX_MoodMusic_MusicsId",
                table: "MoodMusic",
                newName: "IX_MoodMusic_MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodMusic_Musics_MusicId",
                table: "MoodMusic",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodMusic_Musics_MusicId",
                table: "MoodMusic");

            migrationBuilder.RenameColumn(
                name: "MusicId",
                table: "MoodMusic",
                newName: "MusicsId");

            migrationBuilder.RenameIndex(
                name: "IX_MoodMusic_MusicId",
                table: "MoodMusic",
                newName: "IX_MoodMusic_MusicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodMusic_Musics_MusicsId",
                table: "MoodMusic",
                column: "MusicsId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
