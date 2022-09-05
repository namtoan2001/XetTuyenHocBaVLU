using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Phase;

namespace XetTuyenVLU.Services
{
    public class PhaseService : IPhaseService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PhaseService(XetTuyenVLUContext context, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
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

        public List<PhaseVM> GetAllPhases()
        {
            var phases = _context.Dot.OrderByDescending(x => x.ID).ToList();
            var listPhase = new List<PhaseVM>();
            foreach (var phase in phases)
            {
                listPhase.Add(new PhaseVM
                {
                    ID = phase.ID,
                    DotThu = phase.DotThu,
                    Khoa = phase.Khoa,
                    NgayBatDau = phase.NgayBatDau,
                    NgayKetThuc = phase.NgayKetThuc,
                    TrangThaiId = phase.TrangThaiId,
                    IsExpired = phase.IsExpired,
                    TenTrangThai = _context.TrangThai.FirstOrDefault(x => x.ID == phase.TrangThaiId)?.TenTrangThai,
                    TenNguoiTao = _context.TaiKhoan.FirstOrDefault(x => x.ID == phase.TaiKhoanId)?.HoVaTen,
                    NgayTao = phase.NgayTao
                });
            }
            return listPhase;
        }

        public async Task<int> CreatePhase(Dot request)
        {
            var claimId = GetJwtSecurityToken().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            var taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.ID == Int32.Parse(claimId.Value));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Int32.Parse(claimId.Value)}!");
            if (request.DotThu == null
                || request.Khoa == null
                || request.NgayBatDau == DateTime.MinValue
                || request.NgayKetThuc == DateTime.MinValue)
                throw new Exception("Vui lòng nhập đầy đủ thông tin!");
            if(request.NgayBatDau > request.NgayKetThuc)
                throw new Exception("Thời gian không hợp lệ!");
            var phase = new Dot
            {
                DotThu = request.DotThu,
                Khoa = request.Khoa,
                NgayBatDau = request.NgayBatDau,
                NgayKetThuc = request.NgayKetThuc,
                TrangThaiId = 2,
                TaiKhoanId = taiKhoan.ID,
                NgayTao = DateTime.Now
            };
            _context.Dot.Add(phase);
            await _context.SaveChangesAsync();
            return phase.ID;
        }

        public async Task<bool> ChangeStatusPhase(int id)
        {
            var phase = _context.Dot.FirstOrDefault(x => x.ID == id);
            if (phase == null)
                throw new Exception($"Không tìm thấy đợt xét tuyển có ID {id}!");
            phase.TrangThaiId = 1;
            _context.Dot.Update(phase);
            var phases = _context.Dot.Where(x => x.ID != id);
            foreach (var item in phases)
            {
                item.TrangThaiId = 2;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public bool ValidateAllPhasesWereExpired()
        {
            var phases = _context.Dot.ToList();
            foreach (var phase in phases)
            {
                if (phase != null && phase.NgayKetThuc < DateTime.Now)
                {
                    phase.IsExpired = true;
                }
            }
            _context.Dot.UpdateRange(phases);
            return _context.SaveChanges() > 0;
        }
    }
}
