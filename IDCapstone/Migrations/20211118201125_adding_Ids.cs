using Microsoft.EntityFrameworkCore.Migrations;

namespace IDCapstone.Migrations
{
    public partial class adding_Ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Videos_VideoId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Videos_VideoId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Tags",
                newName: "videoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_VideoId",
                table: "Tags",
                newName: "IX_Tags_videoId");

            migrationBuilder.AlterColumn<int>(
                name: "videoId",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Videos_VideoId",
                table: "Ratings",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Videos_videoId",
                table: "Tags",
                column: "videoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Videos_VideoId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Videos_videoId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "videoId",
                table: "Tags",
                newName: "VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_videoId",
                table: "Tags",
                newName: "IX_Tags_VideoId");

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Tags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VideoId",
                table: "Ratings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Videos_VideoId",
                table: "Ratings",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Videos_VideoId",
                table: "Tags",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
