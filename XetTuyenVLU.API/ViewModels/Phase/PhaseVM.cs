namespace XetTuyenVLU.ViewModels.Phase
{
    public class PhaseVM
    {
        public int ID { get; set; }
        public string DotThu { get; set; }
        public string Khoa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int TrangThaiId { get; set; }
        public string? TenTrangThai { get; set; }
        public bool IsExpired { get; set; }
        public int TaiKhoanId { get; set; }
        public string? TenNguoiTao { get; set; }
        public  DateTime NgayTao { get; set; }
    }
}
