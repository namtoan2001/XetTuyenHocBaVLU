using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XetTuyenVLU.Models;
using XetTuyenVLU.Services;
using XetTuyenVLU.ViewModels.Account;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class AccountServiceTest
    {
        private readonly XetTuyenVLUContext _mockContext;
        private readonly AccountService _accountService;
        private readonly MockJwtService _mockJwtService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public AccountServiceTest()
        {
            var options = new DbContextOptionsBuilder<XetTuyenVLUContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new XetTuyenVLUContext(options))
            {
                context.AddRange(
                new TaiKhoan
                {
                    ID = 1,
                    HoVaTen = "Nguyễn Chí Thành",
                    TenDangNhap = "thanh.nguyenchi@gmail.com",
                    MatKhau = BCrypt.Net.BCrypt.HashPassword("123456"),
                    VaiTroId = 1,
                    TenVaiTro = "Admin"
                },
                new TaiKhoan
                {
                    ID = 2,
                    HoVaTen = "Nguyễn Văn Tèo",
                    TenDangNhap = "teo.nguyenvan@gmail.com",
                    MatKhau = BCrypt.Net.BCrypt.HashPassword("123456"),
                    VaiTroId = 2,
                    TenVaiTro = "Collaborator"
                });

                context.AddRange(
                new VaiTro
                {
                    ID = 1,
                    TenVaiTro = "Admin"
                },
                new VaiTro
                {
                    ID = 2,
                    TenVaiTro = "Collaborator"
                });

                context.SaveChanges();
            }
            _mockContext = new XetTuyenVLUContext(options);
            _mockJwtService = new MockJwtService();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _accountService = new AccountService(_mockContext, _mockJwtService, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task GetAllAccounts_ReturnListTaiKhoan()
        {
            //Arrange

            //Action
            var result = _accountService.GetAllAccounts();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<AccountVM>>(result);
        }

        [Fact]
        public async Task GetAllRoles_ReturnListVaiTro()
        {
            //Arrange

            //Action
            var result = _accountService.GetAllRoles();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<VaiTro>>(result);
        }

        [Fact]
        public async Task GetAccountById_ReturnTaiKhoanWithId()
        {
            //Arrange
            var id = 1;

            //Action
            var result = _accountService.GetAccountById(id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AccountVM>(result);
        }

        [Fact]
        public async Task GetAccountById_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            var id = 999;

            //Action
            var exception = Assert.Throws<Exception>(() => _accountService.GetAccountById(id));

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản có ID {id}", exception.Message);
        }

        [Fact]
        public async Task Login_ReturnAccountLoginViewModel()
        {
            //Arrange
            var request = new AccountLoginRequest
            {
                TenDangNhap = "thanh.nguyenchi@gmail.com",
                MatKhau = "123456"
            };

            //Action
            var result = await _accountService.Login(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AccountLoginVM>(result);
        }

        [Fact]
        public async Task Login_WithTenDangNhapNotExist_ThrowAnException()
        {
            //Arrange
            var request = new AccountLoginRequest
            {
                TenDangNhap = "khongtontai@gmail.com",
                MatKhau = "123456"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Login(request));

            //Assert
            Assert.Equal("Tên đăng nhập không tồn tại!", exception.Message);
        }

        [Fact]
        public async Task Login_WithMatKhauIncorrect_ThrowAnException()
        {
            //Arrange
            var request = new AccountLoginRequest
            {
                TenDangNhap = "thanh.nguyenchi@gmail.com",
                MatKhau = "123123123"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Login(request));

            //Assert
            Assert.Equal("Mật khẩu không hợp lệ!", exception.Message);
        }

        [Fact]
        public async Task CreateAccount_ReturnNewIdAccount()
        {
            //Arrange
            var request = new AccountRegisterRequest
            {
                HoVaTen = "Nguyễn Văn A",
                TenDangNhap = "a.nguyenvan@gmail.com",
                MatKhau = "123456",
                VaiTroId = 2
            };

            //Action
            var result = await _accountService.CreateAccount(request);

            //Assert
            Assert.True(result > 0);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task CreateAccount_WithTenDangNhapIsExisted_ThrowAnException()
        {
            //Arrange
            var request = new AccountRegisterRequest
            {
                HoVaTen = "Nguyễn Văn A",
                TenDangNhap = "teo.nguyenvan@gmail.com",
                MatKhau = "123456",
                VaiTroId = 2
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.CreateAccount(request));

            //Assert
            Assert.Equal("Tên đăng nhập đã tồn tại!", exception.Message);
        }

        [Fact]
        public async Task EditAccount_ReturnTrue()
        {
            //Arrange
            var request = new AccountEditingRequest
            {
                ID = "1",
                HoVaTen = "test",
                TenDangNhap = "test@gmail.com",
                VaiTroId = "1"
            };

            //Action
            var result = await _accountService.EditAccount(request);

            //Assert
            Assert.Equal(true, result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task EditAccount_WithTaiKhoanNotExist_ThrowAnException()
        {
            //Arrange
            var request = new AccountEditingRequest
            {
                ID = "999",
                HoVaTen = "test",
                TenDangNhap = "test@gmail.com",
                VaiTroId = "1"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.EditAccount(request));

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản với ID {request.ID}!", exception.Message);
        }

        [Fact]
        public async Task EditAccount_WithVaiTroNotExist_ThrowAnException()
        {
            //Arrange
            var request = new AccountEditingRequest
            {
                ID = "1",
                HoVaTen = "test",
                TenDangNhap = "test@gmail.com",
                VaiTroId = "999"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.EditAccount(request));

            //Assert
            Assert.Equal($"Không tìm thấy vai trò với ID {request.VaiTroId}!", exception.Message);
        }

        [Fact]
        public async Task DeleteAccount_ReturnTrue()
        {
            //Arrange
            int id = 1;

            //Action
            var result = await _accountService.DeleteAccount(id);

            //Assert
            Assert.Equal(true, result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task DeleteAccount_WithTaiKhoanNotExist_ThrowAnException()
        {
            //Arrange
            int id = 999;

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.DeleteAccount(id));

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản với ID {id}!", exception.Message);
        }

        [Fact]
        public async Task GetAccountProfile_ReturnAccountVM()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            //Action
            var result = _accountService.GetAccountProfile();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AccountVM>(result);
        }

        [Fact]
        public async Task GetAccountProfile_WithTaiKhoanNotExist_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = new TaiKhoan
            {
                ID = 99,
                HoVaTen = "test",
                TenDangNhap = "test",
                MatKhau = "test",
                VaiTroId = 1,
                TenVaiTro = "test"
            };
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            //Action
            var exception = Assert.Throws<Exception>(() => _accountService.GetAccountProfile());

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản với ID {taiKhoan.ID}!", exception.Message);
        }

        [Fact]
        public async Task EditAccountProfile_ReturnTrue()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ProfileEditingRequest
            {
                HoVaTen = "test"
            };

            //Action
            var result = await _accountService.EditAccountProfile(request);

            //Assert
            Assert.Equal(true, result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task EditAccountProfile_WithTaiKhoanNotExist_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = new TaiKhoan
            {
                ID = 99,
                HoVaTen = "test",
                TenDangNhap = "test",
                MatKhau = "test",
                VaiTroId = 1,
                TenVaiTro = "test"
            };
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ProfileEditingRequest
            {
                HoVaTen = "test"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.EditAccountProfile(request));

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản với ID {taiKhoan.ID}!", exception.Message);
        }

        [Fact]
        public async Task ChangePassword_ReturnTrue()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ChangePasswordRequest
            {
                matKhauCu = "123456",
                matKhauMoi = "test123"
            };

            //Action
            var result = await _accountService.ChangePassword(request);

            //Assert
            Assert.Equal(true, result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task ChangePassword_WithMatKhauCuIncorrect_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ChangePasswordRequest
            {
                matKhauCu = "9999",
                matKhauMoi = "test123"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.ChangePassword(request));

            //Assert
            Assert.Equal("Mật khẩu cũ không chính xác!", exception.Message);
        }

        [Fact]
        public async Task ChangePassword_WithTaiKhoanNotExist_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = new TaiKhoan
            {
                ID = 99,
                HoVaTen = "test",
                TenDangNhap = "test",
                MatKhau = "test",
                VaiTroId = 1,
                TenVaiTro = "test"
            };
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ChangePasswordRequest
            {
                matKhauCu = "123456",
                matKhauMoi = "test123"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.ChangePassword(request));

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản với ID {taiKhoan.ID}!", exception.Message);
        }

        
    }
}
