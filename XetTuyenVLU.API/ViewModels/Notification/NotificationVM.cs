namespace XetTuyenVLU.ViewModels.Notification
{
    public class NotificationVM
    {
        public int ID { get; set; }
        public string? Content { get; set; }
        public int LoaiThongBaoId { get; set; }
        public string? TenThongBao { get; set; }
        public int TrangThaiId { get; set; }
        public string? TenTrangThai { get; set; }
        public int TaiKhoanId { get; set; }
        public string? TenNguoiTao { get; set; }
        public  DateTime NgayTao { get; set; }
    }
}
