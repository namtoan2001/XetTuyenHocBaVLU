using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class Nganh
    {
        [Key]
        public double Id { get; set; }
        public string? ManganhTohop { get; set; }
        public string? MaNganh { get; set; }
        public string? TenNganh { get; set; }
        public string? MaTohop { get; set; }
        public string? TenTohop { get; set; }
        public string? Cttc { get; set; }
        public string? Ctdb { get; set; }
        public string? Cttt { get; set; }
        public bool? Flag { get; set; }
    }
}
