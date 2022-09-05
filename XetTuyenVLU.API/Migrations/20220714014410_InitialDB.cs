using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BangDiemTHPT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoSoTHPT = table.Column<long>(type: "bigint", nullable: true),
                    MaHocKy_Lop = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Toan = table.Column<double>(type: "float", nullable: true),
                    Van = table.Column<double>(type: "float", nullable: true),
                    Anh = table.Column<double>(type: "float", nullable: true),
                    Phap = table.Column<double>(type: "float", nullable: true),
                    Ly = table.Column<double>(type: "float", nullable: true),
                    Hoa = table.Column<double>(type: "float", nullable: true),
                    Sinh = table.Column<double>(type: "float", nullable: true),
                    Su = table.Column<double>(type: "float", nullable: true),
                    Dia = table.Column<double>(type: "float", nullable: true),
                    GDCD = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangDiemTHPT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BoSungHocBa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoSungHocBa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChungChiNN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNN = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ChungChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DiemQuiDoi = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChungChiNN", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DanToc",
                columns: table => new
                {
                    MA_DANTOC = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TEN_DANTOC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DIEN_GIAI = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DieuChinhThongTin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieuChinhThongTin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DotThu = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Khoa = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dot", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HoSoTHPT",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    MaNoiSinh = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenNoiSinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaDanToc = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenDanToc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaTonGiao = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenTonGiao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CMND = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    QuocTich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HoKhau = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    HoKhau_MaPhuong = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    HoKhau_TenPhuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HoKhau_MaTinhTP = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    HoKhau_TenTinhTP = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    HoKhau_MaQH = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    HoKhau_TenQH = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    NamTotNghiep = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true),
                    SoBaoDanh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    HocLucLop12 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HanhKiemLop12 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LoaiHinhTN = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true),
                    TruongTHPT_MaTinhTP = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TruongTHPT_TenTinhTP = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TruongTHPT_MaQH = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TruongTHPT_TenQH = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    TenTruongTHPT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaTruongTHPT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TenLop12 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KhuVuc = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DoiTuongUuTien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MaCcnn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCNN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaNganh_ToHop1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TenNganh_TenToHop1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChuongTrinhHoc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNganh_ToHop2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TenNganh_TenToHop2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChuongTrinhHoc2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNganh_ToHop3 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TenNganh_TenToHop3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChuongTrinhHoc3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LienLac_DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LienLac_MaPhuongXa = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LienLac_TenPhuongXa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LienLac_MaTP = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LienLac_TenTP = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LienLac_MaQH = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LienLac_TenQH = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DienThoaiDD = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DienThoaiPhuHuynh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateInserted = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEdited = table.Column<DateTime>(type: "datetime", nullable: true),
                    DaNhanHoSo = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('N')"),
                    DiemVeMyThuat = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DiemVeNangKhieu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoTHPT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Nganh",
                columns: table => new
                {
                    ID = table.Column<double>(type: "float", nullable: true),
                    MANGANH_TOHOP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MA_NGANH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TEN_NGANH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MA_TOHOP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TEN_TOHOP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CTTC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CTDB = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CTTT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FLAG = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "QuocTich",
                columns: table => new
                {
                    MaQT = table.Column<double>(type: "float", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    TenQT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblQuocTich", x => x.MaQT);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MaVaiTro = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoTrungTuyen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDot = table.Column<int>(type: "int", nullable: false),
                    HanNopThuTuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    HanNopHocPhi = table.Column<DateTime>(type: "datetime", nullable: false),
                    SoTienHocPhi = table.Column<double>(type: "float", nullable: false),
                    HanNhapHoc = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoTrungTuyen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TinhTP",
                columns: table => new
                {
                    MA_TINHTP = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TEN_TINHTP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TonGiao",
                columns: table => new
                {
                    MA_TONGIAO = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TEN_TONGIAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DIENGIAI = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TP_QH_PX",
                columns: table => new
                {
                    TenTinhTP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaTinhTP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TenQH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaQH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TenPX = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaPX = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TruongTHPT",
                columns: table => new
                {
                    ID = table.Column<double>(type: "float", nullable: true),
                    MA_TPTRUONG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MA_TINHTP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    TEN_TINHTP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MA_QH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TEN_QH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MA_TRUONG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TEN_TRUONG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DIA_CHI = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KHU_VUC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LOAI_TRUONG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangDiemTHPT");

            migrationBuilder.DropTable(
                name: "BoSungHocBa");

            migrationBuilder.DropTable(
                name: "ChungChiNN");

            migrationBuilder.DropTable(
                name: "DanToc");

            migrationBuilder.DropTable(
                name: "DieuChinhThongTin");

            migrationBuilder.DropTable(
                name: "Dot");

            migrationBuilder.DropTable(
                name: "HoSoTHPT");

            migrationBuilder.DropTable(
                name: "Nganh");

            migrationBuilder.DropTable(
                name: "QuocTich");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "ThongBaoTrungTuyen");

            migrationBuilder.DropTable(
                name: "TinhTP");

            migrationBuilder.DropTable(
                name: "TonGiao");

            migrationBuilder.DropTable(
                name: "TP_QH_PX");

            migrationBuilder.DropTable(
                name: "TruongTHPT");
        }
    }
}
