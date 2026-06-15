using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "CourseDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lecturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDetails_LecturerId",
                table: "CourseDetails",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDetails_Lecturer_LecturerId",
                table: "CourseDetails",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDetails_Lecturer_LecturerId",
                table: "CourseDetails");

            migrationBuilder.DropTable(
                name: "Lecturer");

            migrationBuilder.DropIndex(
                name: "IX_CourseDetails_LecturerId",
                table: "CourseDetails");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "CourseDetails");
        }
    }
}
