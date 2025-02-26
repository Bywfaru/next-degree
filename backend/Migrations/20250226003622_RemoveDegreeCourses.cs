using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegreePlanner.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDegreeCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "degree_courses");

            migrationBuilder.AddColumn<string>(
                name: "degree_id",
                table: "courses",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_courses_degree_id",
                table: "courses",
                column: "degree_id");

            migrationBuilder.AddForeignKey(
                name: "fk_courses_degrees_degree_id",
                table: "courses",
                column: "degree_id",
                principalTable: "degrees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_courses_degrees_degree_id",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "ix_courses_degree_id",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "degree_id",
                table: "courses");

            migrationBuilder.CreateTable(
                name: "degree_courses",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    course_id = table.Column<string>(type: "text", nullable: false),
                    degree_id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_degree_courses", x => x.id);
                    table.ForeignKey(
                        name: "fk_degree_courses_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_degree_courses_degrees_degree_id",
                        column: x => x.degree_id,
                        principalTable: "degrees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_degree_courses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_degree_courses_course_id",
                table: "degree_courses",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_degree_courses_degree_id",
                table: "degree_courses",
                column: "degree_id");

            migrationBuilder.CreateIndex(
                name: "ix_degree_courses_user_id",
                table: "degree_courses",
                column: "user_id");
        }
    }
}
