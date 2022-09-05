using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace XetTuyenVLU.Models
{
    public partial class HocBa
    {
        [Key]
        public int Id { get; set; }
        public long? MaHoSoThpt { get; set; }
        public string? DuongDanHocBa { get; set; }
    }
}
