using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImportExcelUsingQuartz.Migrations
{
    /// <inheritdoc />
    public partial class init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stuIdTemp",
                table: "StudentList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("SET IDENTITY_INSERT StudentList ON");
            migrationBuilder.Sql("UPDATE StudentList SET stuIdTemp = stuId");
            migrationBuilder.Sql("SET IDENTITY_INSERT StudentList OFF");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentList",
                table: "StudentList");

            migrationBuilder.DropColumn(
                name: "stuId",
                table: "StudentList");

            migrationBuilder.RenameColumn(
                name: "stuIdTemp",
                table: "StudentList",
                newName: "stuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentList",
                table: "StudentList",
                column: "stuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentList",
                table: "StudentList");

            migrationBuilder.RenameColumn(
                name: "stuId",
                table: "StudentList",
                newName: "stuIdTemp");

            migrationBuilder.AddColumn<int>(
                name: "stuId",
                table: "StudentList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("SET IDENTITY_INSERT StudentList ON");
            migrationBuilder.Sql("UPDATE StudentList SET stuId = stuIdTemp");
            migrationBuilder.Sql("SET IDENTITY_INSERT StudentList OFF");

            migrationBuilder.DropColumn(
                name: "stuIdTemp",
                table: "StudentList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentList",
                table: "StudentList",
                column: "stuId");
        }
    }
}