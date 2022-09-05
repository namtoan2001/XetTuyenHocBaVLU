using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Admission;
using XetTuyenVLU.ViewModels.Notification;

namespace XetTuyenVLU.Services
{
    public class AdmissionService : IAdmissionService
    {
        private readonly XetTuyenVLUContext _context;

        public AdmissionService(XetTuyenVLUContext context)
        {
            _context = context;
        }

        public List<TinhThanhPhoVM> GetCityProvincesForHoKhau()
        {
            var cityProvinces = _context.TpQhPxes
                .OrderBy(x => x.TenTinhTp)
                .ToList()
                .DistinctBy(x => x.MaTinhTp)
                .Select(c => new TinhThanhPhoVM
                {
                    MaTinhTP = c.MaTinhTp,
                    TenTinhTP = c.TenTinhTp

                }).ToList();
            return cityProvinces;
        }

        public List<QuanHuyenVM> GetDistrictsForHoKhau(string MaTinhTP)
        {
            var districts = _context.TpQhPxes
                .OrderBy(x => x.TenQh)
                .Where(x => x.MaTinhTp == MaTinhTP)
                .ToList()
                .DistinctBy(x => x.MaQh);
            if (districts == null)
                throw new Exception($"Cannot find districts with MaTinhTP {MaTinhTP}");
            var districtList = new List<QuanHuyenVM>();
            foreach (var district in districts)
            {
                districtList.Add(new QuanHuyenVM
                {
                    MaQH = district.MaQh,
                    TenQH = district.TenQh
                });
            }
            return districtList;
        }

        public List<PhuongXaVM> GetWardsForHoKhau(string MaQH)
        {
            var wards = _context.TpQhPxes
                .OrderBy(x => x.TenPx)
                .Where(x => x.MaQh == MaQH);
            if (wards == null)
                throw new Exception($"Cannot find wards with MaQH {MaQH}");
            var wardList = new List<PhuongXaVM>();

            foreach (var ward in wards)
            {
                wardList.Add(new PhuongXaVM
                {
                    MaPX = ward.MaPx,
                    TenPX = ward.TenPx,
                    Cap = ward.Cap
                });
            }
            return wardList;
        }

        public List<TinhThanhPhoVM> GetCityProvincesForSchool()
        {
            var cityProvinces = _context.TruongThpts
                .OrderBy(x => x.TenTinhtp)
                .ToList()
                .DistinctBy(x => x.MaTinhtp)
                .Select(c => new TinhThanhPhoVM
                {
                    MaTinhTP = c.MaTinhtp,
                    TenTinhTP = c.TenTinhtp

                }).ToList();
            return cityProvinces;
        }

        public List<QuanHuyenVM> GetDistrictsForSchool(string MaTinhTP)
        {
            var districts = _context.TruongThpts
                .OrderBy(x => x.TenQh)
                .Where(x => x.MaTinhtp == MaTinhTP)
                .ToList()
                .DistinctBy(x => x.MaQh);
            if (districts == null)
                throw new Exception($"Cannot find districts with MaTinhTP {MaTinhTP}");
            var districtList = new List<QuanHuyenVM>();
            foreach (var district in districts)
            {
                districtList.Add(new QuanHuyenVM
                {
                    MaQH = district.MaQh,
                    TenQH = district.TenQh
                });
            }
            return districtList;
        }

        public List<TruongTHPTVM> GetSchools(string MaTinhTP, string MaQH)
        {
            var schools = _context.TruongThpts.Where(x => x.MaTinhtp == MaTinhTP && x.MaQh == MaQH);
            if (schools == null)
                throw new Exception($"Cannot find schools with MaTinhTP {MaTinhTP} and MaQH {MaQH}");
            var schoolList = new List<TruongTHPTVM>();

            foreach (var school in schools)
            {
                schoolList.Add(new TruongTHPTVM
                {
                    MaTruong = school.MaTruong,
                    TenTruong = school.TenTruong
                });
            }
            return schoolList;
        }

        public List<DanToc> GetEthnics()
        {
            var ethnics = _context.DanTocs.ToList();
            return ethnics;
        }

        public List<TonGiao> GetReligions()
        {
            var religions = _context.TonGiaos.ToList();
            return religions;
        }

        public List<QuocTich> GetNationalities()
        {
            var nationalities = _context.QuocTiches.OrderBy(x => x.TenQt).ToList();
            return nationalities;
        }

        public List<ChungChiNn> GetCertificateLanguages()
        {
            var certificates = _context.ChungChiNns.OrderBy(x => x.ChungChi).ToList();
            return certificates;
        }

        public List<NganhXetTuyenVM> GetNganhXetTuyen()
        {
            var nganhs = _context.Nganhs
                .OrderBy(x => x.TenNganh)
                .ToList()
                .DistinctBy(x => x.MaNganh)
                .Select(n => new NganhXetTuyenVM
                {
                    MaNganh = n.MaNganh,
                    TenNganh = n.TenNganh,
                    CTDB = n.Ctdb

                }).ToList();
            return nganhs;
        }

        public List<ToHopXetTuyenVM> GetToHopXetTuyen(string MaNganh)
        {
            var toHops = _context.Nganhs
                .OrderBy(x => x.MaTohop)
                .Where(x => x.MaNganh == MaNganh)
                .ToList()
                .DistinctBy(t => t.MaTohop);
            if (toHops == null)
                throw new Exception($"Cannot find To Hop Xet Tuyen with MaNganh {MaNganh}");
            var toHopList = new List<ToHopXetTuyenVM>();
            foreach (var toHop in toHops)
            {
                toHopList.Add(new ToHopXetTuyenVM
                {
                    MaToHop = toHop.MaTohop,
                    TenToHop = toHop.TenTohop,
                });
            }
            return toHopList;
        }

        public bool ValidateCMND(string cmnd)
        {
            var phase = _context.Dot.FirstOrDefault(x => x.TrangThaiId == 1);
            var isExist = _context.HoSoThpts.Any(x => x.Cmnd == cmnd && x.DotId == phase.ID);
            return isExist;
        }

        public async Task<string> CreateAdmission(AdmissionCreateRequest request)
        {
            var diemtb_cnlop11 = request.diemtb_cnlop11 != null ? JsonConvert.DeserializeObject<BangDiemVM>(request.diemtb_cnlop11) : null;
            var diemtb_hk1lop12 = request.diemtb_hk1lop12 != null ? JsonConvert.DeserializeObject<BangDiemVM>(request.diemtb_hk1lop12) : null;
            var diemtb_cnlop12 = request.diemtb_cnlop12 != null ? JsonConvert.DeserializeObject<BangDiemVM>(request.diemtb_cnlop12) : null;
            var noiSinh = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaTinhTp == request.noisinh);
            var danToc = await _context.DanTocs.FirstOrDefaultAsync(x => x.MaDantoc == request.dantoc);
            var tonGiao = await _context.TonGiaos.FirstOrDefaultAsync(x => x.MaTongiao == request.tongiao);
            var quocTich = await _context.QuocTiches.FirstOrDefaultAsync(x => x.MaQt == request.quoctich);
            var chungChiNgoaiNgu = await _context.ChungChiNns.FirstOrDefaultAsync(x => x.Id.ToString() == request.chungchingoaingu);
            //Ho khau
            var tinhTP = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaTinhTp == request.tinhthanhpho);
            var quanHuyen = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaQh == request.quanhuyen);
            var phuongXa = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaPx == request.phuongxa);
            //Truong THPT
            var tinhTP_THPT = await _context.TruongThpts.FirstOrDefaultAsync(x => x.MaTinhtp == request.tinhthanhpho_thpt);
            var quanHuyen_THPT = await _context.TruongThpts.FirstOrDefaultAsync(x => x.MaQh == request.quanhuyen_thpt);
            var truongTHPT = await _context.TruongThpts.FirstOrDefaultAsync(x =>
                                                                            x.MaTinhtp == request.tinhthanhpho_thpt &&
                                                                            x.MaQh == request.quanhuyen_thpt &&
                                                                            x.MaTruong == request.tentruongthpt);
            //Lien he
            var tinhTP_LienHe = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaTinhTp == request.tinhthanhpho_nha);
            var quanHuyen_LienHe = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaQh == request.quanhuyen_nha);
            var phuongXa_LienHe = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaPx == request.phuongxa_nha);
            //Nguyen vong 1
            var nganh1 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaNganh == request.nganh1);
            var toHopMon1 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaTohop == request.tohopmon1);
            //Nguyen vong 2
            var nganh2 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaNganh == request.nganh2);
            var toHopMon2 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaTohop == request.tohopmon2);
            //Nguyen vong 3
            var nganh3 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaNganh == request.nganh3);
            var toHopMon3 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaTohop == request.tohopmon3);

            if (request.DotId == null ||
                noiSinh == null ||
                danToc == null ||
                tonGiao == null ||
                quocTich == null ||
                tinhTP == null ||
                quanHuyen == null ||
                phuongXa == null ||
                tinhTP_THPT == null ||
                quanHuyen_THPT == null ||
                truongTHPT == null ||
                tinhTP_LienHe == null |
                quanHuyen_LienHe == null ||
                phuongXa_LienHe == null ||
                nganh1 == null ||
                toHopMon1 == null)
            {
                throw new Exception("There are some incorrect data!");
            }

            var admission = new HoSoThpt()
            {
                DotId = Int32.Parse(request.DotId),
                HoVaTen = request.hovaten,
                Email = request.email,
                GioiTinh = request.gioitinh == "1" ? true : false,
                NgaySinh = DateTime.Parse(request.ngaysinh),
                MaNoiSinh = request.noisinh,
                TenNoiSinh = noiSinh?.TenTinhTp,
                MaDanToc = request.dantoc,
                TenDanToc = danToc?.TenDantoc,
                MaTonGiao = request.tongiao,
                TenTonGiao = tonGiao?.TenTongiao,
                Cmnd = request.cmnd,
                QuocTich = request.quoctich + "|" + quocTich?.TenQt,
                HoKhau = request.hokhauthuongtru,
                HoKhauMaTinhTp = request.tinhthanhpho,
                HoKhauTenTinhTp = tinhTP?.TenTinhTp,
                HoKhauMaQh = request.quanhuyen,
                HoKhauTenQh = quanHuyen?.TenQh,
                HoKhauMaPhuong = request.phuongxa,
                HoKhauTenPhuong = phuongXa?.TenPx,
                NamTotNghiep = request.namtotnghiep,
                HocLucLop12 = request.hocluclop12,
                HanhKiemLop12 = request.hanhkiemlop12,
                LoaiHinhTn = request.hocchuongtrinh,
                TruongThptMaTinhTp = request.tinhthanhpho_thpt,
                TruongThptTenTinhTp = tinhTP_THPT?.TenTinhtp,
                TruongThptMaQh = request.quanhuyen_thpt,
                TruongThptTenQh = quanHuyen_THPT?.TenQh,
                MaTruongThpt = request.tentruongthpt,
                TenTruongThpt = truongTHPT?.TenTruong,
                TenLop12 = request.tenlop12,
                KhuVuc = request.khuvucuutien,
                DoiTuongUuTien = request.doituonguutien,
                PhuongAn = request.phuongan,
                MaCcnn = request.chungchingoaingu,
                Ccnn = request.chungchingoaingu != null ? chungChiNgoaiNgu?.ChungChi + " - quy đổi: " + chungChiNgoaiNgu?.DiemQuiDoi + "đ" : "",
                MaNganhToHop1 = request.nganh1 + "#" + request.tohopmon1,
                TenNganhTenToHop1 = nganh1?.TenNganh + "#" + toHopMon1?.MaTohop + " - " + toHopMon1?.TenTohop,
                ChuongTrinhHoc1 = request.chuongtrinh1 != null ? request.chuongtrinh1 : "Tiêu chuẩn",
                LienLacDiaChi = request.diachinha,
                LienLacMaTp = request.tinhthanhpho_nha,
                LienLacTenTp = tinhTP_LienHe?.TenTinhTp,
                LienLacMaQh = request.quanhuyen_nha,
                LienLacTenQh = quanHuyen_LienHe?.TenQh,
                LienLacMaPhuongXa = request.phuongxa_nha,
                LienLacTenPhuongXa = phuongXa_LienHe?.TenPx,
                DienThoaiDd = request.dienthoaididong,
                DienThoaiPhuHuynh = request.dienthoaiphuhuynh,
                DateInserted = DateTime.Now,
                DaNhanHoSo = "C"
            };

            if (request.nganh2 != null)
            {
                admission.MaNganhToHop2 = request.nganh2 + "#" + request.tohopmon2;
                admission.TenNganhTenToHop2 = nganh2?.TenNganh + "#" + toHopMon2?.MaTohop + " - " + toHopMon2?.TenTohop;
                if (request.chuongtrinh2 != null)
                    admission.ChuongTrinhHoc2 = request.chuongtrinh2;
                else
                    admission.ChuongTrinhHoc2 = "Tiêu chuẩn";
            }
            if (request.nganh3 != null)
            {
                admission.MaNganhToHop3 = request.nganh3 + "#" + request.tohopmon3;
                admission.TenNganhTenToHop3 = nganh3?.TenNganh + "#" + toHopMon3?.MaTohop + " - " + toHopMon3?.TenTohop;
                if (request.chuongtrinh3 != null)
                    admission.ChuongTrinhHoc3 = request.chuongtrinh3;
                else
                    admission.ChuongTrinhHoc3 = "Tiêu chuẩn";
            }

            _context.Add(admission);
            await _context.SaveChangesAsync();

            if (request.phuongan == "1" && diemtb_cnlop11 != null && diemtb_hk1lop12 != null)
            {
                var bangDiemCnLop11 = new BangDiemThpt()
                {
                    MaHocKyLop = "CN_LOP11",
                    MaHoSoThpt = admission.Id,
                    Toan = Double.Parse(diemtb_cnlop11.diemtoan),
                    Van = Double.Parse(diemtb_cnlop11.diemvan),
                    Anh = Double.Parse(diemtb_cnlop11.diemanh),
                    Phap = Double.Parse(diemtb_cnlop11.diemphap),
                    Ly = Double.Parse(diemtb_cnlop11.diemly),
                    Hoa = Double.Parse(diemtb_cnlop11.diemhoa),
                    Sinh = Double.Parse(diemtb_cnlop11.diemsinh),
                    Su = Double.Parse(diemtb_cnlop11.diemsu),
                    Dia = Double.Parse(diemtb_cnlop11.diemdia),
                    Gdcd = Double.Parse(diemtb_cnlop11.diemgdcd)
                };
                _context.Add(bangDiemCnLop11);
                await _context.SaveChangesAsync();

                var bangDiemHk1Lop12 = new BangDiemThpt()
                {
                    MaHocKyLop = "HK1_LOP12",
                    MaHoSoThpt = admission.Id,
                    Toan = Double.Parse(diemtb_hk1lop12.diemtoan),
                    Van = Double.Parse(diemtb_hk1lop12.diemvan),
                    Anh = Double.Parse(diemtb_hk1lop12.diemanh),
                    Phap = Double.Parse(diemtb_hk1lop12.diemphap),
                    Ly = Double.Parse(diemtb_hk1lop12.diemly),
                    Hoa = Double.Parse(diemtb_hk1lop12.diemhoa),
                    Sinh = Double.Parse(diemtb_hk1lop12.diemsinh),
                    Su = Double.Parse(diemtb_hk1lop12.diemsu),
                    Dia = Double.Parse(diemtb_hk1lop12.diemdia),
                    Gdcd = Double.Parse(diemtb_hk1lop12.diemgdcd)
                };
                _context.Add(bangDiemHk1Lop12);
                await _context.SaveChangesAsync();
            }

            if (request.phuongan == "2" && diemtb_cnlop12 != null)
            {
                var bangDiemCnLop12 = new BangDiemThpt()
                {
                    MaHocKyLop = "CN_LOP12",
                    MaHoSoThpt = admission.Id,
                    Toan = Double.Parse(diemtb_cnlop12.diemtoan),
                    Van = Double.Parse(diemtb_cnlop12.diemvan),
                    Anh = Double.Parse(diemtb_cnlop12.diemanh),
                    Phap = Double.Parse(diemtb_cnlop12.diemphap),
                    Ly = Double.Parse(diemtb_cnlop12.diemly),
                    Hoa = Double.Parse(diemtb_cnlop12.diemhoa),
                    Sinh = Double.Parse(diemtb_cnlop12.diemsinh),
                    Su = Double.Parse(diemtb_cnlop12.diemsu),
                    Dia = Double.Parse(diemtb_cnlop12.diemdia),
                    Gdcd = Double.Parse(diemtb_cnlop12.diemgdcd)
                };
                _context.Add(bangDiemCnLop12);
                await _context.SaveChangesAsync();
            }

            admission.SoBaoDanh = "DVL_" + admission.Id;
            await _context.SaveChangesAsync();

            return admission.SoBaoDanh;
        }

        public DotXetTuyenVM GetPhase()
        {
            var phase = _context.Dot.FirstOrDefault(x => x.TrangThaiId == 1);
            return new DotXetTuyenVM
            {
                id = phase.ID,
                NamXetTuyen = phase?.NgayKetThuc.Year.ToString(),
                ThuTuDot = phase.DotThu,
                Khoa = phase.Khoa
            };
        }

        public bool ValidatePhaseIsExpired()
        {
            var phase = _context.Dot.FirstOrDefault(x => x.TrangThaiId == 1);
            if(phase != null && phase.NgayKetThuc < DateTime.Now)
            {
                phase.IsExpired = true;
                _context.Dot.Update(phase);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public ThongBao GetNotificationForPhaseIsExpired()
        {
            var notification = _context.ThongBao.FirstOrDefault(x => x.LoaiThongBaoId == 3 && x.TrangThaiId == 1);
            if (notification != null)
                return notification;
            return new ThongBao();
        }

        public LichTrinhVM GetScheduleForEditProfile()
        {
            var schedule = _context.LichTrinh.FirstOrDefault(x => x.TrangThaiId == 1 && x.HinhThucId == 1);
            return new LichTrinhVM
            {
                id = schedule.ID,
                NamXetTuyen = schedule?.NgayKetThuc.Year.ToString(),
                MaDot = schedule.MaDot,
                NgayBatDau = schedule.NgayBatDau.ToString("dd/MM/yyyy"),
                NgayKetThuc = schedule.NgayKetThuc.ToString("dd/MM/yyyy"),
            };
        }
    }
}
