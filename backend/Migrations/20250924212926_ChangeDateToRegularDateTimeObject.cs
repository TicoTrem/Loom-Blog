using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateToRegularDateTimeObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "BlogPosts",
                newName: "LastUpdatedDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "BlogPosts",
                newName: "CreatedDateUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedDateUtc",
                table: "BlogPosts",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDateUtc",
                table: "BlogPosts",
                newName: "CreatedDate");
        }
    }
}
