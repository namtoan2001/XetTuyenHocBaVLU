using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class QuocTich
    {
        [Key]
        public int Id { get; set; }
        public double MaQt { get; set; }
        public string? TenQt { get; set; }
    }
}
