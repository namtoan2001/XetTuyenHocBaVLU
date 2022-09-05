using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Account;

namespace XetTuyenVLU.Services
{
    public class AccountService : IAccountService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(XetTuyenVLUContext context, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
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

        public List<AccountVM> GetAllAccounts()
        {
            var taiKhoan = _context.TaiKhoan.OrderByDescending(x => x.ID).ToList();
            var account = new List<AccountVM>();
            foreach (var tk in taiKhoan)
            {
                account.Add(new AccountVM
                {
                    ID = tk.ID,
                    HoVaTen = tk.HoVaTen,
                    TenDangNhap = tk.TenDangNhap,
                    VaiTroId = tk.VaiTroId,
                    TenVaiTro = tk.TenVaiTro,
                });
            }
            return account;
        }

        public List<VaiTro> GetAllRoles()
        {
            return _context.VaiTro.ToList();
        }

        public AccountVM GetAccountById(int Id)
        {
            var taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.ID == Id);
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản có ID {Id}");
            var account = new AccountVM
            {
                ID = taiKhoan.ID,
                HoVaTen = taiKhoan.HoVaTen,
                TenDangNhap = taiKhoan.TenDangNhap,
                VaiTroId = taiKhoan.VaiTroId,
                TenVaiTro = taiKhoan.TenVaiTro,
            };
            return account;
        }

        public async Task<AccountLoginVM> Login(AccountLoginRequest request)
        {
            var taiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == request.TenDangNhap);
            if (taiKhoan == null)
                throw new Exception("Tên đăng nhập không tồn tại!");

            bool isCorrect = BCrypt.Net.BCrypt.Verify(request.MatKhau, taiKhoan.MatKhau);
            if (!isCorrect)
                throw new Exception("Mật khẩu không hợp lệ!");

            var token = _jwtService.GenarateJwt(taiKhoan);

            return new AccountLoginVM() { fullName = taiKhoan.HoVaTen, role = taiKhoan.TenVaiTro, jwtToken = token };
        }

        public async Task<int> CreateAccount(AccountRegisterRequest request)
        {
            var checkTaiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == request.TenDangNhap);
            var vaiTro = await _context.VaiTro.FirstOrDefaultAsync(x => x.ID == request.VaiTroId);
            if (checkTaiKhoan != null)
                throw new Exception("Tên đăng nhập đã tồn tại!");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.MatKhau);
            var taiKhoan = new TaiKhoan()
            {
                HoVaTen = request.HoVaTen,
                TenDangNhap = request.TenDangNhap,
                MatKhau = passwordHash,
                VaiTroId = request.VaiTroId,
                TenVaiTro = vaiTro.TenVaiTro
            };

            _context.TaiKhoan.Add(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan.ID;
        }

        public async Task<bool> EditAccount(AccountEditingRequest request)
        {
            var taiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.ID == Int32.Parse(request.ID));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {request.ID}!");
            var vaiTro = await _context.VaiTro.FirstOrDefaultAsync(x => x.ID == Int32.Parse(request.VaiTroId));
            if (vaiTro == null)
                throw new Exception($"Không tìm thấy vai trò với ID {request.VaiTroId}!");

            taiKhoan.HoVaTen = request.HoVaTen;
            taiKhoan.VaiTroId = Int32.Parse(request.VaiTroId);
            taiKhoan.TenVaiTro = vaiTro.TenVaiTro;
            _context.TaiKhoan.Update(taiKhoan);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAccount(int Id)
        {
            var taiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.ID == Id);
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Id}!");
            _context.TaiKhoan.Remove(taiKhoan);
            return await _context.SaveChangesAsync() > 0;
        }

        public AccountVM GetAccountProfile()
        {
            var claimId = GetJwtSecurityToken().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            var taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.ID == Int32.Parse(claimId.Value));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Int32.Parse(claimId.Value)}!");
            return new AccountVM
            {
                ID = taiKhoan.ID,
                HoVaTen = taiKhoan.HoVaTen,
                TenDangNhap = taiKhoan.TenDangNhap,
                VaiTroId = taiKhoan.VaiTroId,
                TenVaiTro = taiKhoan.TenVaiTro
            };
        }

        public async Task<bool> EditAccountProfile(ProfileEditingRequest request)
        {
            var claimId = GetJwtSecurityToken().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            var taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.ID == Int32.Parse(claimId.Value));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Int32.Parse(claimId.Value)}!");
            taiKhoan.HoVaTen = request.HoVaTen;
            _context.TaiKhoan.Update(taiKhoan);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            var claimId = GetJwtSecurityToken().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            var taiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.ID == Int32.Parse(claimId.Value));
            if (taiKhoan == null)
                throw new Exception($"Không tìm thấy tài khoản với ID {Int32.Parse(claimId.Value)}!");
            if (request.matKhauCu == "" || request.matKhauMoi == "")
                throw new Exception("Vui lòng nhập đầy đủ thông tin!");
            bool isCorrect = BCrypt.Net.BCrypt.Verify(request.matKhauCu, taiKhoan.MatKhau);
            if (!isCorrect)
                throw new Exception("Mật khẩu cũ không chính xác!");
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.matKhauMoi);
            taiKhoan.MatKhau = passwordHash;
            _context.TaiKhoan.Update(taiKhoan);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
