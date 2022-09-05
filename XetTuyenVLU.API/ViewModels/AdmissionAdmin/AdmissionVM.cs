using XetTuyenVLU.ViewModels.Admission;

namespace XetTuyenVLU.ViewModels.AdmissionAdmin
{
    public class AdmissionVM
    {
        public long Id { get; set; }
        public int DotId { get; set; }
        public string? DotThu { get; set; }
        public string? Khoa { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? HoVaTen { get; set; }
        public string? Email { get; set; }
        public string? GioiTinh { get; set; }
        public string? NgaySinh { get; set; }
        public string? MaNoiSinh { get; set; }
        public string? MaDanToc { get; set; }
        public string? MaTonGiao { get; set; }
        public string? Cmnd { get; set; }
        public string? MaQuocTich { get; set; }
        public string? DiaChiHoKhau { get; set; }
        public string? HoKhauMaPhuong { get; set; }
        public string? HoKhauMaTinhTp { get; set; }
        public string? HoKhauMaQh { get; set; }
        public string? NamTotNghiep { get; set; }
        public string? SoBaoDanh { get; set; }
        public string? HocLucLop12 { get; set; }
        public string? HanhKiemLop12 { get; set; }
        public string? LoaiHinhTn { get; set; }
        public string? TruongThptMaTinhTp { get; set; }
        public string? TruongThptMaQh { get; set; }
        public string? MaTruongThpt { get; set; }
        public string? TenLop12 { get; set; }
        public string? KhuVuc { get; set; }
        public string? DoiTuongUuTien { get; set; }
        public string? MaCcnn { get; set; }
        public string? MaNganh1 { get; set; }
        public string? MaToHop1 { get; set; }
        public string? ChuongTrinhHoc1 { get; set; }
        public string? MaNganh2 { get; set; }
        public string? MaToHop2 { get; set; }
        public string? ChuongTrinhHoc2 { get; set; }
        public string? MaNganh3 { get; set; }
        public string? MaToHop3 { get; set; }
        public string? ChuongTrinhHoc3 { get; set; }
        public string? DiaChiLienLac { get; set; }
        public string? LienLacMaPhuongXa { get; set; }
        public string? LienLacMaTp { get; set; }
        public string? LienLacMaQh { get; set; }
        public string? DienThoaiDd { get; set; }
        public string? DienThoaiPhuHuynh { get; set; }
        public string? DaNhanHoSo { get; set; }
        public bool? DaGuiMail { get; set; }
        public string? phuongAn { get; set; }
        public BangDiemVM? diemCnlop11 { get; set; }
        public BangDiemVM? diemHk1lop12 { get; set; }
        public BangDiemVM? diemCnlop12 { get; set; }
    }
}
