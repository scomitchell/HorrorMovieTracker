using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorrorMovieBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewModelRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rating",
                table: "Reviews");
        }
    }
}
