using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLSV.Migrations
{
    public partial class addfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdHocSinh = table.Column<int>(type: "int", nullable: false),
                    IdGiaoVien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_GiaoVien_IdGiaoVien",
                        column: x => x.IdGiaoVien,
                        principalTable: "GiaoVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_File_HocSinh_IdHocSinh",
                        column: x => x.IdHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHocSinh = table.Column<int>(type: "int", nullable: false),
                    IdGiaoVien = table.Column<int>(type: "int", nullable: false),
                    thoiGian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichHoc_GiaoVien_IdGiaoVien",
                        column: x => x.IdGiaoVien,
                        principalTable: "GiaoVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LichHoc_HocSinh_IdHocSinh",
                        column: x => x.IdHocSinh,
                        principalTable: "HocSinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_IdGiaoVien",
                table: "File",
                column: "IdGiaoVien");

            migrationBuilder.CreateIndex(
                name: "IX_File_IdHocSinh",
                table: "File",
                column: "IdHocSinh");

            migrationBuilder.CreateIndex(
                name: "IX_LichHoc_IdGiaoVien",
                table: "LichHoc",
                column: "IdGiaoVien");

            migrationBuilder.CreateIndex(
                name: "IX_LichHoc_IdHocSinh",
                table: "LichHoc",
                column: "IdHocSinh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "LichHoc");
        }
    }
}
