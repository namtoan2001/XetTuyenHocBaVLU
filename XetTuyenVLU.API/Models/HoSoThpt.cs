using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class HoSoThpt
    {
        [Key]
        public long Id { get; set; }
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? MaNoiSinh { get; set; }
        public string? TenNoiSinh { get; set; }
        public string? MaDanToc { get; set; }
        public string? TenDanToc { get; set; }
        public string? MaTonGiao { get; set; }
        public string? TenTonGiao { get; set; }
        public string? Cmnd { get; set; }
        public string? QuocTich { get; set; }
        public string? HoKhau { get; set; }
        public string? HoKhauMaPhuong { get; set; }
        public string? HoKhauTenPhuong { get; set; }
        public string? HoKhauMaTinhTp { get; set; }
        public string? HoKhauTenTinhTp { get; set; }
        public string? HoKhauMaQh { get; set; }
        public string? HoKhauTenQh { get; set; }
        public string? NamTotNghiep { get; set; }
        public string? SoBaoDanh { get; set; }
        public string? HocLucLop12 { get; set; }
        public string? HanhKiemLop12 { get; set; }
        public string? LoaiHinhTn { get; set; }
        public string? TruongThptMaTinhTp { get; set; }
        public string? TruongThptTenTinhTp { get; set; }
        public string? TruongThptMaQh { get; set; }
        public string? TruongThptTenQh { get; set; }
        public string? TenTruongThpt { get; set; }
        public string? MaTruongThpt { get; set; }
        public string? TenLop12 { get; set; }
        public string? KhuVuc { get; set; }
        public string? DoiTuongUuTien { get; set; }
        public string? PhuongAn { get; set; }
        public string? MaCcnn { get; set; }
        public string? Ccnn { get; set; }
        public string? MaNganhToHop1 { get; set; }
        public string? TenNganhTenToHop1 { get; set; }
        public string? ChuongTrinhHoc1 { get; set; }
        public string? MaNganhToHop2 { get; set; }
        public string? TenNganhTenToHop2 { get; set; }
        public string? ChuongTrinhHoc2 { get; set; }
        public string? MaNganhToHop3 { get; set; }
        public string? TenNganhTenToHop3 { get; set; }
        public string? ChuongTrinhHoc3 { get; set; }
        public string? LienLacDiaChi { get; set; }
        public string? LienLacMaPhuongXa { get; set; }
        public string? LienLacTenPhuongXa { get; set; }
        public string? LienLacMaTp { get; set; }
        public string? LienLacTenTp { get; set; }
        public string? LienLacMaQh { get; set; }
        public string? LienLacTenQh { get; set; }
        public string? DienThoaiDd { get; set; }
        public string? DienThoaiPhuHuynh { get; set; }
        public DateTime? DateInserted { get; set; }
        public DateTime? DateEdited { get; set; }
        public string? DaNhanHoSo { get; set; }
        public bool? DaGuiMail { get; set; }
        public int DotId { get; set; }
        public string? DiemVeMyThuat { get; set; }
        public string? DiemVeNangKhieu { get; set; }
        public virtual Dot Dot { get; set; }
    }
}
