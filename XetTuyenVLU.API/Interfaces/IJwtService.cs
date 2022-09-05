using System.IdentityModel.Tokens.Jwt;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Account;

namespace XetTuyenVLU.Interfaces
{
    public interface IJwtService
    {
        public string GenarateJwt(TaiKhoan taikhoan);
        public JwtSecurityToken DecodeJwt(string token);
    }
}
