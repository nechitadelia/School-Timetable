using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School_Timetable.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfa02fd6-a972-4931-b0b1-7faaf525e87f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3b1ba80-9c68-4650-9afc-6143ecdec240");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "SchoolSubjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "SchoolClasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Professors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ClassProfessors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bf1f97f7-735d-4826-9eab-481bf3b892d4", null, "User", "USER" },
                    { "eaf85d92-c0a9-4faf-9235-66c80c0b88ab", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubjects_AppUserId",
                table: "SchoolSubjects",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_AppUserId",
                table: "SchoolClasses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_AppUserId",
                table: "Professors",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassProfessors_AppUserId",
                table: "ClassProfessors",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassProfessors_AspNetUsers_AppUserId",
                table: "ClassProfessors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_AspNetUsers_AppUserId",
                table: "Professors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClasses_AspNetUsers_AppUserId",
                table: "SchoolClasses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSubjects_AspNetUsers_AppUserId",
                table: "SchoolSubjects",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassProfessors_AspNetUsers_AppUserId",
                table: "ClassProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_Professors_AspNetUsers_AppUserId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClasses_AspNetUsers_AppUserId",
                table: "SchoolClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSubjects_AspNetUsers_AppUserId",
                table: "SchoolSubjects");

            migrationBuilder.DropIndex(
                name: "IX_SchoolSubjects_AppUserId",
                table: "SchoolSubjects");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_AppUserId",
                table: "SchoolClasses");

            migrationBuilder.DropIndex(
                name: "IX_Professors_AppUserId",
                table: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_ClassProfessors_AppUserId",
                table: "ClassProfessors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf1f97f7-735d-4826-9eab-481bf3b892d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eaf85d92-c0a9-4faf-9235-66c80c0b88ab");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "SchoolSubjects");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ClassProfessors");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cfa02fd6-a972-4931-b0b1-7faaf525e87f", null, "Admin", "ADMIN" },
                    { "d3b1ba80-9c68-4650-9afc-6143ecdec240", null, "User", "USER" }
                });
        }
    }
}
