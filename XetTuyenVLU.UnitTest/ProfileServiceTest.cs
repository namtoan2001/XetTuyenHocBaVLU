using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XetTuyenVLU.Models;
using XetTuyenVLU.Services;
using XetTuyenVLU.ViewModels.Profile;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class ProfileServiceTest
    {
        private readonly ProfileService _profileService;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEvironment;
        public ProfileServiceTest()
        {
            var options = new DbContextOptionsBuilder<XetTuyenVLUContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            using (var context = new XetTuyenVLUContext(options))
            {
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
                context.BangDiemThpts.Add(
                    new BangDiemThpt
                    {
                        MaHoSoThpt = 150015,
                        MaHocKyLop = "CN_LOP12",
                        Toan = 9,
                        Van = 9,
                        Anh = 9,
                        Phap = 9,
                        Ly = 9,
                        Hoa = 9,
                        Sinh = 9,
                        Su = 9,
                        Dia = 9,
                        Gdcd = 9,
                    });
                context.BangDiemThpts.Add(
                    new BangDiemThpt
                    {
                        MaHoSoThpt = 150016,
                        MaHocKyLop = "CN_LOP11",
                        Toan = 9,
                        Van = 9,
                        Anh = 9,
                        Phap = 9,
                        Ly = 9,
                        Hoa = 9,
                        Sinh = 9,
                        Su = 9,
                        Dia = 9,
                        Gdcd = 9,
                    });
                context.BangDiemThpts.Add(
                    new BangDiemThpt
                    {
                        MaHoSoThpt = 150016,
                        MaHocKyLop = "HK1_LOP12",
                        Toan = 9,
                        Van = 9,
                        Anh = 9,
                        Phap = 9,
                        Ly = 9,
                        Hoa = 9,
                        Sinh = 9,
                        Su = 9,
                        Dia = 9,
                        Gdcd = 9,
                    });
                context.HoSoThpts.AddRange(
                new HoSoThpt
                {
                    Id = 150015,
                    HoVaTen = "Bùi Quốc Đạt",
                    Email = "dat@info.com",
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
                    DaNhanHoSo = "C",
                    DotId = 1
                },
                new HoSoThpt
                {
                    Id = 150016,
                    HoVaTen = "AAAAA",
                    Email = "dat@info.com",
                    GioiTinh = true,
                    NgaySinh = DateTime.Parse("2001-05-06"),
                    MaNoiSinh = "79",
                    TenNoiSinh = "Thành phố Hồ Chí Minh",
                    MaDanToc = "01",
                    TenDanToc = "Kinh",
                    MaTonGiao = "01",
                    TenTonGiao = "Phật Giáo",
                    Cmnd = "123123121",
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
                    DaNhanHoSo = "C",
                    DotId = 1
                });

                context.SaveChanges();
                var mockContext = new XetTuyenVLUContext(options);
                _mockWebHostEvironment = new Mock<IWebHostEnvironment>();
                _mockWebHostEvironment.Setup(x => x.ContentRootPath).Returns("C:\\SEP\\XetTuyenVLU\\XetTuyenVLU.UnitTest\\");
                _profileService = new ProfileService(mockContext, _mockWebHostEvironment.Object);
            }



        }

        [Fact]
        public async Task GetBangDiem_WithOneBang_ReturnBangDiemWithOneBang()
        {
            //Arrange
            var maHoSo = 150015;

            //Action

            var bangdiems = _profileService.GetBangDiem(maHoSo);

            //Assert

            Assert.Equal(1, bangdiems.Count);
            Assert.IsType<List<BangDiemThpt>>(bangdiems);

        }


        [Fact]
        public async Task GetBangDiem_WithTwoBang_ReturnBangDiemWithTwoBang()
        {
            //Arrange
            var maHoSo = 150016;

            //Action

            var bangdiems = _profileService.GetBangDiem(maHoSo);

            //Assert

            Assert.Equal(2, bangdiems.Count);
            Assert.IsType<List<BangDiemThpt>>(bangdiems);

        }

        [Fact]
        public async Task GetBangDiem_WithNotCorrectMaHoSo_ShouldReturnEmpty()
        {
            //Arrange
            var maHoSo = 150000;

            //Action

            var bangdiems = _profileService.GetBangDiem(maHoSo);

            //Assert

            Assert.Equal(0, bangdiems.Count);
            Assert.IsType<List<BangDiemThpt>>(bangdiems);

        }

        [Fact]
        public async Task GetProfileByCMND_WithCorrectCMNDAndDot_ShouldReturnProfile()
        {
            //Arrange
            var cmnd = "123123123";
            int dot = 1;

            //Action

            var hoso = _profileService.GetProfileByCMND(cmnd, dot);

            //Assert

            Assert.NotNull(hoso);
            Assert.IsType<HoSoThpt>(hoso);

        }

        [Fact]
        public async Task GetProfileByCMND_WithIncorrectCMND_ShouldReturnEmpty()
        {
            //Arrange
            var cmnd = "10000";
            var dot = 3;
            //Action
            var hoso = _profileService.GetProfileByCMND(cmnd, dot);
            //Assert
            Assert.Null(hoso);
        }

        [Fact]
        public async Task GetProfileByCMND_WithIncorrectDotButCorrectCmnd_ShouldReturnEmpty()
        {
            //Arrange
            var cmnd = "123123123";
            var dot = 3;
            //Action
            var hoso = _profileService.GetProfileByCMND(cmnd, dot);
            //Assert
            Assert.Null(hoso);
        }

        [Fact]
        public async Task EditProfile_WithCorrectInPut_ShouldReturnTrue()
        {
            //Arrange
            var request = new EditProfileRequest
            {
                id = "150015",
                hovaten = "Nguyễn Văn Tèo",
                gioitinh = "1",
                ngaysinh = "01-01-2001",
                noisinh = "79",
                dantoc = "01",
                tongiao = "01",
                cmnd = "123123123",
                quoctich = 704,
                hokhauthuongtru = "123 Huỳnh Tấn Phát",
                tinhthanhpho = "79",
                quanhuyen = "778",
                phuongxa = "27487",
                namtotnghiep = "2022",
                hocluclop12 = "Khá",
                hanhkiemlop12 = "Tốt",
                hocchuongtrinh = "THPT",
                tinhthanhpho_thpt = "02",
                quanhuyen_thpt = "18",
                tentruongthpt = "02072",
                tenlop12 = "12A3",
                khuvucuutien = "3",
                doituonguutien = "0",
                diachinha = "100 Nguyễn Thị Minh Khai",
                tinhthanhpho_nha = "79",
                quanhuyen_nha = "778",
                phuongxa_nha = "27487",
                dienthoaididong = "09123123123",
                email = "teo.nguyenvan@gmail.com",
                dienthoaiphuhuynh = "09123123123",
                phuongan = "1",
                diemtb_cnlop11 = "{\"diemtoan\":\"9.5\",\"diemvan\":\"9\",\"diemanh\":\"9\",\"diemphap\":\"9\",\"diemly\":\"9\",\"diemhoa\":\"9\",\"diemsinh\":\"9\",\"diemsu\":\"9\",\"diemdia\":\"9\",\"diemgdcd\":\"9\"}",
                diemtb_hk1lop12 = "{\"diemtoan\":\"8.5\",\"diemvan\":\"8\",\"diemanh\":\"8\",\"diemphap\":\"8\",\"diemly\":\"8\",\"diemhoa\":\"8\",\"diemsinh\":\"8\",\"diemsu\":\"8\",\"diemdia\":\"8\",\"diemgdcd\":\"8\"}",
                chungchingoaingu = "10",
                nganh1 = "7480103",
                tohopmon1 = "A01",
                chuongtrinh1 = "Tiêu chuẩn",

            };

            //Action
            var result = await _profileService.EditProfile(request);

            //Assert
            Assert.Equal(true, result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task EditProfile_WithInCorrectID_ShouldThrowException()
        {
            //Arrange
            var request = new EditProfileRequest
            {
                id = "150011",
                hovaten = "Nguyễn Văn Tèo",
                gioitinh = "1",
                ngaysinh = "01-01-2001",
                noisinh = "79",
                dantoc = "01",
                tongiao = "01",
                cmnd = "123123123",
                quoctich = 704,
                hokhauthuongtru = "123 Huỳnh Tấn Phát",
                tinhthanhpho = "79",
                quanhuyen = "778",
                phuongxa = "27487",
                namtotnghiep = "2022",
                hocluclop12 = "Khá",
                hanhkiemlop12 = "Tốt",
                hocchuongtrinh = "THPT",
                tinhthanhpho_thpt = "02",
                quanhuyen_thpt = "18",
                tentruongthpt = "02072",
                tenlop12 = "12A3",
                khuvucuutien = "3",
                doituonguutien = "0",
                diachinha = "100 Nguyễn Thị Minh Khai",
                tinhthanhpho_nha = "79",
                quanhuyen_nha = "778",
                phuongxa_nha = "27487",
                dienthoaididong = "09123123123",
                email = "teo.nguyenvan@gmail.com",
                dienthoaiphuhuynh = "09123123123",
                phuongan = "1",
                diemtb_cnlop11 = "{\"diemtoan\":\"9.5\",\"diemvan\":\"9\",\"diemanh\":\"9\",\"diemphap\":\"9\",\"diemly\":\"9\",\"diemhoa\":\"9\",\"diemsinh\":\"9\",\"diemsu\":\"9\",\"diemdia\":\"9\",\"diemgdcd\":\"9\"}",
                diemtb_hk1lop12 = "{\"diemtoan\":\"8.5\",\"diemvan\":\"8\",\"diemanh\":\"8\",\"diemphap\":\"8\",\"diemly\":\"8\",\"diemhoa\":\"8\",\"diemsinh\":\"8\",\"diemsu\":\"8\",\"diemdia\":\"8\",\"diemgdcd\":\"8\"}",
                chungchingoaingu = "10",
                nganh1 = "7480103",
                tohopmon1 = "A01",
                chuongtrinh1 = "Tiêu chuẩn",

            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _profileService.EditProfile(request));

            //Assert
            Assert.Equal("Không tìm thấy hồ sơ với ID 150011!", exception.Message);
        }

        [Fact]
        public async Task EditProfile_WithInCorrectData_ShouldThrowException()
        {
            //Arrange
            var request = new EditProfileRequest
            {
                hovaten = "Nguyễn Văn Tèo",
                gioitinh = "1",
                ngaysinh = "01-01-2001",
                noisinh = "79",
                dantoc = "01",
                tongiao = "01",
                cmnd = "123123124",
                quoctich = 9999,
                hokhauthuongtru = "123 Huỳnh Tấn Phát",
                tinhthanhpho = "79",
                quanhuyen = "778",
                phuongxa = "27487",
                namtotnghiep = "2022",
                hocluclop12 = "Khá",
                hanhkiemlop12 = "Tốt",
                hocchuongtrinh = "THPT",
                tinhthanhpho_thpt = "02",
                quanhuyen_thpt = "18",
                tentruongthpt = "02072",
                tenlop12 = "12A3",
                khuvucuutien = "3",
                doituonguutien = "0",
                diachinha = "100 Nguyễn Thị Minh Khai",
                tinhthanhpho_nha = "79",
                quanhuyen_nha = "778",
                phuongxa_nha = "27487",
                dienthoaididong = "09123123123",
                email = "teo.nguyenvan@gmail.com",
                dienthoaiphuhuynh = "09123123123",
                phuongan = "1",
                diemtb_cnlop11 = "{\"diemtoan\":\"9.5\",\"diemvan\":\"9\",\"diemanh\":\"9\",\"diemphap\":\"9\",\"diemly\":\"9\",\"diemhoa\":\"9\",\"diemsinh\":\"9\",\"diemsu\":\"9\",\"diemdia\":\"9\",\"diemgdcd\":\"9\"}",
                diemtb_hk1lop12 = "{\"diemtoan\":\"8.5\",\"diemvan\":\"8\",\"diemanh\":\"8\",\"diemphap\":\"8\",\"diemly\":\"8\",\"diemhoa\":\"8\",\"diemsinh\":\"8\",\"diemsu\":\"8\",\"diemdia\":\"8\",\"diemgdcd\":\"8\"}",
                chungchingoaingu = "10",
                nganh1 = "7480103",
                tohopmon1 = "A01",
                chuongtrinh1 = "Tiêu chuẩn",
            };

            //Action
            var exception = await Assert.ThrowsAsync<Exception>(() => _profileService.EditProfile(request));

            //Assert
            Assert.Equal("There are some incorrect data!", exception.Message);
        }

        [Fact]
        public async Task ValidateCMNDToEdit_WithCorrectCmnd_ShouldReturnFalse()
        {
            //Arrange
            var cmnd = "123123124";
            var currentCmnd = "123123121";

            //Action
            var isExist = _profileService.ValidateCMNDToEdit(cmnd, currentCmnd);
            //Assert
            Assert.Equal(false, isExist);
        }

        [Fact]
        public async Task ValidateCMNDToEdit_WithAlreadyHaveCmnd_ShouldReturnTrue()
        {
            //Arrange
            var cmnd = "123123121";
            var currentCmnd = "123123124";

            //Action
            var isExist = _profileService.ValidateCMNDToEdit(cmnd, currentCmnd);
            //Assert
            Assert.Equal(true, isExist);
        }
        [Fact]
        public async Task UpLoadHocBa_ReturnTrue()
        {
            //Arrange
            IList<IFormFile> file = null;
            var hocba = new AddImgPath
            {
                MaHoSoThpt = 1,
                imgFile = file
            };
            //Action
            var result = await _profileService.AddImgPathHocBa(hocba);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
        }

    }
}
