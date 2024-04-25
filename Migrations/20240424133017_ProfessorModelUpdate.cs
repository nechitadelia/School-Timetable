using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School_Timetable.Migrations
{
    /// <inheritdoc />
    public partial class ProfessorModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e484a4f-f9c6-46c0-8bd3-005ed37ffabb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66d901d1-82ec-4066-9860-67621dd1364a");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Professors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Professors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            //migrationBuilder.AddColumn<int>(
            //    name: "MaxHours",
            //    table: "Professors",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a6a2689-d483-4a32-aca3-95266c321e8f", null, "User", "USER" },
                    { "82f2ad71-affc-48ec-8ca6-bc6030bf6687", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a6a2689-d483-4a32-aca3-95266c321e8f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f2ad71-affc-48ec-8ca6-bc6030bf6687");

            //migrationBuilder.DropColumn(
            //    name: "MaxHours",
            //    table: "Professors");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Professors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Professors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e484a4f-f9c6-46c0-8bd3-005ed37ffabb", null, "Admin", "ADMIN" },
                    { "66d901d1-82ec-4066-9860-67621dd1364a", null, "User", "USER" }
                });
        }
    }
}
