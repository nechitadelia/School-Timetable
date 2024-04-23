using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Timetable.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ClassProfessors",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SchoolClassId = table.Column<int>(type: "int", nullable: false),
            //        SubjectName = table.Column<string>(type: "varchar(50)", nullable: false),
            //        ProfessorId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClassProfessors", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SchoolClasses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        YearOfStudy = table.Column<int>(type: "int", nullable: false),
            //        ClassLetter = table.Column<string>(type: "char(1)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SchoolClasses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SchoolSubjects",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(50)", nullable: false),
            //        HoursPerWeek = table.Column<int>(type: "int", nullable: false),
            //        YearOfStudy = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SchoolSubjects", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Professors",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
            //        LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        AssignedHours = table.Column<int>(type: "int", nullable: false),
            //        SchoolSubjectId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Professors", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Professors_SchoolSubjects_SchoolSubjectId",
            //            column: x => x.SchoolSubjectId,
            //            principalTable: "SchoolSubjects",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Professors_SchoolSubjectId",
            //    table: "Professors",
            //    column: "SchoolSubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ClassProfessors");

            //migrationBuilder.DropTable(
            //    name: "Professors");

            //migrationBuilder.DropTable(
            //    name: "SchoolClasses");

            //migrationBuilder.DropTable(
            //    name: "SchoolSubjects");
        }
    }
}
