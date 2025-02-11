using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToDegree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "progress",
                table: "courses",
                newName: "status");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "degrees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "degrees");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "courses",
                newName: "progress");
        }
    }
}
