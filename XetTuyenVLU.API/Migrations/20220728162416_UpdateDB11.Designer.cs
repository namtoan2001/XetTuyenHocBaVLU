﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XetTuyenVLU.Models;

#nullable disable

namespace XetTuyenVLU.Migrations
{
    [DbContext(typeof(XetTuyenVLUContext))]
    [Migration("20220728162416_UpdateDB11")]
    partial class UpdateDB11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("XetTuyenVLU.Models.BangDiemThpt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double?>("Anh")
                        .HasColumnType("float");

                    b.Property<double?>("Dia")
                        .HasColumnType("float");

                    b.Property<double?>("Gdcd")
                        .HasColumnType("float")
                        .HasColumnName("GDCD");

                    b.Property<double?>("Hoa")
                        .HasColumnType("float");

                    b.Property<double?>("Ly")
                        .HasColumnType("float");

                    b.Property<long?>("MaHoSoThpt")
                        .HasColumnType("bigint")
                        .HasColumnName("MaHoSoTHPT");

                    b.Property<string>("MaHocKyLop")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("MaHocKy_Lop");

                    b.Property<double?>("Phap")
                        .HasColumnType("float");

                    b.Property<double?>("Sinh")
                        .HasColumnType("float");

                    b.Property<double?>("Su")
                        .HasColumnType("float");

                    b.Property<double?>("Toan")
                        .HasColumnType("float");

                    b.Property<double?>("Van")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("BangDiemTHPT", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.ChungChiNn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ChungChi")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double?>("DiemQuiDoi")
                        .HasColumnType("float");

                    b.Property<string>("MaNn")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("MaNN");

                    b.HasKey("Id");

                    b.ToTable("ChungChiNN", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.DanToc", b =>
                {
                    b.Property<string>("MaDantoc")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("MA_DANTOC");

                    b.Property<string>("DienGiai")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("DIEN_GIAI");

                    b.Property<string>("TenDantoc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_DANTOC");

                    b.HasKey("MaDantoc");

                    b.ToTable("DanToc", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.Dot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("DotThu")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("Khoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Dot");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.HoSoThpt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Ccnn")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("CCNN");

                    b.Property<string>("ChuongTrinhHoc1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChuongTrinhHoc2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChuongTrinhHoc3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cmnd")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMND");

                    b.Property<string>("DaNhanHoSo")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasDefaultValueSql("('N')")
                        .IsFixedLength();

                    b.Property<DateTime?>("DateEdited")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateInserted")
                        .HasColumnType("datetime");

                    b.Property<string>("DiemVeMyThuat")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("DiemVeNangKhieu")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("DienThoaiDd")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("DienThoaiDD");

                    b.Property<string>("DienThoaiPhuHuynh")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DoiTuongUuTien")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DotId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool?>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HanhKiemLop12")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("HoKhau")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("HoKhauMaPhuong")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("HoKhau_MaPhuong");

                    b.Property<string>("HoKhauMaQh")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("HoKhau_MaQH");

                    b.Property<string>("HoKhauMaTinhTp")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("HoKhau_MaTinhTP");

                    b.Property<string>("HoKhauTenPhuong")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("HoKhau_TenPhuong");

                    b.Property<string>("HoKhauTenQh")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("HoKhau_TenQH");

                    b.Property<string>("HoKhauTenTinhTp")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("HoKhau_TenTinhTP");

                    b.Property<string>("HoVaTen")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HocLucLop12")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("KhuVuc")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("LienLacDiaChi")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("LienLac_DiaChi");

                    b.Property<string>("LienLacMaPhuongXa")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("LienLac_MaPhuongXa");

                    b.Property<string>("LienLacMaQh")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("LienLac_MaQH");

                    b.Property<string>("LienLacMaTp")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("LienLac_MaTP");

                    b.Property<string>("LienLacTenPhuongXa")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LienLac_TenPhuongXa");

                    b.Property<string>("LienLacTenQh")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("LienLac_TenQH");

                    b.Property<string>("LienLacTenTp")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("LienLac_TenTP");

                    b.Property<string>("LoaiHinhTn")
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)")
                        .HasColumnName("LoaiHinhTN");

                    b.Property<string>("MaCcnn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaDanToc")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("MaNganhToHop1")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("MaNganh_ToHop1");

                    b.Property<string>("MaNganhToHop2")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("MaNganh_ToHop2");

                    b.Property<string>("MaNganhToHop3")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("MaNganh_ToHop3");

                    b.Property<string>("MaNoiSinh")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("MaTonGiao")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("MaTruongThpt")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("MaTruongTHPT");

                    b.Property<string>("NamTotNghiep")
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("date");

                    b.Property<string>("QuocTich")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SoBaoDanh")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenDanToc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenLop12")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenNganhTenToHop1")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("TenNganh_TenToHop1");

                    b.Property<string>("TenNganhTenToHop2")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("TenNganh_TenToHop2");

                    b.Property<string>("TenNganhTenToHop3")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("TenNganh_TenToHop3");

                    b.Property<string>("TenNoiSinh")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenTonGiao")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenTruongThpt")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("TenTruongTHPT");

                    b.Property<string>("TruongThptMaQh")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("TruongTHPT_MaQH");

                    b.Property<string>("TruongThptMaTinhTp")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("TruongTHPT_MaTinhTP");

                    b.Property<string>("TruongThptTenQh")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasColumnName("TruongTHPT_TenQH");

                    b.Property<string>("TruongThptTenTinhTp")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("TruongTHPT_TenTinhTP");

                    b.HasKey("Id");

                    b.ToTable("HoSoTHPT", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.LoaiHinhThuc", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("TenHinhThuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ID");

                    b.ToTable("LoaiHinhThuc");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.LoaiThongBao", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("TenThongBao")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("LoaiThongBao");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.Nganh", b =>
                {
                    b.Property<double>("Id")
                        .HasColumnType("float")
                        .HasColumnName("ID");

                    b.Property<string>("Ctdb")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("CTDB");

                    b.Property<string>("Cttc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("CTTC");

                    b.Property<string>("Cttt")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("CTTT");

                    b.Property<bool?>("Flag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("FLAG")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("MaNganh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MA_NGANH");

                    b.Property<string>("MaTohop")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MA_TOHOP");

                    b.Property<string>("ManganhTohop")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MANGANH_TOHOP");

                    b.Property<string>("TenNganh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_NGANH");

                    b.Property<string>("TenTohop")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_TOHOP");

                    b.HasKey("Id");

                    b.ToTable("Nganh", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.QuocTich", b =>
                {
                    b.Property<double>("MaQt")
                        .HasColumnType("float")
                        .HasColumnName("MaQT");

                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("TenQt")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TenQT");

                    b.HasKey("MaQt")
                        .HasName("PK_tblQuocTich");

                    b.ToTable("QuocTich", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.TaiKhoan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("HoVaTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenVaiTro")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("VaiTroId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.ThoiGian", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("HinhThucId")
                        .HasColumnType("int");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<int>("MaDot")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ThoiGian");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.ThongBao", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoaiThongBaoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ThongBao");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.TinhTp", b =>
                {
                    b.Property<string>("MaTinhtp")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("MA_TINHTP");

                    b.Property<string>("TenTinhtp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_TINHTP");

                    b.HasKey("MaTinhtp");

                    b.ToTable("TinhTP", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.TonGiao", b =>
                {
                    b.Property<string>("MaTongiao")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("MA_TONGIAO");

                    b.Property<string>("Diengiai")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DIENGIAI");

                    b.Property<string>("TenTongiao")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_TONGIAO");

                    b.HasKey("MaTongiao");

                    b.ToTable("TonGiao", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.TpQhPx", b =>
                {
                    b.Property<double>("Id")
                        .HasColumnType("float");

                    b.Property<string>("Cap")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("EnglishName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MaPx")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MaPX");

                    b.Property<string>("MaQh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MaQH");

                    b.Property<string>("MaTinhTp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MaTinhTP");

                    b.Property<string>("TenPx")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TenPX");

                    b.Property<string>("TenQh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TenQH");

                    b.Property<string>("TenTinhTp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TenTinhTP");

                    b.HasKey("Id");

                    b.ToTable("TP_QH_PX", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.TrangThai", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("TenTrangThai")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("TrangThai");
                });

            modelBuilder.Entity("XetTuyenVLU.Models.TruongThpt", b =>
                {
                    b.Property<double>("Id")
                        .HasColumnType("float")
                        .HasColumnName("ID");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("DIA_CHI");

                    b.Property<string>("KhuVuc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("KHU_VUC");

                    b.Property<string>("LoaiTruong")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("LOAI_TRUONG");

                    b.Property<string>("MaQh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("MA_QH");

                    b.Property<string>("MaTinhtp")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("MA_TINHTP");

                    b.Property<string>("MaTptruong")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("MA_TPTRUONG");

                    b.Property<string>("MaTruong")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("MA_TRUONG");

                    b.Property<string>("TenQh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_QH");

                    b.Property<string>("TenTinhtp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_TINHTP");

                    b.Property<string>("TenTruong")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("TEN_TRUONG");

                    b.HasKey("Id");

                    b.ToTable("TruongTHPT", (string)null);
                });

            modelBuilder.Entity("XetTuyenVLU.Models.VaiTro", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("TenVaiTro")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("VaiTro");
                });
#pragma warning restore 612, 618
        }
    }
}
