using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "prerequisites",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "degree_courses",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "courses",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "assignments",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "assignment_categories",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_prerequisites_user_id",
                table: "prerequisites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_degree_courses_user_id",
                table: "degree_courses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_courses_user_id",
                table: "courses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_user_id",
                table: "assignments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_categories_user_id",
                table: "assignment_categories",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignment_categories_users_user_id",
                table: "assignment_categories",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_users_user_id",
                table: "assignments",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_courses_users_user_id",
                table: "courses",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_degree_courses_users_user_id",
                table: "degree_courses",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_prerequisites_users_user_id",
                table: "prerequisites",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignment_categories_users_user_id",
                table: "assignment_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_assignments_users_user_id",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "fk_courses_users_user_id",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "fk_degree_courses_users_user_id",
                table: "degree_courses");

            migrationBuilder.DropForeignKey(
                name: "fk_prerequisites_users_user_id",
                table: "prerequisites");

            migrationBuilder.DropIndex(
                name: "ix_prerequisites_user_id",
                table: "prerequisites");

            migrationBuilder.DropIndex(
                name: "ix_degree_courses_user_id",
                table: "degree_courses");

            migrationBuilder.DropIndex(
                name: "ix_courses_user_id",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "ix_assignments_user_id",
                table: "assignments");

            migrationBuilder.DropIndex(
                name: "ix_assignment_categories_user_id",
                table: "assignment_categories");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "prerequisites");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "degree_courses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "assignments");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "assignment_categories");
        }
    }
}
