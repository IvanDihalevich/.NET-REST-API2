using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_user");

            migrationBuilder.RenameColumn(
                name: "course_id",
                table: "courses",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_at",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "courses",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "max_students",
                table: "courses",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "finish_at",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('utc', now())");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "courses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_course",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<string>(type: "varchar(255)", nullable: false),
                    join_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_joined = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_course_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_course_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_courses_user_id",
                table: "courses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_course_course_id",
                table: "user_course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_course_user_id",
                table: "user_course",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_courses_users_user_id",
                table: "courses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_courses_users_user_id",
                table: "courses");

            migrationBuilder.DropTable(
                name: "user_course");

            migrationBuilder.DropIndex(
                name: "ix_courses_user_id",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "courses",
                newName: "course_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_at",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "timezone('utc', now())");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "courses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<int>(
                name: "max_students",
                table: "courses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "finish_at",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "timezone('utc', now())");

            migrationBuilder.CreateTable(
                name: "course_user",
                columns: table => new
                {
                    courses_course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    students_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_user", x => new { x.courses_course_id, x.students_id });
                    table.ForeignKey(
                        name: "fk_course_user_courses_courses_course_id",
                        column: x => x.courses_course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_course_user_users_students_id",
                        column: x => x.students_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_user_students_id",
                table: "course_user",
                column: "students_id");
        }
    }
}
