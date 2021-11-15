using Microsoft.EntityFrameworkCore.Migrations;

namespace IDCapstone.Data.Migrations
{
    public partial class sitewantsmetoaddmigrationIdontknowwhy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersListId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersList", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UsersListId",
                table: "AspNetUsers",
                column: "UsersListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsersList_UsersListId",
                table: "AspNetUsers",
                column: "UsersListId",
                principalTable: "UsersList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsersList_UsersListId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UsersList");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UsersListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsersListId",
                table: "AspNetUsers");
        }
    }
}
