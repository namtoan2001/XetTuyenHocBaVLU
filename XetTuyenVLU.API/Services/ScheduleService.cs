using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Schedule;

namespace XetTuyenVLU.Services
{

    public class ScheduleService : IScheduleService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduleService(XetTuyenVLUContext context, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public JwtSecurityToken GetJwtSecurityToken()
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string token = authHeader.Substring("Bearer ".Length).Trim();
            return _jwtService.DecodeJwt(token);
        }

        public List<ScheduleVM> GetAllSchedules()
        {
            var schedules = _context.LichTrinh.OrderByDescending(x => x.ID).ToList();
            var scheduleList = new List<ScheduleVM>();
            foreach (var schedule in schedules)
            {
                scheduleList.Add(new ScheduleVM
                {
                    ID = schedule.ID,
                    DotId = schedule.MaDot,
                    DotThu = _context.Dot.FirstOrDefault(x => x.ID == schedule.MaDot)?.DotThu,
                    Khoa = _context.Dot.FirstOrDefault(x => x.ID == schedule.MaDot)?.Khoa,
                    NgayBatDau = schedule.NgayBatDau,
                    NgayKetThuc = schedule.NgayKetThuc,
                    TrangThaiId = schedule.TrangThaiId,
                    TenTrangThai = _context.TrangThai.FirstOrDefault(x => x.ID == schedule.TrangThaiId)?.TenTrangThai,
                    HinhThucId = schedule.HinhThucId,
                    TenHinhThuc = _context.LoaiHinhThuc.FirstOrDefault(x => x.ID == schedule.HinhThucId)?.TenHinhThuc,
                    IsExpired = schedule.IsExpired,
                    TaiKhoanId = schedule.TaiKhoanId,
                    TenNguoiTao = _context.TaiKhoan.FirstOrDefault(x => x.ID == schedule.TaiKhoanId)?.HoVaTen,
                    NgayTao = schedule.NgayTao
                });
            }
            return scheduleList;
        }

        public async Task<int> CreateSchedule(ScheduleCreateRequest request)
        {
            var claimId = GetJwtSecurityToken().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            var taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.ID == Int32.Parse(claimId.Value));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Int32.Parse(claimId.Value)}!");
            if (request.DotId == null
                || request.HinhThucId == null
                || request.NgayBatDau == DateTime.MinValue
                || request.NgayKetThuc == DateTime.MinValue)
                throw new Exception("Vui lòng nhập đầy đủ thông tin!");
            var phase = _context.Dot.FirstOrDefault(x => x.ID == Int32.Parse(request.DotId));
            if (phase == null || request.NgayBatDau < phase.NgayBatDau || request.NgayKetThuc > phase.NgayKetThuc)
                throw new Exception("Thời gian phải nằm trong đợt xét tuyển!");
            var schedule = new LichTrinh
            {
                MaDot = Int32.Parse(request.DotId),
                HinhThucId = Int32.Parse(request.HinhThucId),
                NgayBatDau = request.NgayBatDau,
                NgayKetThuc = request.NgayKetThuc,
                TrangThaiId = 2,
                TaiKhoanId = taiKhoan.ID,
                NgayTao = DateTime.Now
            };
            _context.LichTrinh.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule.ID;
        }

        public async Task<bool> ChangeStatusSchedule(int id)
        {
            var schedule = _context.LichTrinh.FirstOrDefault(x => x.ID == id);
            if (schedule == null)
                throw new Exception($"Không tìm thấy lịch trình có ID {id}!");
            var phase = _context.Dot.FirstOrDefault(x => x.ID == schedule.MaDot);
            if (phase == null || phase.TrangThaiId != 1)
                throw new Exception("Đợt xét tuyển của lịch trình này đã Closed!");
            schedule.TrangThaiId = 1;
            _context.LichTrinh.Update(schedule);
            var schedules = _context.LichTrinh.Where(x => x.HinhThucId == schedule.HinhThucId && x.ID != id);
            foreach (var item in schedules)
            {
                item.TrangThaiId = 2;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public bool ValidateAllSchedulesWereExpired()
        {
            var schedules = _context.Dot.ToList();
            foreach (var schedule in schedules)
            {
                if (schedule != null && schedule.NgayKetThuc < DateTime.Now)
                {
                    schedule.IsExpired = true;
                }
            }
            _context.Dot.UpdateRange(schedules);
            return _context.SaveChanges() > 0;
        }

        public List<Dot> GetAllPhasesNotExpiry()
        {
            return _context.Dot.Where(x => !x.IsExpired).ToList();
        }

        public List<LoaiHinhThuc> GetCategoriesForSchedule()
        {
            return _context.LoaiHinhThuc.ToList();
        }
    }
}
