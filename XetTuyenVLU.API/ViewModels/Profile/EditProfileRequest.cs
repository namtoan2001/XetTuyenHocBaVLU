using System.ComponentModel.DataAnnotations;
namespace XetTuyenVLU.ViewModels.Profile
{
    public class EditProfileRequest
    {
        [Required]

        public string id { get; set; }
        [Required]
        public string hovaten { get; set; }
        [Required]
        public string gioitinh { get; set; }
        [Required]
        public string ngaysinh { get; set; }
        [Required]
        public string noisinh { get; set; }
        [Required]
        public string dantoc { get; set; }
        [Required]
        public string tongiao { get; set; }
        [Required]
        public string cmnd { get; set; }
        [Required]
        public int quoctich { get; set; }
        [Required]
        public string hokhauthuongtru { get; set; }
        [Required]
        public string tinhthanhpho { get; set; }
        [Required]
        public string quanhuyen { get; set; }
        [Required]
        public string phuongxa { get; set; }
        [Required]
        public string namtotnghiep { get; set; }
        [Required]
        public string hocluclop12 { get; set; }
        [Required]
        public string hanhkiemlop12 { get; set; }
        [Required]
        public string hocchuongtrinh { get; set; }
        [Required]
        public string tinhthanhpho_thpt { get; set; }
        [Required]
        public string quanhuyen_thpt { get; set; }
        [Required]
        public string tentruongthpt { get; set; }
        [Required]
        public string tenlop12 { get; set; }
        [Required]
        public string khuvucuutien { get; set; }
        [Required]
        public string doituonguutien { get; set; }
        [Required]
        public bool sudunghokhau { get; set; }
        [Required]
        public string diachinha { get; set; }
        [Required]
        public string tinhthanhpho_nha { get; set; }
        [Required]
        public string quanhuyen_nha { get; set; }
        [Required]
        public string phuongxa_nha { get; set; }
        [Required]
        public string dienthoaididong { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string dienthoaiphuhuynh { get; set; }
        [Required]
        public string phuongan { get; set; }
        public string? diemtb_cnlop11 { get; set; }
        public string? diemtb_hk1lop12 { get; set; }
        public string? diemtb_cnlop12 { get; set; }
        public string? chungchingoaingu { get; set; }
        [Required]
        public string nganh1 { get; set; }
        [Required]
        public string tohopmon1 { get; set; }
        [Required]
        public string? chuongtrinh1 { get; set; }
        public string? nganh2 { get; set; }
        public string? tohopmon2 { get; set; }
        public string? chuongtrinh2 { get; set; }
        public string? nganh3 { get; set; }
        public string? tohopmon3 { get; set; }
        public string? chuongtrinh3 { get; set; }
    }
}
