using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using XetTuyenVLU.ViewModels.AdmissionAdmin;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class AdmissionAdminServiceTest
    {
        private readonly XetTuyenVLUContext _mockContext;
        private readonly AdmissionAdminService _admissionAdminService;
        private readonly Mock<IOptions<MailSettings>> _mockMailSettings;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEvironment;

        public AdmissionAdminServiceTest()
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

                context.TpQhPxes.AddRange(
                new TpQhPx
                {
                    Id = 1,
                    TenTinhTp = "Thành phố Hồ Chí Minh",
                    MaTinhTp = "79",
                    TenQh = "Quận 1",
                    MaQh = "760",
                    TenPx = "Phường Đa Kao",
                    MaPx = "26737",
                    Cap = "Phường"
                },
                new TpQhPx
                {
                    Id = 8938,
                    TenTinhTp = "Thành phố Hồ Chí Minh",
                    MaTinhTp = "79",
                    TenQh = "Quận 7",
                    MaQh = "778",
                    TenPx = "Phường Đa Kao",
                    MaPx = "27487",
                    Cap = "Phường"
                },
                new TpQhPx
                {
                    Id = 2,
                    TenTinhTp = "Thành phố Hà Nội",
                    MaTinhTp = "01",
                    TenQh = "Quận Ba Đình",
                    MaQh = "001",
                    TenPx = "Phường Phúc Xá",
                    MaPx = "00001",
                    Cap = "Phường"
                });

                context.TruongThpts.AddRange(
                new TruongThpt
                {
                    Id = 646,
                    MaTruong = "02072",
                    MaTinhtp = "02",
                    TenTinhtp = "Tp. Hồ Chí Minh",
                    MaQh = "18",
                    TenQh = "Quận Thủ Đức",
                    MaTptruong = "072",
                    TenTruong = "THPT Thủ Đức",
                    DiaChi = "166/24 Đặng Văn Bi KP1, P.Bình Thọ, Q.Thủ Đức",
                    KhuVuc = "3",
                    LoaiTruong = "Không"
                },
                new TruongThpt
                {
                    Id = 620,
                    MaTruong = "02066",
                    MaTinhtp = "02",
                    TenTinhtp = "Tp. Hồ Chí Minh",
                    MaQh = "16",
                    TenQh = "Quận Bình Thạnh",
                    MaTptruong = "066",
                    TenTruong = "THPT Gia Định",
                    DiaChi = "195/29 Xô Viết Nghệ Tĩnh, P.17, Q.Bình Thạnh",
                    KhuVuc = "3",
                    LoaiTruong = "Không"
                });

                context.DanTocs.AddRange(
                new DanToc
                {
                    MaDantoc = "01",
                    TenDantoc = "Kinh",
                    DienGiai = "Việt"
                },
                new DanToc
                {
                    MaDantoc = "02",
                    TenDantoc = "Tày",
                    DienGiai = "Thổ, Ngạn, Phén, Thù lao, Pa Dí"
                });

                context.TonGiaos.AddRange(
                new TonGiao
                {
                    MaTongiao = "01",
                    TenTongiao = "Phật Giáo",
                    Diengiai = "Phật giáo"
                },
                new TonGiao
                {
                    MaTongiao = "02",
                    TenTongiao = "Thiên Chúa",
                    Diengiai = "Thiên Chúa"
                });

                context.QuocTiches.AddRange(
                new QuocTich
                {
                    MaQt = 704,
                    Id = 1,
                    TenQt = "Việt Nam"
                },
                new QuocTich
                {
                    MaQt = 724,
                    Id = 140,
                    TenQt = "Tây Ban Nha"
                });

                context.ChungChiNns.AddRange(
                new ChungChiNn
                {
                    Id = 1,
                    MaNn = "EN",
                    ChungChi = "Tiếng Anh - TOEFL iBT 65-78 điểm",
                    DiemQuiDoi = 8.5
                },
                new ChungChiNn
                {
                    Id = 8,
                    MaNn = "EN",
                    ChungChi = "Tiếng Anh - IELTS 6.0 điểm",
                    DiemQuiDoi = 9
                });

                context.Nganhs.AddRange(
                new Nganh
                {
                    Id = 13,
                    ManganhTohop = "7480201A00",
                    MaNganh = "7480201",
                    TenNganh = "Công nghệ Thông tin",
                    MaTohop = "A00",
                    TenTohop = "Toán, Vật lý, Hóa học",
                    Cttc = "Y",
                    Ctdb = "Y",
                },
                new Nganh
                {
                    Id = 44,
                    ManganhTohop = "7480103A01",
                    MaNganh = "7480103",
                    TenNganh = "Kỹ thuật Phần mềm",
                    MaTohop = "A01",
                    TenTohop = "Toán, Vật lý, Tiếng Anh",
                    Cttc = "Y",
                    Ctdb = null,
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

                context.HoSoThpts.Add(
                new HoSoThpt
                {
                    Id = 150014,
                    DotId = 1,
                    HoVaTen = "Nguyễn Chí Thành",
                    Email = "thanh@info.com",
                    GioiTinh = true,
                    NgaySinh = DateTime.Parse("2001-05-06"),
                    MaNoiSinh = "79",
                    TenNoiSinh = "Thành phố Hồ Chí Minh",
                    MaDanToc = "01",
                    TenDanToc = "Kinh",
                    MaTonGiao = "01",
                    TenTonGiao = "Phật Giáo",
                    Cmnd = "123123123",
                    QuocTich = "704|Việt Nam",
                    HoKhau = "123 Huỳnh Tấn Phát",
                    HoKhauMaPhuong = "27487",
                    HoKhauTenPhuong = "Phường Tân Phú",
                    HoKhauMaTinhTp = "79",
                    HoKhauTenTinhTp = "Thành phố Hồ Chí Minh",
                    HoKhauMaQh = "778",
                    HoKhauTenQh = "Quận 7",
                    NamTotNghiep = "2022",
                    SoBaoDanh = "DVL_150014",
                    HocLucLop12 = "Khá",
                    HanhKiemLop12 = "Tốt",
                    LoaiHinhTn = "THPT",
                    TruongThptMaTinhTp = "02",
                    TruongThptTenTinhTp = "Tp. Hồ Chí Minh",
                    TruongThptMaQh = "18",
                    TruongThptTenQh = "Quận Thủ Đức",
                    MaTruongThpt = "072",
                    TenTruongThpt = "THPT Thủ Đức",
                    TenLop12 = "12A3",
                    KhuVuc = "3",
                    DoiTuongUuTien = "0",
                    MaCcnn = "10",
                    Ccnn = "Tiếng Anh - IELTS 7.0 điểm - quy đổi: 9.5đ",
                    MaNganhToHop1 = "7480201#A01",
                    TenNganhTenToHop1 = "Công nghệ Thông tin#A01 - Toán, Vật lý, Tiếng Anh",
                    ChuongTrinhHoc1 = "Đặc biệt",
                    MaNganhToHop3 = "7480103#A01",
                    TenNganhTenToHop3 = "Kỹ thuật Phần mềm#A01 - Toán, Vật lý, Tiếng Anh",
                    ChuongTrinhHoc3 = "Tiêu chuẩn",
                    LienLacDiaChi = "123 Huỳnh Tấn Phát",
                    LienLacMaTp = "79",
                    LienLacTenTp = "Thành phố Hồ Chí Minh",
                    LienLacMaQh = "778",
                    LienLacTenQh = "Quận 7",
                    LienLacMaPhuongXa = "27487",
                    LienLacTenPhuongXa = "Phường Tân Phú",
                    DienThoaiDd = "0912345612",
                    DienThoaiPhuHuynh = "0912345612",
                    DateInserted = DateTime.Now,
                    DaNhanHoSo = "C"
                });

                context.ThongBao.AddRange(
                new ThongBao
                {
                    ID = 1,
                    Content = "<div>test</div>",
                    TrangThaiId = 2,
                    LoaiThongBaoId = 2,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                },
                new ThongBao
                {
                    ID = 2,
                    Content = "<div>test</div>",
                    TrangThaiId = 1,
                    LoaiThongBaoId = 2,
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

                context.HocBa.AddRange(
                new HocBa
                {
                    Id = 1,
                    MaHoSoThpt = 150014,
                    DuongDanHocBa = "FilesTest\\test1.txt"
                },
                new HocBa
                {
                    Id = 2,
                    MaHoSoThpt = 150014,
                    DuongDanHocBa = "FilesTest\\test2.txt"
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
            _mockWebHostEvironment = new Mock<IWebHostEnvironment>();
            _mockWebHostEvironment.Setup(x => x.ContentRootPath).Returns("C:\\SEP\\XetTuyenVLU\\XetTuyenVLU.UnitTest\\");
            _admissionAdminService = new AdmissionAdminService(_mockContext, _mockMailSettings.Object, _mockWebHostEvironment.Object);
        }

        [Fact]
        public async Task GetAdmissionById_ReturnAdmissionWithId()
        {
            //Arrange
            var id = 150014;

            //Action
            var result = _admissionAdminService.GetAdmissionById(id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AdmissionVM>(result);
        }

        [Fact]
        public async Task GetAdmissionById_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            var id = 999;

            //Action
            var exception = Assert.Throws<Exception>(() => _admissionAdminService.GetAdmissionById(id));

            //Assert
            Assert.Equal($"Không tìm thấy hồ sơ có ID {id}!", exception.Message);
        }

        [Fact]
        public async Task EditAdmission_ReturnTrue()
        {
            //Arrange
            var request = new AdmissionEditingRequest
            {
                Id = "150014",
                HoVaTen = "Nguyễn Văn Tèo",
                GioiTinh = "1",
                NgaySinh = "01-01-2001",
                MaNoiSinh = "79",
                MaDanToc = "01",
                MaTonGiao = "01",
                Cmnd = "123123124",
                MaQuocTich = "704",
                DiaChiHoKhau = "123 Huỳnh Tấn Phát",
                HoKhauMaTinhTp = "79",
                HoKhauMaQh = "778",
                HoKhauMaPhuong = "27487",
                NamTotNghiep = "2022",
                HocLucLop12 = "Khá",
                HanhKiemLop12 = "Tốt",
                LoaiHinhTn = "THPT",
                TruongThptMaTinhTp = "02",
                TruongThptMaQh = "18",
                MaTruongThpt = "02072",
                TenLop12 = "12A3",
                KhuVuc = "3",
                DoiTuongUuTien = "0",
                DiaChiLienLac = "100 Nguyễn Thị Minh Khai",
                LienLacMaTp = "79",
                LienLacMaQh = "778",
                LienLacMaPhuongXa = "27487",
                DienThoaiDd = "09123123123",
                Email = "teo.nguyenvan@gmail.com",
                DienThoaiPhuHuynh = "09123123123",
                PhuongAn = "1",
                DiemCnlop11 = "{\"diemtoan\":\"9.5\",\"diemvan\":\"9\",\"diemanh\":\"9\",\"diemphap\":\"9\",\"diemly\":\"9\",\"diemhoa\":\"9\",\"diemsinh\":\"9\",\"diemsu\":\"9\",\"diemdia\":\"9\",\"diemgdcd\":\"9\"}",
                DiemHk1lop12 = "{\"diemtoan\":\"8.5\",\"diemvan\":\"8\",\"diemanh\":\"8\",\"diemphap\":\"8\",\"diemly\":\"8\",\"diemhoa\":\"8\",\"diemsinh\":\"8\",\"diemsu\":\"8\",\"diemdia\":\"8\",\"diemgdcd\":\"8\"}",
                MaCcnn = "10",
                MaNganh1 = "7480103",
                MaToHop1 = "A01",
                ChuongTrinhHoc1 = "Tiêu chuẩn",
            };

            //Action
            var result = await _admissionAdminService.EditAdmission(request);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task EditAdmission_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            var request = new AdmissionEditingRequest
            {
                Id = "999",
                HoVaTen = "Nguyễn Văn Tèo",
                GioiTinh = "1",
                NgaySinh = "01-01-2001",
                MaNoiSinh = "79",
                MaDanToc = "01",
                MaTonGiao = "01",
                Cmnd = "123123124",
                MaQuocTich = "704",
                DiaChiHoKhau = "123 Huỳnh Tấn Phát",
                HoKhauMaTinhTp = "79",
                HoKhauMaQh = "778",
                HoKhauMaPhuong = "27487",
                NamTotNghiep = "2022",
                HocLucLop12 = "Khá",
                HanhKiemLop12 = "Tốt",
                LoaiHinhTn = "THPT",
                TruongThptMaTinhTp = "02",
                TruongThptMaQh = "18",
                MaTruongThpt = "02072",
                TenLop12 = "12A3",
                KhuVuc = "3",
                DoiTuongUuTien = "0",
                DiaChiLienLac = "100 Nguyễn Thị Minh Khai",
                LienLacMaTp = "79",
                LienLacMaQh = "778",
                LienLacMaPhuongXa = "27487",
                DienThoaiDd = "09123123123",
                Email = "teo.nguyenvan@gmail.com",
                DienThoaiPhuHuynh = "09123123123",
                PhuongAn = "1",
                DiemCnlop11 = "{\"diemtoan\":\"9.5\",\"diemvan\":\"9\",\"diemanh\":\"9\",\"diemphap\":\"9\",\"diemly\":\"9\",\"diemhoa\":\"9\",\"diemsinh\":\"9\",\"diemsu\":\"9\",\"diemdia\":\"9\",\"diemgdcd\":\"9\"}",
                DiemHk1lop12 = "{\"diemtoan\":\"8.5\",\"diemvan\":\"8\",\"diemanh\":\"8\",\"diemphap\":\"8\",\"diemly\":\"8\",\"diemhoa\":\"8\",\"diemsinh\":\"8\",\"diemsu\":\"8\",\"diemdia\":\"8\",\"diemgdcd\":\"8\"}",
                MaCcnn = "10",
                MaNganh1 = "7480103",
                MaToHop1 = "A01",
                ChuongTrinhHoc1 = "Tiêu chuẩn",
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _admissionAdminService.EditAdmission(request));

            //Assert
            Assert.Equal($"Không tìm thấy hồ sơ xét tuyển có ID {request.Id}!", exception.Message);
        }

        [Fact]
        public async Task GetMailBeforeSend_ReturnAdmissionMailVM()
        {
            //Arrange

            //Action
            var result = _admissionAdminService.GetMailBeforeSend();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AdmissionMailVM>(result);
        }

        [Fact]
        public async Task SendEmailForAdmission_ReturnTrue()
        {
            //Arrange
            var request = new AdmissionSendMailRequest
            {
                HoVaTen = "Thành Nguyễn",
                ToEmail = "test@gmail.com"
            };

            //Action
            var result = await _admissionAdminService.SendEmailForAdmission(request);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task ReceiveAdmissionProfileById_ReturnTrue()
        {
            //Arrange
            int id = 150014;

            //Action
            var result = _admissionAdminService.ReceiveAdmissionProfileById(id);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task ReceiveAdmissionProfileById_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            int id = 999;

            //Action
            var exception = Assert.Throws<Exception>(() => _admissionAdminService.ReceiveAdmissionProfileById(id));

            //Assert
            Assert.Equal($"Không tìm thấy hồ sơ có ID {id}!", exception.Message);
        }

        [Fact]
        public async Task DownloadAdmissionFiles_ReturnFileDownloadVM()
        {
            //Arrange
            int id = 150014;

            //Action
            var result = _admissionAdminService.DownloadAdmissionFiles(id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<FileDownloadVM>(result);
        }

        [Fact]
        public async Task DownloadAdmissionFiles_WithAdmissionIdNotExist_ThrowAnException()
        {
            //Arrange
            int id = 999;

            //Action
            var exception = Assert.Throws<Exception>(() => _admissionAdminService.DownloadAdmissionFiles(id));

            //Assert
            Assert.Equal("Thí sinh chưa upload học bạ!", exception.Message);
        }
        [Fact]
        public async Task GetDatainHoso_ReturnListData()
        {
            //Arrange

            //Action
            var data = _admissionAdminService.GetDatainHoso();

            //Assert
            Assert.NotNull(data);
            Assert.IsType<List<Hoso>>(data);
        }
        [Fact]
        public async Task DeleteDatainHosoThpt_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            var Id = 123;
            //Action
            var result = _admissionAdminService.DeleteHoSoThpts(Id);
            //Assert
            Assert.ThrowsAsync<Exception>(() => result);
        }
        [Fact]
        public async Task DeleteDatainHosoThpt_WithIdExist_ReturnTrue()
        {
            //Arrange
            var id = 1;
            //Action
            var result = await _admissionAdminService.DeleteHoSoThpts(id);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
        }
        [Fact]
        public async Task DeleteBangDieminBangDiemThpts_WithMaHoSoThptExist_ReturnTrue()
        {
            //Arrange
            var MaHoSoThpt = 1;
            //Action
            var result = await _admissionAdminService.DeleteBangDiemThpts(MaHoSoThpt);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
        }
        [Fact]
        public async Task DeleteBangDieminBangDiemThpts_WithMaHoSoThptNotExist_ThrowAnException()
        {
            //Arrange
            var MaHoSoThpt = 12;
            //Action
            var result = _admissionAdminService.DeleteBangDiemThpts(MaHoSoThpt);
            //Assert
            Assert.ThrowsAsync<Exception>(() => result);
        }

        [Fact]
        public async Task UpdateSendMailStatusById_ReturnTrue()
        {
            //Arrange
            int id = 150014;

            //Action
            var result = _admissionAdminService.UpdateSendMailStatusById(id);

            //Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task UpdateSendMailStatusById_WithIdNotExist_ThrowAnException()
        {
            //Arrange
            int id = 999;

            //Action
            var exception = Assert.Throws<Exception>(() => _admissionAdminService.UpdateSendMailStatusById(id));

            //Assert
            Assert.Equal($"Không tìm thấy hồ sơ có ID {id}!", exception.Message);
        }
    }
}
