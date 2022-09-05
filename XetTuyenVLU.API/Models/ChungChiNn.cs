using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class ChungChiNn
    {
        [Key]
        public int Id { get; set; }
        public string? MaNn { get; set; }
        public string? ChungChi { get; set; }
        public double? DiemQuiDoi { get; set; }
    }
}
