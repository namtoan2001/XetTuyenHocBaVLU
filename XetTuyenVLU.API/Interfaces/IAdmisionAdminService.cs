using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.AdmissionAdmin;

namespace XetTuyenVLU.Interfaces
{
    public interface IAdmisionAdminService
    {
        public List<Hoso> GetDatainHoso();
        public List<BangDiem> GetBangDiem();
        public List<Phase> GetPhase();
        public AdmissionMailVM GetMailBeforeSend();
        public AdmissionVM GetAdmissionById(int id);
        public Task<bool> DeleteHoSoThpts(long Id);
        public Task<bool> DeleteBangDiemThpts(long MaHoSoThpt);
        public Task<bool> EditAdmission(AdmissionEditingRequest request);
        public Task<bool> SendEmailForAdmission(AdmissionSendMailRequest request);
        public bool ReceiveAdmissionProfileById(int id);
        public bool RejectAdmissionProfileById(int id);
        public bool UpdateSendMailStatusById(int id);
        public FileDownloadVM DownloadAdmissionFiles(int id);
    }
}
