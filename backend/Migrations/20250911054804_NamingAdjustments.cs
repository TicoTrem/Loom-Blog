using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class NamingAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Authors_authorId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "lastUpdatedDate",
                table: "BlogPosts",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "createdDate",
                table: "BlogPosts",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "BlogPosts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "BlogPosts",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_authorId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Authors_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Authors_AuthorId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "BlogPosts",
                newName: "lastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "BlogPosts",
                newName: "createdDate");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogPosts",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "BlogPosts",
                newName: "authorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Authors_authorId",
                table: "BlogPosts",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
