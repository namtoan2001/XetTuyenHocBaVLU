using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace XetTuyenVLU.Models
{
    public class TaiKhoan
    {
        [Key]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string HoVaTen { get; set; }

        [NotNull]
        [Column(TypeName = "nvarchar(100)")]
        public string TenDangNhap { get; set; }

        [NotNull]
        [Column(TypeName = "nvarchar(100)")]
        [IgnoreDataMember]
        public string MatKhau { get; set; }

        [NotNull]
        public int VaiTroId { get; set; }

        [NotNull]
        [Column(TypeName = "nvarchar(100)")]
        public string TenVaiTro { get; set; }
    }
}
