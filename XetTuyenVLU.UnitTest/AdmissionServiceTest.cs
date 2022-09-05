using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XetTuyenVLU.Models;
using XetTuyenVLU.Services;
using XetTuyenVLU.ViewModels.Admission;
using Xunit;

namespace XetTuyenVLU.UnitTest
{
    public class AdmissionServiceTest
    {
        private readonly AdmissionService _admissionService;
        public AdmissionServiceTest()
        {
            var options = new DbContextOptionsBuilder<XetTuyenVLUContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new XetTuyenVLUContext(options))
            {
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

                context.HoSoThpts.Add(
                new HoSoThpt
                {
                    DotId = 1,
                    Id = 150014,
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

                context.Dot.AddRange(
                new Dot
                {
                    ID = 1,
                    DotThu = "1",
                    Khoa = "28",
                    NgayBatDau = DateTime.ParseExact("21/01/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("21/02/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 2,
                    NgayTao = DateTime.Now,
                    TaiKhoanId = 1
                },
                new Dot
                {
                    ID = 2,
                    DotThu = "2",
                    Khoa = "28",
                    NgayBatDau = DateTime.ParseExact("21/03/2022", "dd/MM/yyyy", null),
                    NgayKetThuc = DateTime.ParseExact("21/04/2022", "dd/MM/yyyy", null),
                    TrangThaiId = 1,
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

                context.SaveChanges();
            }
            var mockContext = new XetTuyenVLUContext(options);
            _admissionService = new AdmissionService(mockContext);
        }

        [Fact]
        public async Task GetCityProvincesForHoKhau_ReturnListCityProvinces()
        {
            //Arrange

            //Action
            var cityProvinces = _admissionService.GetCityProvincesForHoKhau();

            //Assert
            Assert.NotNull(cityProvinces);
            Assert.IsType<List<TinhThanhPhoVM>>(cityProvinces);
        }

        [Fact]
        public async Task GetDistrictsForHoKhau_ReturnListDistricts()
        {
            //Arrange
            var maTinhTP = "79";

            //Action
            var districts = _admissionService.GetDistrictsForHoKhau(maTinhTP);

            //Assert
            Assert.NotNull(districts);
            Assert.IsType<List<QuanHuyenVM>>(districts);
        }

        [Fact]
        public async Task GetDistrictsForHoKhau_WithMaTinhTPNotExist_ReturnEmptyListDistricts()
        {
            //Arrange
            var maTinhTP = "999";

            //Action
            var districts = _admissionService.GetDistrictsForHoKhau(maTinhTP);

            //Assert
            Assert.Equal(0, districts.Count);
            Assert.IsType<List<QuanHuyenVM>>(districts);
        }

        [Fact]
        public async Task GetWardsForHoKhau_ReturnListWards()
        {
            //Arrange
            var maQH = "001";

            //Action
            var wards = _admissionService.GetWardsForHoKhau(maQH);

            //Assert
            Assert.NotNull(wards);
            Assert.IsType<List<PhuongXaVM>>(wards);
        }

        [Fact]
        public async Task GetWardsForHoKhau_WithMaQHNotExist_ReturnEmptyListWards()
        {
            //Arrange
            var maQH = "111";

            //Action
            var wards = _admissionService.GetWardsForHoKhau(maQH);

            //Assert
            Assert.Equal(0, wards.Count);
            Assert.IsType<List<PhuongXaVM>>(wards);
        }

        [Fact]
        public async Task GetCityProvincesForSchool_ReturnListCityProvinces()
        {
            //Arrange

            //Action
            var cityProvinces = _admissionService.GetCityProvincesForSchool();

            //Assert
            Assert.NotNull(cityProvinces);
            Assert.IsType<List<TinhThanhPhoVM>>(cityProvinces);
        }

        [Fact]
        public async Task GetDistrictsForSchool_ReturnListDistricts()
        {
            //Arrange
            var maTinhTP = "02";

            //Action
            var districts = _admissionService.GetDistrictsForSchool(maTinhTP);

            //Assert
            Assert.NotNull(districts);
            Assert.IsType<List<QuanHuyenVM>>(districts);
        }

        [Fact]
        public async Task GetDistrictsForSchool_WithMaTinhTPNotExist_ReturnEmptyListDistricts()
        {
            //Arrange
            var maTinhTP = "999";

            //Action
            var districts = _admissionService.GetDistrictsForSchool(maTinhTP);

            //Assert
            Assert.Equal(0, districts.Count);
            Assert.IsType<List<QuanHuyenVM>>(districts);
        }

        [Fact]
        public async Task GetSchools_ReturnListSchools()
        {
            //Arrange
            var maTinhTP = "02";
            var maQH = "18";

            //Action
            var schools = _admissionService.GetSchools(maTinhTP, maQH);

            //Assert
            Assert.NotNull(schools);
            Assert.IsType<List<TruongTHPTVM>>(schools);
        }

        [Fact]
        public async Task GetSchools_WithMaTinhTPOrMaQHNotExist_ReturnEmptyListSchools()
        {
            //Arrange
            var maTinhTP = "02";
            var maQH = "99";

            //Action
            var schools = _admissionService.GetSchools(maTinhTP, maQH);

            //Assert
            Assert.Equal(0, schools.Count);
            Assert.IsType<List<TruongTHPTVM>>(schools);
        }

        [Fact]
        public async Task GetEthnics_ReturnListEthnics()
        {
            //Arrange

            //Action
            var ethnics = _admissionService.GetEthnics();

            //Assert
            Assert.NotNull(ethnics);
            Assert.IsType<List<DanToc>>(ethnics);
        }

        [Fact]
        public async Task GetReligions_ReturnListReligions()
        {
            //Arrange

            //Action
            var religions = _admissionService.GetReligions();

            //Assert
            Assert.NotNull(religions);
            Assert.IsType<List<TonGiao>>(religions);
        }

        [Fact]
        public async Task GetNationalities_ReturnListNationalities()
        {
            //Arrange

            //Action
            var nationalities = _admissionService.GetNationalities();

            //Assert
            Assert.NotNull(nationalities);
            Assert.IsType<List<QuocTich>>(nationalities);
        }

        [Fact]
        public async Task GetCertificateLanguages_ReturnListCertificate()
        {
            //Arrange

            //Action
            var certificate = _admissionService.GetCertificateLanguages();

            //Assert
            Assert.NotNull(certificate);
            Assert.IsType<List<ChungChiNn>>(certificate);
        }

        [Fact]
        public async Task GetNganhXetTuyen_ReturnListNganhs()
        {
            //Arrange

            //Action
            var nganhs = _admissionService.GetNganhXetTuyen();

            //Assert
            Assert.NotNull(nganhs);
            Assert.IsType<List<NganhXetTuyenVM>>(nganhs);
        }

        [Fact]
        public async Task GetToHopXetTuyen_ReturnListToHops()
        {
            //Arrange
            var maNganh = "7480201";

            //Action
            var toHops = _admissionService.GetToHopXetTuyen(maNganh);

            //Assert
            Assert.NotNull(toHops);
            Assert.IsType<List<ToHopXetTuyenVM>>(toHops);
        }

        [Fact]
        public async Task GetToHopXetTuyen_WithMaNganhNotExist_ReturnEmptyListToHops()
        {
            //Arrange
            var maNganh = "9999999";

            //Action
            var toHops = _admissionService.GetToHopXetTuyen(maNganh);

            //Assert
            Assert.Equal(0, toHops.Count);
            Assert.IsType<List<ToHopXetTuyenVM>>(toHops);
        }

        [Fact]
        public async Task ValidateCMND_WithCMNDExist_ReturnTrue()
        {
            //Arrange
            var cmnd = "123123123";

            //Action
            var result = _admissionService.ValidateCMND(cmnd);

            //Assert
            Assert.Equal(true, result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ValidateCMND_WithCMNDNotExist_ReturnFalse()
        {
            //Arrange
            var cmnd = "99999999";

            //Action
            var result = _admissionService.ValidateCMND(cmnd);

            //Assert
            Assert.Equal(false, result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateAdmission_ReturnSoBaoDanhForNewAdmission()
        {
            //Arrange
            var request = new AdmissionCreateRequest
            {
                DotId = "1",
                hovaten = "Nguyễn Văn Tèo",
                gioitinh = "1",
                ngaysinh = "01-01-2001",
                noisinh = "79",
                dantoc = "01",
                tongiao = "01",
                cmnd = "123123124",
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
            var result = await _admissionService.CreateAdmission(request);

            //Assert
            Assert.True(result.Contains("DVL_"));
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task CreateAdmission_WithNoiSinhNotExist_ThrowAnException()
        {
            //Arrange
            var request = new AdmissionCreateRequest
            {
                DotId = "1",
                hovaten = "Nguyễn Văn Tèo",
                gioitinh = "1",
                ngaysinh = "01-01-2001",
                noisinh = "9999",
                dantoc = "01",
                tongiao = "01",
                cmnd = "123123124",
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
            var exception = await Assert.ThrowsAsync<Exception>(() => _admissionService.CreateAdmission(request));

            //Assert
            Assert.Equal("There are some incorrect data!", exception.Message);
        }

        [Fact]
        public async Task CreateAdmission_WithQuocTichNotExist_ThrowAnException()
        {
            //Arrange
            var request = new AdmissionCreateRequest
            {
                DotId = "1",
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
            var exception = await Assert.ThrowsAsync<Exception>(() => _admissionService.CreateAdmission(request));

            //Assert
            Assert.Equal("There are some incorrect data!", exception.Message);
        }

        [Fact]
        public async Task GetPhase_ReturnPhase()
        {
            //Arrange

            //Action
            var phase = _admissionService.GetPhase();

            //Assert
            Assert.NotNull(phase);
            Assert.IsType<DotXetTuyenVM>(phase);
        }

         [Fact]
        public async Task GetScheduleForEditProfile_ReturnScheduleForEditProfile()
        {
            //Arrange

            //Action
            var phase = _admissionService.GetScheduleForEditProfile();

            //Assert
            Assert.NotNull(phase);
            Assert.IsType<LichTrinhVM>(phase);
        }
    }
}
