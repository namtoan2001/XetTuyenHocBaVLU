using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public partial class BangDiemThpt
    {
        [Key]
        public int Id { get; set; }
        public long? MaHoSoThpt { get; set; }
        public string? MaHocKyLop { get; set; }
        public double? Toan { get; set; }
        public double? Van { get; set; }
        public double? Anh { get; set; }
        public double? Phap { get; set; }
        public double? Ly { get; set; }
        public double? Hoa { get; set; }
        public double? Sinh { get; set; }
        public double? Su { get; set; }
        public double? Dia { get; set; }
        public double? Gdcd { get; set; }
    }
}
