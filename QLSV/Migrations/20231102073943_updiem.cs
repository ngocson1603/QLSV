using Microsoft.EntityFrameworkCore.Migrations;

namespace QLSV.Migrations
{
    public partial class updiem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiemHocSinh",
                table: "DiemHocSinh");

            migrationBuilder.DropIndex(
                name: "IX_DiemHocSinh_IdHocSinh",
                table: "DiemHocSinh");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DiemHocSinh");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiemHocSinh",
                table: "DiemHocSinh",
                columns: new[] { "IdHocSinh", "IdKhoaHoc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiemHocSinh",
                table: "DiemHocSinh");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DiemHocSinh",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiemHocSinh",
                table: "DiemHocSinh",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DiemHocSinh_IdHocSinh",
                table: "DiemHocSinh",
                column: "IdHocSinh");
        }
    }
}
