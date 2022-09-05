using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Schedule;

namespace XetTuyenVLU.Interfaces
{
    public interface IScheduleService
    {
        public List<ScheduleVM> GetAllSchedules();
        public List<Dot> GetAllPhasesNotExpiry();
        public List<LoaiHinhThuc> GetCategoriesForSchedule();
        public Task<int> CreateSchedule(ScheduleCreateRequest request);
        public Task<bool> ChangeStatusSchedule(int id);
        public bool ValidateAllSchedulesWereExpired();
    }
}
