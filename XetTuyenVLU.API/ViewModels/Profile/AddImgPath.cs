using System.ComponentModel.DataAnnotations.Schema;

namespace XetTuyenVLU.ViewModels.Profile
{
    public class AddImgPath
    {
        public long? MaHoSoThpt { get; set; }
        public string? DuongDanHocBa { get; set; }

        [NotMapped]
        public IList<IFormFile> imgFile { get; set; }
    }
}
