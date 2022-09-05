using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Account;

namespace XetTuyenVLU.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenarateJwt(TaiKhoan taikhoan)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[] {
                new Claim(ClaimTypes.Sid, taikhoan.ID.ToString()),
                new Claim("userName", taikhoan.TenDangNhap),
                new Claim("fullName", taikhoan.HoVaTen),
                new Claim("role", taikhoan.TenVaiTro)
            };

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims, null, DateTime.Now.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payload);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public JwtSecurityToken DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken;
        }
    }
}
