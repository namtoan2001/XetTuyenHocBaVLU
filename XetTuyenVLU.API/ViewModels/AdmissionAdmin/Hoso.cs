using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace XetTuyenVLU.ViewModels.AdmissionAdmin
{
    public class Hoso
    {
        public long Id { get; set; }
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public string? Cmnd { get; set; }
        public string? DienThoaiDd { get; set; }
        public string? TenNoiSinh { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? TenDanToc { get; set; }
        public string? TenTonGiao { get; set; }
        public string? QuocTich { get; set; }
        public string? HoKhau { get; set; }
        public string? NamTotNghiep { get; set; }
        public string? SoBaoDanh { get; set; }
        public string? HocLucLop12 { get; set; }
        public string? HanhKiemLop12 { get; set; }
        public string? LoaiHinhTn { get; set; }
        public string? TenTruongThpt { get; set; }
        public string? TenLop12 { get; set; }
        public string? KhuVuc { get; set; }
        public string? DoiTuongUuTien { get; set; }
        public string? Chungchingoaingu { get; set; }
        public string? TenNganhTenToHop1 { get; set; }
        public string? ChuongTrinhHoc1 { get; set; }
        public string? TenNganhTenToHop2 { get; set; }
        public string? ChuongTrinhHoc2 { get; set; }
        public string? TenNganhTenToHop3 { get; set; }
        public string? ChuongTrinhHoc3 { get; set; }
        public string? DiaChiLienLac { get; set; }
        public string? DaNhanHoSo { get; set; }
        public bool? DaGuiMail { get; set; }
        public string? DiemVeMyThuat { get; set; }
        public string? DiemVeNangKhieu { get; set; }
        public string? Phase { get; set; }
        public int? DotId { get; set; }
        public string? Khoa { get; set; }
    }
}
