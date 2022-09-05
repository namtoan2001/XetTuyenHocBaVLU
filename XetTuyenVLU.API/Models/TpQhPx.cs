using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class TpQhPx
    {
        [Key]
        public double Id { get; set; }
        public string? TenTinhTp { get; set; }
        public string? MaTinhTp { get; set; }
        public string? TenQh { get; set; }
        public string? MaQh { get; set; }
        public string? TenPx { get; set; }
        public string? MaPx { get; set; }
        public string? Cap { get; set; }
        public string? EnglishName { get; set; }
    }
}
