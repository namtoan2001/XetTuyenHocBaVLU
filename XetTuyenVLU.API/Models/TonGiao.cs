using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class TonGiao
    {
        [Key]
        public string MaTongiao { get; set; }
        public string? TenTongiao { get; set; }
        public string? Diengiai { get; set; }
    }
}
