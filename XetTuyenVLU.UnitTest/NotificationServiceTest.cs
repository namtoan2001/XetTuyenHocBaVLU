using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XetTuyenVLU.Models;
using XetTuyenVLU.Services;
using XetTuyenVLU.ViewModels.Notification;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class NotificationServiceTest
    {
        private readonly XetTuyenVLUContext _mockContext;
        private readonly NotificationService _notificationService;
        private readonly MockJwtService _mockJwtService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public NotificationServiceTest()
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

                context.ThongBao.AddRange(
                new ThongBao
                {
                    ID = 1,
                    Content = "<div>test</div>",
                    TrangThaiId = 2,
                    LoaiThongBaoId = 3,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                },
                new ThongBao
                {
                    ID = 2,
                    Content = "<div>test</div>",
                    TrangThaiId = 1,
                    LoaiThongBaoId = 3,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                });

                context.LoaiThongBao.AddRange(
                new LoaiThongBao
                {
                    ID = 1,
                    TenThongBao = "test1"
                },
                new LoaiThongBao
                {
                    ID = 3,
                    TenThongBao = "test3"
                });

                context.SaveChanges();
            }
            _mockContext = new XetTuyenVLUContext(options);
            _mockJwtService = new MockJwtService();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _notificationService = new NotificationService(_mockContext, _mockJwtService, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task GetAllPhases_ReturnListPhase()
        {
            //Arrange

            //Action
            var result = _notificationService.GetAllNotifications();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<NotificationVM>>(result);
        }

        [Fact]
        public async Task GetAllNotificationCategories_ReturnListCategories()
        {
            //Arrange

            //Action
            var result = _notificationService.GetAllNotificationCategories();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<LoaiThongBao>>(result);
        }

        [Fact]
        public async Task GetNotificationById_ReturnNotificationWithId()
        {
            //Arrange
            int id = 1;

            //Action
            var result = _notificationService.GetNotificationById(id);

            //Assert
            Assert.Equal(id, result.ID);
            Assert.IsType<NotificationVM>(result);
        }

        [Fact]
        public async Task GetNotificationById_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            int id = 99;

            //Action
            var exception = Assert.Throws<Exception>(() => _notificationService.GetNotificationById(id));

            //Assert
            Assert.Equal($"Không tìm thấy thông báo có ID {id}!", exception.Message);
        }

        [Fact]
        public async Task ChangeStatusNotification_ReturnTrue()
        {
            //Arrange
            int id = 1;

            //Action
            var result = await _notificationService.ChangeStatusNotification(id);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task ChangeStatusNotification_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            int id = 99;

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _notificationService.ChangeStatusNotification(id));

            //Assert
            Assert.Equal($"Không tìm thấy thông báo có ID {id}!", exception.Message);
        }

        [Fact]
        public async Task CreateNotification_ReturnNewIdNotification()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new NotificationCreateRequest
            {
                Content = "<div>test new</div>",
                LoaiThongBaoId = "3"
            };

            //Action
            var result = await _notificationService.CreateNotification(request);

            //Assert
            Assert.True(result > 0);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task CreateNotification_WithAnyInputMissing_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new NotificationCreateRequest
            {
                LoaiThongBaoId = "3"
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _notificationService.CreateNotification(request));

            //Assert
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", exception.Message);
        }
    }
}
