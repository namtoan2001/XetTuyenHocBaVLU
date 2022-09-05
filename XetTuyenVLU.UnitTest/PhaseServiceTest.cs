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
using XetTuyenVLU.ViewModels.Phase;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class PhaseServiceTest
    {
        private readonly XetTuyenVLUContext _mockContext;
        private readonly PhaseService _phaseService;
        private readonly MockJwtService _mockJwtService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public PhaseServiceTest()
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

                context.Dot.AddRange(
                new Dot
                {
                    ID = 1,
                    DotThu = "1",
                    Khoa = "28",
                    NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 2,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                },
                new Dot
                {
                    ID = 2,
                    DotThu = "2",
                    Khoa = "28",
                    NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 1,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                });

                context.SaveChanges();
            }
            _mockContext = new XetTuyenVLUContext(options);
            _mockJwtService = new MockJwtService();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _phaseService = new PhaseService(_mockContext, _mockJwtService, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task GetAllPhases_ReturnListPhase()
        {
            //Arrange

            //Action
            var result = _phaseService.GetAllPhases();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<PhaseVM>>(result);
        }

        [Fact]
        public async Task CreatePhase_ReturnNewIdPhase()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new Dot
            {
                DotThu = "3",
                Khoa = "28",
                NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
            };

            //Action
            var result = await _phaseService.CreatePhase(request);

            //Assert
            Assert.True(result > 0);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task CreatePhase_WithAccountNotExist_ThrowAnException()
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
            var request = new Dot
            {
                DotThu = "3",
                Khoa = "28",
                NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _phaseService.CreatePhase(request));

            //Assert
            Assert.Equal($"Không tìm thấy tài khoản với ID {taiKhoan.ID}!", exception.Message);
        }

        [Fact]
        public async Task CreatePhase_WithAnyInputMissing_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new Dot
            {
                DotThu = "3",
                NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _phaseService.CreatePhase(request));

            //Assert
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", exception.Message);
        }
    }
}
