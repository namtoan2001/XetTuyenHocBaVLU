using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Account;

namespace XetTuyenVLU.UnitTest
{
    public class MockJwtService : IJwtService
    {
        private const string secretKey = "ThisIsTheSecretKey";
        private const string issuer = "XetTuyenVLU";
        private const string audience = "XetTuyenVLU";

        public string GenarateJwt(TaiKhoan taiKhoan)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[] {
                new Claim(ClaimTypes.Sid, taiKhoan.ID.ToString()),
                new Claim("userName", taiKhoan.TenDangNhap),
                new Claim("fullName", taiKhoan.HoVaTen),
                new Claim("role", taiKhoan.TenVaiTro)
            };

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(issuer, audience, claims, null, DateTime.Now.AddDays(1));

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
