using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XetTuyenVLU.Models
{
    public class LoaiHinhThuc
    {
        [Key]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string TenHinhThuc { get; set; }
    }
}
