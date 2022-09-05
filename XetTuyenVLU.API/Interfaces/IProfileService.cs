using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Profile; 

namespace XetTuyenVLU.Interfaces
{
    public interface IProfileService
    {
        public HoSoThpt GetProfileByCMND(string cmnd, int dot);

        public List<BangDiemThpt> GetBangDiem(int maHoSo);

        public bool ValidateCMNDToEdit(string cmnd, string currentCmnd);

        public Task<bool> EditProfile(EditProfileRequest request);

        public Task<bool> AddImgPathHocBa(AddImgPath hocBa);

    }
}
