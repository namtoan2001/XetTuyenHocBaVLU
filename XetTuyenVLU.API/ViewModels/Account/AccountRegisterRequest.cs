using System.ComponentModel.DataAnnotations;

namespace XetTuyenVLU.ViewModels.Account
{
    public class AccountRegisterRequest
    {
        [Required]
        public string HoVaTen { get; set; }

        [Required]
        [EmailAddress]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }

        [Required]
        public int VaiTroId { get; set; }
    }
}
