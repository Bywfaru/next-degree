using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCourseCurrentGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_grade",
                table: "courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "current_grade",
                table: "courses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
