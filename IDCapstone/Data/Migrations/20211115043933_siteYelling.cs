using Microsoft.EntityFrameworkCore.Migrations;

namespace IDCapstone.Data.Migrations
{
    public partial class siteYelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerVideos_Players_PlayerId",
                table: "PlayerVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerVideos_Videos_VideoId",
                table: "PlayerVideos");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "PlayerVideos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "PlayerVideos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerVideos_Players_PlayerId",
                table: "PlayerVideos",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerVideos_Videos_VideoId",
                table: "PlayerVideos",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerVideos_Players_PlayerId",
                table: "PlayerVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerVideos_Videos_VideoId",
                table: "PlayerVideos");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "PlayerVideos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "PlayerVideos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerVideos_Players_PlayerId",
                table: "PlayerVideos",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerVideos_Videos_VideoId",
                table: "PlayerVideos",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
