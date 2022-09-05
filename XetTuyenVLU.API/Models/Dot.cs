using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public class Dot
    {
        [Key]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string DotThu { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Khoa { get; set; }

        public int TrangThaiId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayBatDau { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayKetThuc { get; set; }

        public bool IsExpired { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayTao { get; set; }

        public int TaiKhoanId { get; set; }
    }
}
