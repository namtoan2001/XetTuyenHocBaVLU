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
using XetTuyenVLU.ViewModels.Schedule;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class ScheduleServiceTest
    {
        private readonly XetTuyenVLUContext _mockContext;
        private readonly ScheduleService _scheduleService;
        private readonly MockJwtService _mockJwtService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public ScheduleServiceTest()
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
                    TrangThaiId = 1,
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
                    TrangThaiId = 2,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                });

                context.LichTrinh.AddRange(
                new LichTrinh
                {
                    ID = 1,
                    MaDot = 1,
                    NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 1,
                    HinhThucId = 1,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                },
                new LichTrinh
                {
                    ID = 2,
                    MaDot = 1,
                    NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 1,
                    HinhThucId = 2,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                },
                new LichTrinh
                {
                    ID = 3,
                    MaDot = 1,
                    NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 2,
                    HinhThucId = 1,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                });

                context.LoaiHinhThuc.AddRange(
                new LoaiHinhThuc
                {
                    ID = 1,
                    TenHinhThuc = "test1"
                },
                new LoaiHinhThuc
                {
                    ID = 2,
                    TenHinhThuc = "test2"
                });

                context.SaveChanges();
            }
            _mockContext = new XetTuyenVLUContext(options);
            _mockJwtService = new MockJwtService();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _scheduleService = new ScheduleService(_mockContext, _mockJwtService, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task GetAllSchedules_ReturnListSchedule()
        {
            //Arrange

            //Action
            var result = _scheduleService.GetAllSchedules();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<ScheduleVM>>(result);
        }

        [Fact]
        public async Task GetCategoriesForSchedule_ReturnListCategories()
        {
            //Arrange

            //Action
            var result = _scheduleService.GetCategoriesForSchedule();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<LoaiHinhThuc>>(result);
        }

        [Fact]
        public async Task GetAllPhasesNotExpiry_ReturnListPhase()
        {
            //Arrange

            //Action
            var result = _scheduleService.GetAllPhasesNotExpiry();

            //Assert
            Assert.True(result.Count > 0);
            Assert.IsType<List<Dot>>(result);
        }

        [Fact]
        public async Task ChangeStatusSchedule_ReturnTrue()
        {
            //Arrange
            int id = 3;

            //Action
            var result = await _scheduleService.ChangeStatusSchedule(id);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task ChangeStatusSchedule_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            int id = 99;

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _scheduleService.ChangeStatusSchedule(id));

            //Assert
            Assert.Equal($"Không tìm thấy lịch trình có ID {id}!", exception.Message);
        }

        [Fact]
        public async Task CreateSchedule_ReturnNewIdSchedule()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ScheduleCreateRequest
            {
                DotId = "1",
                HinhThucId = "1",
                NgayBatDau = DateTime.ParseExact("30/11/2022", "dd/MM/yyyy", null),
                NgayKetThuc = DateTime.ParseExact("30/12/2022", "dd/MM/yyyy", null)
            };

            //Action
            var result = await _scheduleService.CreateSchedule(request);

            //Assert
            Assert.True(result > 0);
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task CreateSchedule_WithAnyInputMissing_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ScheduleCreateRequest
            {
                DotId = "1",
                HinhThucId = "1",
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _scheduleService.CreateSchedule(request));

            //Assert
            Assert.Equal("Vui lòng nhập đầy đủ thông tin!", exception.Message);
        }

        [Fact]
        public async Task CreateSchedule_WithTimeInvalid_ThrowAnException()
        {
            //Arrange
            var context = new DefaultHttpContext();
            var taiKhoan = await _mockContext.TaiKhoan.FirstOrDefaultAsync(x => x.ID == 1);
            string token = "Bearer " + _mockJwtService.GenarateJwt(taiKhoan);
            context.Request.Headers["Authorization"] = token;
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
            var request = new ScheduleCreateRequest
            {
                DotId = "1",
                HinhThucId = "1",
                NgayBatDau = DateTime.ParseExact("30/11/2023", "dd/MM/yyyy", null),
                NgayKetThuc = DateTime.ParseExact("30/12/2024", "dd/MM/yyyy", null)
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _scheduleService.CreateSchedule(request));

            //Assert
            Assert.Equal("Thời gian phải nằm trong đợt xét tuyển!", exception.Message);
        }
    }
}
