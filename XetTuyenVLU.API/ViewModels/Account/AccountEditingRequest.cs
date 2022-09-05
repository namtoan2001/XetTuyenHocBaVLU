using System.ComponentModel.DataAnnotations;

namespace XetTuyenVLU.ViewModels.Account
{
    public class AccountEditingRequest
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string HoVaTen { get; set; }

        [Required]
        [EmailAddress]
        public string TenDangNhap { get; set; }

        [Required]
        public string VaiTroId { get; set; }

    }
}
