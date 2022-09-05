using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Admission;
using XetTuyenVLU.ViewModels.Notification;

namespace XetTuyenVLU.Interfaces
{
    public interface IAdmissionService
    {
        public List<TinhThanhPhoVM> GetCityProvincesForHoKhau();
        public List<QuanHuyenVM> GetDistrictsForHoKhau(string MaTinhTP);
        public List<PhuongXaVM> GetWardsForHoKhau(string MaQH);
        public List<TinhThanhPhoVM> GetCityProvincesForSchool();
        public List<QuanHuyenVM> GetDistrictsForSchool(string MaTinhTP);
        public List<TruongTHPTVM> GetSchools(string MaTinhTP, string MaQH);
        public List<DanToc> GetEthnics();
        public List<TonGiao> GetReligions();
        public List<QuocTich> GetNationalities();
        public List<ChungChiNn> GetCertificateLanguages();
        public List<NganhXetTuyenVM> GetNganhXetTuyen();
        public List<ToHopXetTuyenVM> GetToHopXetTuyen(string MaNganh);
        public bool ValidateCMND(string cmnd);
        public Task<string> CreateAdmission(AdmissionCreateRequest request);
        public DotXetTuyenVM GetPhase();

        public LichTrinhVM GetScheduleForEditProfile();
        public bool ValidatePhaseIsExpired();
        public ThongBao GetNotificationForPhaseIsExpired();
    }
}
