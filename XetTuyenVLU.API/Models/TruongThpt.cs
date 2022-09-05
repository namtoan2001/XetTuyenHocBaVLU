using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class TruongThpt
    {
        [Key]
        public double Id { get; set; }
        public string? MaTptruong { get; set; }
        public string? MaTinhtp { get; set; }
        public string? TenTinhtp { get; set; }
        public string? MaQh { get; set; }
        public string? TenQh { get; set; }
        public string? MaTruong { get; set; }
        public string? TenTruong { get; set; }
        public string? DiaChi { get; set; }
        public string? KhuVuc { get; set; }
        public string? LoaiTruong { get; set; }
    }
}
