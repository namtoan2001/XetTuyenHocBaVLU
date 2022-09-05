using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Notification;

namespace XetTuyenVLU.Services
{
    public class NotificationService : INotificationService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NotificationService(XetTuyenVLUContext context, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
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

        public List<NotificationVM> GetAllNotifications()
        {
            var thongBaos = _context.ThongBao.OrderByDescending(x => x.ID).ToList();
            var thongBaoVM = new List<NotificationVM>();
            foreach (var thongBao in thongBaos)
            {
                thongBaoVM.Add(new NotificationVM
                {
                    ID = thongBao.ID,
                    Content = thongBao.Content,
                    LoaiThongBaoId = thongBao.LoaiThongBaoId,
                    TenThongBao = _context.LoaiThongBao.FirstOrDefault(x => x.ID == thongBao.LoaiThongBaoId)?.TenThongBao,
                    TrangThaiId = thongBao.TrangThaiId,
                    TenTrangThai = _context.TrangThai.FirstOrDefault(x => x.ID == thongBao.TrangThaiId)?.TenTrangThai,
                    TaiKhoanId = thongBao.TaiKhoanId,
                    TenNguoiTao = _context.TaiKhoan.FirstOrDefault(x => x.ID == thongBao.TaiKhoanId)?.HoVaTen,
                    NgayTao = thongBao.NgayTao
                });
            }
            return thongBaoVM;
        }

        public async Task<int> CreateNotification(NotificationCreateRequest request)
        {
            var claimId = GetJwtSecurityToken().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            var taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.ID == Int32.Parse(claimId.Value));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Int32.Parse(claimId.Value)}!");
            if(request.Content == null || request.LoaiThongBaoId == null)
                throw new Exception("Vui lòng nhập đầy đủ thông tin!");
            var thongBao = new ThongBao
            {
                Content = request.Content,
                TrangThaiId = 2,
                LoaiThongBaoId = Int32.Parse(request.LoaiThongBaoId),
                NgayTao = DateTime.Now,
                TaiKhoanId = taiKhoan.ID
            };
            _context.ThongBao.Add(thongBao);
            await _context.SaveChangesAsync();
            return thongBao.ID;
        }

        public List<LoaiThongBao> GetAllNotificationCategories()
        {
            return _context.LoaiThongBao.ToList();
        }

        public NotificationVM GetNotificationById(int id)
        {
            var thongBao = _context.ThongBao.FirstOrDefault(x => x.ID == id);
            if(thongBao == null)
                throw new Exception($"Không tìm thấy thông báo có ID {id}!");
            var thongBaoVM = new NotificationVM
            {
                ID = thongBao.ID,
                Content = thongBao.Content,
                LoaiThongBaoId = thongBao.ID,
                TenThongBao = _context.LoaiThongBao.FirstOrDefault(x => x.ID == thongBao.LoaiThongBaoId)?.TenThongBao
            };
            return thongBaoVM;
        }

        public async Task<bool> ChangeStatusNotification(int id)
        {
            var thongBao = _context.ThongBao.FirstOrDefault(x => x.ID == id);
            if (thongBao == null)
                throw new Exception($"Không tìm thấy thông báo có ID {id}!");
            thongBao.TrangThaiId = 1;
            _context.ThongBao.Update(thongBao);
            var thongBaos = _context.ThongBao.Where(x => x.LoaiThongBaoId == thongBao.LoaiThongBaoId && x.ID != id);
            foreach (var item in thongBaos)
            {
                item.TrangThaiId = 2;
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
