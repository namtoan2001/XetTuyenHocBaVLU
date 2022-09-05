using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class DanToc
    {
        [Key]
        public string MaDantoc { get; set; }
        public string? TenDantoc { get; set; }
        public string? DienGiai { get; set; }
    }
}
