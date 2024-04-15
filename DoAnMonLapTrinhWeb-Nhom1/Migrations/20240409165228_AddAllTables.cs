using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnMonLapTrinhWeb_Nhom1.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    IDChucVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChucVu__70FCCF65DD36D787", x => x.IDChucVu);
                });

            migrationBuilder.CreateTable(
                name: "DiaDiem",
                columns: table => new
                {
                    MaDiaDiem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiaDiem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiaDiem__F015962A8780C2E6", x => x.MaDiaDiem);
                });

            migrationBuilder.CreateTable(
                name: "HangXe",
                columns: table => new
                {
                    MaHangXe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HangXe__8C6DD0A7F001ABFF", x => x.MaHangXe);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    MaiKhuyenMai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhuyenMai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NoiDungKhuyenMai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayKhuyenMai = table.Column<DateTime>(type: "datetime", nullable: false),
                    PhanTramKhhuyenMai = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhuyenMa__BD611B46F103F3FB", x => x.MaiKhuyenMai);
                });

            migrationBuilder.CreateTable(
                name: "LoaiXe",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiXe__730A575939795964", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "MailTuDong",
                columns: table => new
                {
                    MaMail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MailTuDo__0DB26639A62061F8", x => x.MaMail);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    MaPhuongThuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhuongThuc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhuongTh__35F7404EFCC16D3F", x => x.MaPhuongThuc);
                });

            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    IDQuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quyen__B3A2827E136F0562", x => x.IDQuyen);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    SDT = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    GPLX = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    Avarta = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IDChucVu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaiKhoan__A9D1053552AD3104", x => x.Email);
                    table.ForeignKey(
                        name: "FK__TaiKhoan__IDChuc__76969D2E",
                        column: x => x.IDChucVu,
                        principalTable: "ChucVu",
                        principalColumn: "IDChucVu");
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    IDChucVu = table.Column<int>(type: "int", nullable: false),
                    IDQuyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhanQuye__8BC6E742D5E035C7", x => new { x.IDChucVu, x.IDQuyen });
                    table.ForeignKey(
                        name: "FK__PhanQuyen__IDChu__778AC167",
                        column: x => x.IDChucVu,
                        principalTable: "ChucVu",
                        principalColumn: "IDChucVu");
                    table.ForeignKey(
                        name: "FK__PhanQuyen__IDQuy__787EE5A0",
                        column: x => x.IDQuyen,
                        principalTable: "Quyen",
                        principalColumn: "IDQuyen");
                });

            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    BienSoXe = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MaDiaDiem = table.Column<int>(type: "int", nullable: false),
                    MaHangXe = table.Column<int>(type: "int", nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: false),
                    TenXe = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NamSanXuat = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    GiaThue = table.Column<decimal>(type: "money", nullable: false),
                    NhienLieu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaLuc_PhanKhoi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HinhAnh1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Xe__A7805993CF3E80A0", x => x.BienSoXe);
                    table.ForeignKey(
                        name: "FK__Xe__Email__6D0D32F4",
                        column: x => x.Email,
                        principalTable: "TaiKhoan",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK__Xe__MaDiaDiem__72C60C4A",
                        column: x => x.MaDiaDiem,
                        principalTable: "DiaDiem",
                        principalColumn: "MaDiaDiem");
                    table.ForeignKey(
                        name: "FK__Xe__MaHangXe__74AE54BC",
                        column: x => x.MaHangXe,
                        principalTable: "HangXe",
                        principalColumn: "MaHangXe");
                    table.ForeignKey(
                        name: "FK__Xe__MaLoai__71D1E811",
                        column: x => x.MaLoai,
                        principalTable: "LoaiXe",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    BienSoXe = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SoSao = table.Column<int>(type: "int", nullable: true),
                    NhanXet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhGia__ED1D49C0256A5A2A", x => new { x.BienSoXe, x.Email });
                    table.ForeignKey(
                        name: "FK__DanhGia__BienSoX__70DDC3D8",
                        column: x => x.BienSoXe,
                        principalTable: "Xe",
                        principalColumn: "BienSoXe");
                    table.ForeignKey(
                        name: "FK__DanhGia__Email__6E01572D",
                        column: x => x.Email,
                        principalTable: "TaiKhoan",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "YeuCauDatXe",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BienSoXe = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime", nullable: false),
                    SoNgayThue = table.Column<int>(type: "int", nullable: false),
                    TrangThaiChapNhan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__YeuCauDa__83A900AC0247BA4F", x => new { x.Email, x.BienSoXe });
                    table.ForeignKey(
                        name: "FK__YeuCauDat__BienS__6FE99F9F",
                        column: x => x.BienSoXe,
                        principalTable: "Xe",
                        principalColumn: "BienSoXe");
                    table.ForeignKey(
                        name: "FK__YeuCauDat__Email__6C190EBB",
                        column: x => x.Email,
                        principalTable: "TaiKhoan",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayLap = table.Column<DateTime>(type: "datetime", nullable: false),
                    TongDonGia = table.Column<decimal>(type: "money", nullable: false),
                    MaPhuongThuc = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BienSoXe = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__835ED13B8B789FF1", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK__HoaDon__75A278F5",
                        columns: x => new { x.Email, x.BienSoXe },
                        principalTable: "YeuCauDatXe",
                        principalColumns: new[] { "Email", "BienSoXe" });
                    table.ForeignKey(
                        name: "FK__HoaDon__MaPhuong__73BA3083",
                        column: x => x.MaPhuongThuc,
                        principalTable: "PhuongThucThanhToan",
                        principalColumn: "MaPhuongThuc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_Email",
                table: "DanhGia",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Email_BienSoXe",
                table: "HoaDon",
                columns: new[] { "Email", "BienSoXe" });

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaPhuongThuc",
                table: "HoaDon",
                column: "MaPhuongThuc");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_IDQuyen",
                table: "PhanQuyen",
                column: "IDQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_IDChucVu",
                table: "TaiKhoan",
                column: "IDChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_Email",
                table: "Xe",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_MaDiaDiem",
                table: "Xe",
                column: "MaDiaDiem");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_MaHangXe",
                table: "Xe",
                column: "MaHangXe");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_MaLoai",
                table: "Xe",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauDatXe_BienSoXe",
                table: "YeuCauDatXe",
                column: "BienSoXe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "MailTuDong");

            migrationBuilder.DropTable(
                name: "PhanQuyen");

            migrationBuilder.DropTable(
                name: "YeuCauDatXe");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "Quyen");

            migrationBuilder.DropTable(
                name: "Xe");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "DiaDiem");

            migrationBuilder.DropTable(
                name: "HangXe");

            migrationBuilder.DropTable(
                name: "LoaiXe");

            migrationBuilder.DropTable(
                name: "ChucVu");
        }
    }
}
