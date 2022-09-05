using System.ComponentModel.DataAnnotations;

namespace XetTuyenVLU.Models
{
    public partial class TinhTp
    {
        [Key]
        public string MaTinhtp { get; set; }
        public string? TenTinhtp { get; set; }
    }
}
