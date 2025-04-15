using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorrorMovieBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewModelRatingAg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Reviews",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Reviews",
                newName: "rating");
        }
    }
}
