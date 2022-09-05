using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XetTuyenVLU.Helpers;
using XetTuyenVLU.Models;
using XetTuyenVLU.Services;
using XetTuyenVLU.SettingsForMail;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class MailServiceTest
    {
        private readonly XetTuyenVLUContext _mockContext;
        private readonly MailService _mailService;
        private readonly Mock<IOptions<MailSettings>> _mockMailSettings;

        public MailServiceTest()
        {
            var options = new DbContextOptionsBuilder<XetTuyenVLUContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new XetTuyenVLUContext(options))
            {
                context.ThongBao.AddRange(
                new ThongBao
                {
                    ID = 1,
                    Content = "<div>test</div>",
                    TrangThaiId = 1,
                    LoaiThongBaoId = 1,
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
            _mockMailSettings = new Mock<IOptions<MailSettings>>();
            var mailSetting = new MailSettings
            {
                Mail = "xettuyenvlu@gmail.com",
                DisplayName = "Trường đại học Văn Lang",
                Password = "hgagbxtmaeedjjvp",
                Host = "smtp.gmail.com",
                Port = 465
            };
            _mockMailSettings.Setup(x => x.Value).Returns(mailSetting);
            _mailService = new MailService(_mockContext, _mockMailSettings.Object);
        }

        [Fact]
        public async Task SendEmailAsync_ReturnVoid()
        {
            //Arrange
            var request = new MailRequest
            {
                AdmissionCode = "VL_123456",
                FullName = "Thành Nguyễn",
                ToEmail = "test@gmail.com"
            };

            //Action
            var result = await _mailService.SendEmailAsync(request);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }
    }
}
