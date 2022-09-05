using Microsoft.EntityFrameworkCore;

namespace XetTuyenVLU.Models
{
    public partial class XetTuyenVLUContext : DbContext
    {
        public XetTuyenVLUContext()
        {
        }

        public XetTuyenVLUContext(DbContextOptions<XetTuyenVLUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BangDiemThpt> BangDiemThpts { get; set; } = null!;
        public virtual DbSet<ChungChiNn> ChungChiNns { get; set; } = null!;
        public virtual DbSet<DanToc> DanTocs { get; set; } = null!;
        public virtual DbSet<HoSoThpt> HoSoThpts { get; set; } = null!;
        public virtual DbSet<Nganh> Nganhs { get; set; } = null!;
        public virtual DbSet<QuocTich> QuocTiches { get; set; } = null!;
        public virtual DbSet<TinhTp> TinhTps { get; set; } = null!;
        public virtual DbSet<TonGiao> TonGiaos { get; set; } = null!;
        public virtual DbSet<TpQhPx> TpQhPxes { get; set; } = null!;
        public virtual DbSet<TruongThpt> TruongThpts { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; } = null!;
        public virtual DbSet<VaiTro> VaiTro { get; set; } = null!;
        public virtual DbSet<Dot> Dot { get; set; } = null!;
        public virtual DbSet<TrangThai> TrangThai { get; set; } = null!;
        public virtual DbSet<LichTrinh> LichTrinh { get; set; } = null!;
        public virtual DbSet<LoaiHinhThuc> LoaiHinhThuc { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBao { get; set; } = null!;
        public virtual DbSet<LoaiThongBao> LoaiThongBao { get; set; } = null!;
        public virtual DbSet<HocBa> HocBa { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS_2021;Database=XetTuyenVLU;Trusted_Connection=True;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangDiemThpt>(entity =>
            {
                entity.ToTable("BangDiemTHPT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gdcd).HasColumnName("GDCD");

                entity.Property(e => e.MaHoSoThpt).HasColumnName("MaHoSoTHPT");

                entity.Property(e => e.MaHocKyLop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaHocKy_Lop");
            });

            modelBuilder.Entity<ChungChiNn>(entity =>
            {
                entity.ToTable("ChungChiNN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChungChi).HasMaxLength(200);

                entity.Property(e => e.MaNn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNN");
            });

            modelBuilder.Entity<DanToc>(entity =>
            {
                entity.ToTable("DanToc");

                entity.Property(e => e.DienGiai)
                    .HasMaxLength(255)
                    .HasColumnName("DIEN_GIAI");

                entity.Property(e => e.MaDantoc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MA_DANTOC");

                entity.Property(e => e.TenDantoc)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_DANTOC");
            });

            modelBuilder.Entity<HoSoThpt>(entity =>
            {
                entity.ToTable("HoSoTHPT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ccnn)
                    .HasMaxLength(200)
                    .HasColumnName("CCNN");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMND");

                entity.Property(e => e.DaNhanHoSo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();

                entity.Property(e => e.DateEdited).HasColumnType("datetime");

                entity.Property(e => e.DateInserted).HasColumnType("datetime");

                entity.Property(e => e.DiemVeMyThuat)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DiemVeNangKhieu)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DienThoaiDd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DienThoaiDD");

                entity.Property(e => e.DienThoaiPhuHuynh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoiTuongUuTien).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HanhKiemLop12).HasMaxLength(20);

                entity.Property(e => e.HoKhau).HasMaxLength(300);

                entity.Property(e => e.HoKhauMaPhuong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("HoKhau_MaPhuong");

                entity.Property(e => e.HoKhauMaQh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("HoKhau_MaQH");

                entity.Property(e => e.HoKhauMaTinhTp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("HoKhau_MaTinhTP");

                entity.Property(e => e.HoKhauTenPhuong)
                    .HasMaxLength(100)
                    .HasColumnName("HoKhau_TenPhuong");

                entity.Property(e => e.HoKhauTenQh)
                    .HasMaxLength(120)
                    .HasColumnName("HoKhau_TenQH");

                entity.Property(e => e.HoKhauTenTinhTp)
                    .HasMaxLength(120)
                    .HasColumnName("HoKhau_TenTinhTP");

                entity.Property(e => e.HoVaTen).HasMaxLength(100);

                entity.Property(e => e.HocLucLop12).HasMaxLength(20);

                entity.Property(e => e.KhuVuc)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LienLacDiaChi)
                    .HasMaxLength(200)
                    .HasColumnName("LienLac_DiaChi");

                entity.Property(e => e.LienLacMaPhuongXa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LienLac_MaPhuongXa");

                entity.Property(e => e.LienLacMaQh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LienLac_MaQH");

                entity.Property(e => e.LienLacMaTp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LienLac_MaTP");

                entity.Property(e => e.LienLacTenPhuongXa)
                    .HasMaxLength(100)
                    .HasColumnName("LienLac_TenPhuongXa");

                entity.Property(e => e.LienLacTenQh)
                    .HasMaxLength(150)
                    .HasColumnName("LienLac_TenQH");

                entity.Property(e => e.LienLacTenTp)
                    .HasMaxLength(150)
                    .HasColumnName("LienLac_TenTP");

                entity.Property(e => e.LoaiHinhTn)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("LoaiHinhTN");

                entity.Property(e => e.MaDanToc)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaNganhToHop1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaNganh_ToHop1");

                entity.Property(e => e.MaNganhToHop2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaNganh_ToHop2");

                entity.Property(e => e.MaNganhToHop3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaNganh_ToHop3");

                entity.Property(e => e.MaNoiSinh)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaTonGiao)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaTruongThpt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTruongTHPT");

                entity.Property(e => e.NamTotNghiep)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.QuocTich).HasMaxLength(50);

                entity.Property(e => e.SoBaoDanh).HasMaxLength(10);

                entity.Property(e => e.TenDanToc).HasMaxLength(100);

                entity.Property(e => e.TenLop12).HasMaxLength(50);

                entity.Property(e => e.TenNganhTenToHop1)
                    .HasMaxLength(200)
                    .HasColumnName("TenNganh_TenToHop1");

                entity.Property(e => e.TenNganhTenToHop2)
                    .HasMaxLength(200)
                    .HasColumnName("TenNganh_TenToHop2");

                entity.Property(e => e.TenNganhTenToHop3)
                    .HasMaxLength(200)
                    .HasColumnName("TenNganh_TenToHop3");

                entity.Property(e => e.TenNoiSinh).HasMaxLength(100);

                entity.Property(e => e.TenTonGiao).HasMaxLength(100);

                entity.Property(e => e.TenTruongThpt)
                    .HasMaxLength(100)
                    .HasColumnName("TenTruongTHPT");

                entity.Property(e => e.TruongThptMaQh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TruongTHPT_MaQH");

                entity.Property(e => e.TruongThptMaTinhTp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TruongTHPT_MaTinhTP");

                entity.Property(e => e.TruongThptTenQh)
                    .HasMaxLength(120)
                    .HasColumnName("TruongTHPT_TenQH");

                entity.Property(e => e.TruongThptTenTinhTp)
                    .HasMaxLength(200)
                    .HasColumnName("TruongTHPT_TenTinhTP");
            });

            modelBuilder.Entity<Nganh>(entity =>
            {
                entity.ToTable("Nganh");

                entity.Property(e => e.Ctdb)
                    .HasMaxLength(255)
                    .HasColumnName("CTDB");

                entity.Property(e => e.Cttc)
                    .HasMaxLength(255)
                    .HasColumnName("CTTC");

                entity.Property(e => e.Cttt)
                    .HasMaxLength(255)
                    .HasColumnName("CTTT");

                entity.Property(e => e.Flag)
                    .HasColumnName("FLAG")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaNganh)
                    .HasMaxLength(255)
                    .HasColumnName("MA_NGANH");

                entity.Property(e => e.MaTohop)
                    .HasMaxLength(255)
                    .HasColumnName("MA_TOHOP");

                entity.Property(e => e.ManganhTohop)
                    .HasMaxLength(255)
                    .HasColumnName("MANGANH_TOHOP");

                entity.Property(e => e.TenNganh)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_NGANH");

                entity.Property(e => e.TenTohop)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_TOHOP");
            });

            modelBuilder.Entity<QuocTich>(entity =>
            {
                entity.HasKey(e => e.MaQt)
                    .HasName("PK_tblQuocTich");

                entity.ToTable("QuocTich");

                entity.Property(e => e.MaQt).HasColumnName("MaQT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TenQt)
                    .HasMaxLength(255)
                    .HasColumnName("TenQT");
            });

            modelBuilder.Entity<TinhTp>(entity =>
            {
                entity.ToTable("TinhTP");

                entity.Property(e => e.MaTinhtp)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MA_TINHTP");

                entity.Property(e => e.TenTinhtp)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_TINHTP");
            });

            modelBuilder.Entity<TonGiao>(entity =>
            {
                entity.ToTable("TonGiao");

                entity.Property(e => e.Diengiai).HasColumnName("DIENGIAI");

                entity.Property(e => e.MaTongiao)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MA_TONGIAO");

                entity.Property(e => e.TenTongiao)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_TONGIAO");
            });

            modelBuilder.Entity<TpQhPx>(entity =>
            {
                entity.ToTable("TP_QH_PX");

                entity.Property(e => e.Cap).HasMaxLength(255);

                entity.Property(e => e.EnglishName).HasMaxLength(255);

                entity.Property(e => e.MaPx)
                    .HasMaxLength(255)
                    .HasColumnName("MaPX");

                entity.Property(e => e.MaQh)
                    .HasMaxLength(255)
                    .HasColumnName("MaQH");

                entity.Property(e => e.MaTinhTp)
                    .HasMaxLength(255)
                    .HasColumnName("MaTinhTP");

                entity.Property(e => e.TenPx)
                    .HasMaxLength(255)
                    .HasColumnName("TenPX");

                entity.Property(e => e.TenQh)
                    .HasMaxLength(255)
                    .HasColumnName("TenQH");

                entity.Property(e => e.TenTinhTp)
                    .HasMaxLength(255)
                    .HasColumnName("TenTinhTP");
            });

            modelBuilder.Entity<TruongThpt>(entity =>
            {
                entity.ToTable("TruongTHPT");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(255)
                    .HasColumnName("DIA_CHI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KhuVuc)
                    .HasMaxLength(255)
                    .HasColumnName("KHU_VUC");

                entity.Property(e => e.LoaiTruong)
                    .HasMaxLength(255)
                    .HasColumnName("LOAI_TRUONG");

                entity.Property(e => e.MaQh)
                    .HasMaxLength(255)
                    .HasColumnName("MA_QH");

                entity.Property(e => e.MaTinhtp)
                    .HasMaxLength(5)
                    .HasColumnName("MA_TINHTP");

                entity.Property(e => e.MaTptruong)
                    .HasMaxLength(20)
                    .HasColumnName("MA_TPTRUONG");

                entity.Property(e => e.MaTruong)
                    .HasMaxLength(20)
                    .HasColumnName("MA_TRUONG");

                entity.Property(e => e.TenQh)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_QH");

                entity.Property(e => e.TenTinhtp)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_TINHTP");

                entity.Property(e => e.TenTruong)
                    .HasMaxLength(255)
                    .HasColumnName("TEN_TRUONG");
            });
            modelBuilder.Entity<HocBa>(entity =>
            {
                entity.ToTable("HocBa");

                entity.Property(e => e.Id)
                    .HasColumnName("ID");
                entity.Property(e => e.MaHoSoThpt)
                    .HasColumnName("MaHoSoThpt");
                entity.Property(e => e.DuongDanHocBa)
                    .HasColumnName("DuongDanHocBa");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
