using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public class LichTrinh
    {
        [Key]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [NotNull]
        public int MaDot { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayBatDau { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayKetThuc { get; set; }

        public int TrangThaiId { get; set; }

        public int HinhThucId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayTao { get; set; }

        public int TaiKhoanId { get; set; }

        public bool IsExpired { get; set; }
    }
}
