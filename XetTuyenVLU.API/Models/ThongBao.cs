using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public class ThongBao
    {
        [Key]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Content { get; set; }

        public int TrangThaiId { get; set; }

        public int LoaiThongBaoId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayTao { get; set; }

        public int TaiKhoanId { get; set; }
    }
}
