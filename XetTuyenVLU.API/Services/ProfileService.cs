using XetTuyenVLU.Interfaces;
using XetTuyenVLU.ViewModels.Profile;
using XetTuyenVLU.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using XetTuyenVLU.ViewModels.Admission;

namespace XetTuyenVLU.Services
{
    public class ProfileService : IProfileService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileService(XetTuyenVLUContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public List<BangDiemThpt> GetBangDiem(int maHoSo)
        {
            var bangdiem = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == maHoSo);
            if (bangdiem == null)
            {
                throw new Exception($"Cannot find bangdiem with maHoSo {maHoSo}");
            }
            var bangdiemList = new List<BangDiemThpt>();
            foreach (var bdiem in bangdiem)
            {
                bangdiemList.Add(new BangDiemThpt
                {
                    MaHocKyLop = bdiem.MaHocKyLop,
                    Toan = bdiem.Toan,
                    Van = bdiem.Van,
                    Anh = bdiem.Anh,
                    Phap = bdiem.Phap,
                    Ly = bdiem.Ly,
                    Hoa = bdiem.Hoa,
                    Sinh = bdiem.Sinh,
                    Su = bdiem.Su,
                    Dia = bdiem.Dia,
                    Gdcd = bdiem.Gdcd,
                }); ;
            }

            return bangdiemList;
        }


        public HoSoThpt GetProfileByCMND(string cmnd, int dot)
        {
            var hoso = _context.HoSoThpts
                    .Join(_context.Dot, x => x.DotId,
                          y => y.ID, (x, y) => new
                          {
                              DotThu = y.DotThu,
                              Id = x.Id,
                              HoVaTen = x.HoVaTen,
                              Email = x.Email,
                              GioiTinh = x.GioiTinh,
                              NgaySinh = x.NgaySinh,
                              TenNoiSinh = x.TenNoiSinh,
                              TenDanToc = x.TenDanToc,
                              TenTonGiao = x.TenTonGiao,
                              Cmnd = x.Cmnd,
                              QuocTich = x.QuocTich,
                              HoKhau = x.HoKhau,
                              HoKhauTenPhuong = x.HoKhauTenPhuong,
                              HoKhauTenTinhTp = x.HoKhauTenTinhTp,
                              HoKhauTenQh = x.HoKhauTenQh,
                              NamTotNghiep = x.NamTotNghiep,
                              SoBaoDanh = x.SoBaoDanh,
                              HocLucLop12 = x.HocLucLop12,
                              HanhKiemLop12 = x.HanhKiemLop12,
                              LoaiHinhTn = x.LoaiHinhTn,
                              MaTruongThpt = x.MaTruongThpt,
                              TenTruongThpt = x.TenTruongThpt,
                              TruongThptTenTinhTp = x.TruongThptTenTinhTp,
                              TruongThptTenQh = x.TruongThptTenQh,
                              TenLop12 = x.TenLop12,
                              KhuVuc = x.KhuVuc,
                              DoiTuongUuTien = x.DoiTuongUuTien,
                              MaCcnn = x.MaCcnn,
                              Ccnn = x.Ccnn,
                              TenNganhTenToHop1 = x.TenNganhTenToHop1,
                              TenNganhTenToHop2 = x.TenNganhTenToHop2,
                              TenNganhTenToHop3 = x.TenNganhTenToHop3,
                              MaNganhToHop1 = x.MaNganhToHop1,
                              MaNganhToHop2 = x.MaNganhToHop2,
                              MaNganhToHop3 = x.MaNganhToHop3,
                              LienLacDiaChi = x.LienLacDiaChi,
                              LienLacMaTp = x.LienLacMaTp,
                              LienLacMaPhuongXa = x.LienLacMaPhuongXa,
                              LienLacMaQh = x.LienLacMaQh,
                              DienThoaiDd = x.DienThoaiDd,
                              DienThoaiPhuHuynh = x.DienThoaiPhuHuynh,
                              DaNhanHoSo = x.DaNhanHoSo,
                              ChuongTrinhHoc1 = x.ChuongTrinhHoc1,
                              ChuongTrinhHoc2 = x.ChuongTrinhHoc2,
                              ChuongTrinhHoc3 = x.ChuongTrinhHoc3,
                              MaDanToc = x.MaDanToc,
                              MaTonGiao = x.MaTonGiao,
                              MaNoiSinh = x.MaNoiSinh,
                              HoKhauMaPhuong = x.HoKhauMaPhuong,
                              HoKhauMaTinhTp = x.HoKhauMaTinhTp,
                              HoKhauMaQh = x.HoKhauMaQh,
                              TruongThptMaTinhTp = x.TruongThptMaTinhTp,
                              TruongThptMaQh = x.TruongThptMaQh,

                          }).Where(z => z.DotThu == dot.ToString() && z.Cmnd == cmnd)
                            .Select(z => new HoSoThpt()
                            {
                                Id = z.Id,
                                HoVaTen = z.HoVaTen,
                                Email = z.Email,
                                GioiTinh = z.GioiTinh,
                                NgaySinh = z.NgaySinh,
                                TenNoiSinh = z.TenNoiSinh,
                                TenDanToc = z.TenDanToc,
                                TenTonGiao = z.TenTonGiao,
                                Cmnd = z.Cmnd,
                                QuocTich = z.QuocTich,
                                HoKhau = z.HoKhau,
                                HoKhauTenPhuong = z.HoKhauTenPhuong,
                                HoKhauTenTinhTp = z.HoKhauTenTinhTp,
                                HoKhauTenQh = z.HoKhauTenQh,
                                NamTotNghiep = z.NamTotNghiep,
                                SoBaoDanh = z.SoBaoDanh,
                                HocLucLop12 = z.HocLucLop12,
                                HanhKiemLop12 = z.HanhKiemLop12,
                                LoaiHinhTn = z.LoaiHinhTn,
                                MaTruongThpt = z.MaTruongThpt,
                                TenTruongThpt = z.TenTruongThpt,
                                TruongThptTenTinhTp = z.TruongThptTenTinhTp,
                                TruongThptTenQh = z.TruongThptTenQh,
                                TenLop12 = z.TenLop12,
                                KhuVuc = z.KhuVuc,
                                DoiTuongUuTien = z.DoiTuongUuTien,
                                MaCcnn = z.MaCcnn,
                                Ccnn = z.Ccnn,
                                TenNganhTenToHop1 = z.TenNganhTenToHop1,
                                TenNganhTenToHop2 = z.TenNganhTenToHop2,
                                TenNganhTenToHop3 = z.TenNganhTenToHop3,
                                MaNganhToHop1 = z.MaNganhToHop1,
                                MaNganhToHop2 = z.MaNganhToHop2,
                                MaNganhToHop3 = z.MaNganhToHop3,
                                LienLacDiaChi = z.LienLacDiaChi,
                                LienLacMaTp = z.LienLacMaTp,
                                LienLacMaPhuongXa = z.LienLacMaPhuongXa,
                                LienLacMaQh = z.LienLacMaQh,
                                DienThoaiDd = z.DienThoaiDd,
                                DienThoaiPhuHuynh = z.DienThoaiPhuHuynh,
                                DaNhanHoSo = z.DaNhanHoSo,
                                ChuongTrinhHoc1 = z.ChuongTrinhHoc1,
                                ChuongTrinhHoc2 = z.ChuongTrinhHoc2,
                                ChuongTrinhHoc3 = z.ChuongTrinhHoc3,
                                MaDanToc = z.MaDanToc,
                                MaTonGiao = z.MaTonGiao,
                                MaNoiSinh = z.MaNoiSinh,
                                HoKhauMaPhuong = z.HoKhauMaPhuong,
                                HoKhauMaTinhTp = z.HoKhauMaTinhTp,
                                HoKhauMaQh = z.HoKhauMaQh,
                                TruongThptMaTinhTp = z.TruongThptMaTinhTp,
                                TruongThptMaQh = z.TruongThptMaQh,
                            }).FirstOrDefault();
            return hoso;
        }

        public bool ValidateCMNDToEdit(string cmnd, string currentCmnd)
        {
            var list = _context.HoSoThpts.Where(x => x.Cmnd != currentCmnd).ToList();

            var isExist = list.Any(x => x.Cmnd == cmnd);
            return isExist;
        }


        public async Task<bool> EditProfile(EditProfileRequest request)
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

            if (noiSinh == null ||
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


            var profile = _context.HoSoThpts.FirstOrDefault(x => x.Id == Int32.Parse(request.id));
            if (profile == null)
                throw new Exception($"Không tìm thấy hồ sơ với ID {Int32.Parse(request.id)}!");


            profile.HoVaTen = request.hovaten;
            profile.Email = request.email;
            profile.GioiTinh = request.gioitinh == "1" ? true : false;
            profile.NgaySinh = DateTime.Parse(request.ngaysinh);
            profile.MaNoiSinh = request.noisinh;
            profile.TenNoiSinh = noiSinh?.TenTinhTp;
            profile.MaDanToc = request.dantoc;
            profile.TenDanToc = danToc?.TenDantoc;
            profile.MaTonGiao = request.tongiao;
            profile.TenTonGiao = tonGiao?.TenTongiao;
            profile.Cmnd = request.cmnd;
            profile.QuocTich = request.quoctich + "|" + quocTich?.TenQt;
            profile.HoKhau = request.hokhauthuongtru;
            profile.HoKhauMaTinhTp = request.tinhthanhpho;
            profile.HoKhauTenTinhTp = tinhTP?.TenTinhTp;
            profile.HoKhauMaQh = request.quanhuyen;
            profile.HoKhauTenQh = quanHuyen?.TenQh;
            profile.HoKhauMaPhuong = request.phuongxa;
            profile.HoKhauTenPhuong = phuongXa?.TenPx;
            profile.NamTotNghiep = request.namtotnghiep;
            profile.HocLucLop12 = request.hocluclop12;
            profile.HanhKiemLop12 = request.hanhkiemlop12;
            profile.LoaiHinhTn = request.hocchuongtrinh;
            profile.TruongThptMaTinhTp = request.tinhthanhpho_thpt;
            profile.TruongThptTenTinhTp = tinhTP_THPT?.TenTinhtp;
            profile.TruongThptMaQh = request.quanhuyen_thpt;
            profile.TruongThptTenQh = quanHuyen_THPT?.TenQh;
            profile.MaTruongThpt = request.tentruongthpt;
            profile.TenTruongThpt = truongTHPT?.TenTruong;
            profile.TenLop12 = request.tenlop12;
            profile.KhuVuc = request.khuvucuutien;
            profile.DoiTuongUuTien = request.doituonguutien;
            profile.MaCcnn = request.chungchingoaingu;
            profile.Ccnn = request.chungchingoaingu != null ? chungChiNgoaiNgu?.ChungChi + " - quy đổi: " + chungChiNgoaiNgu?.DiemQuiDoi + "đ" : "";
            profile.MaNganhToHop1 = request.nganh1 + "#" + request.tohopmon1;
            profile.TenNganhTenToHop1 = nganh1?.TenNganh + "#" + toHopMon1?.MaTohop + " - " + toHopMon1?.TenTohop;
            profile.ChuongTrinhHoc1 = request.chuongtrinh1 != null ? request.chuongtrinh1 : "Tiêu chuẩn";
            profile.LienLacDiaChi = request.diachinha;
            profile.LienLacMaTp = request.tinhthanhpho_nha;
            profile.LienLacTenTp = tinhTP_LienHe?.TenTinhTp;
            profile.LienLacMaQh = request.quanhuyen_nha;
            profile.LienLacTenQh = quanHuyen_LienHe?.TenQh;
            profile.LienLacMaPhuongXa = request.phuongxa_nha;
            profile.LienLacTenPhuongXa = phuongXa_LienHe?.TenPx;
            profile.DienThoaiDd = request.dienthoaididong;
            profile.DienThoaiPhuHuynh = request.dienthoaiphuhuynh;
            profile.DateInserted = DateTime.Now;
            profile.DaNhanHoSo = "C";


            if (request.nganh2 != null)
            {
                profile.MaNganhToHop2 = request.nganh2 + "#" + request.tohopmon2;
                profile.TenNganhTenToHop2 = nganh2?.TenNganh + "#" + toHopMon2?.MaTohop + " - " + toHopMon2?.TenTohop;
                if (request.chuongtrinh2 != null && request.chuongtrinh2 != "null")
                    profile.ChuongTrinhHoc2 = request.chuongtrinh2;
                else
                    profile.ChuongTrinhHoc2 = "Tiêu chuẩn";
            }
            else
            {
                profile.MaNganhToHop2 = null;
                profile.TenNganhTenToHop2 = null;
                profile.ChuongTrinhHoc2 = null;
            }
            if (request.nganh3 != null)
            {
                profile.MaNganhToHop3 = request.nganh3 + "#" + request.tohopmon3;
                profile.TenNganhTenToHop3 = nganh3?.TenNganh + "#" + toHopMon3?.MaTohop + " - " + toHopMon3?.TenTohop;
                if (request.chuongtrinh3 != null && request.chuongtrinh3 != "null")
                    profile.ChuongTrinhHoc3 = request.chuongtrinh3;
                else
                    profile.ChuongTrinhHoc3 = "Tiêu chuẩn";
            }
            else
            {
                profile.MaNganhToHop3 = null;
                profile.TenNganhTenToHop3 = null;
                profile.ChuongTrinhHoc3 = null;
            }

            _context.HoSoThpts.Update(profile);


            if (request.phuongan == "1" && diemtb_cnlop11 != null && diemtb_hk1lop12 != null)
            {
                var bangDiemCnLop11 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == Int32.Parse(request.id) && x.MaHocKyLop == "CN_LOP11");
                var bangDiemCnLop12 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == Int32.Parse(request.id) && x.MaHocKyLop == "CN_LOP12");
                if (bangDiemCnLop11 == null)
                {
                    var newbangDiemCnLop11 = new BangDiemThpt()
                    {
                        MaHocKyLop = "CN_LOP11",
                        MaHoSoThpt = long.Parse(request.id),
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
                    _context.BangDiemThpts.Add(newbangDiemCnLop11);
                    _context.BangDiemThpts.Remove(bangDiemCnLop12);
                }
                else
                {
                    bangDiemCnLop11.Toan = Double.Parse(diemtb_cnlop11.diemtoan);
                    bangDiemCnLop11.Van = Double.Parse(diemtb_cnlop11.diemvan);
                    bangDiemCnLop11.Anh = Double.Parse(diemtb_cnlop11.diemanh);
                    bangDiemCnLop11.Phap = Double.Parse(diemtb_cnlop11.diemphap);
                    bangDiemCnLop11.Ly = Double.Parse(diemtb_cnlop11.diemly);
                    bangDiemCnLop11.Hoa = Double.Parse(diemtb_cnlop11.diemhoa);
                    bangDiemCnLop11.Sinh = Double.Parse(diemtb_cnlop11.diemsinh);
                    bangDiemCnLop11.Su = Double.Parse(diemtb_cnlop11.diemsu);
                    bangDiemCnLop11.Dia = Double.Parse(diemtb_cnlop11.diemdia);
                    bangDiemCnLop11.Gdcd = Double.Parse(diemtb_cnlop11.diemgdcd);
                    _context.BangDiemThpts.Update(bangDiemCnLop11);
                }

                var bangDiemHk1Lop12 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == Int32.Parse(request.id) && x.MaHocKyLop == "HK1_LOP12");
                if (bangDiemHk1Lop12 == null)
                {
                    var newbangDiemHk1Lop12 = new BangDiemThpt()
                    {
                        MaHocKyLop = "HK1_LOP12",
                        MaHoSoThpt = long.Parse(request.id),
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
                    _context.BangDiemThpts.Add(newbangDiemHk1Lop12);
                }
                else
                {
                    bangDiemHk1Lop12.Toan = Double.Parse(diemtb_hk1lop12.diemtoan);
                    bangDiemHk1Lop12.Van = Double.Parse(diemtb_hk1lop12.diemvan);
                    bangDiemHk1Lop12.Anh = Double.Parse(diemtb_hk1lop12.diemanh);
                    bangDiemHk1Lop12.Phap = Double.Parse(diemtb_hk1lop12.diemphap);
                    bangDiemHk1Lop12.Ly = Double.Parse(diemtb_hk1lop12.diemly);
                    bangDiemHk1Lop12.Hoa = Double.Parse(diemtb_hk1lop12.diemhoa);
                    bangDiemHk1Lop12.Sinh = Double.Parse(diemtb_hk1lop12.diemsinh);
                    bangDiemHk1Lop12.Su = Double.Parse(diemtb_hk1lop12.diemsu);
                    bangDiemHk1Lop12.Dia = Double.Parse(diemtb_hk1lop12.diemdia);
                    bangDiemHk1Lop12.Gdcd = Double.Parse(diemtb_hk1lop12.diemgdcd);
                    _context.BangDiemThpts.Update(bangDiemHk1Lop12);
                }


            }

            if (request.phuongan == "2" && diemtb_cnlop12 != null)
            {
                var bangDiemCnLop12 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == Int32.Parse(request.id) && x.MaHocKyLop == "CN_LOP12");
                var bangDiemCnLop11 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == Int32.Parse(request.id) && x.MaHocKyLop == "CN_LOP11");
                var bangDiemHk1Lop12 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == Int32.Parse(request.id) && x.MaHocKyLop == "HK1_LOP12");
                if (bangDiemCnLop12 == null)
                {
                    var newbangDiemCnLop12 = new BangDiemThpt()
                    {
                        MaHocKyLop = "CN_LOP12",
                        MaHoSoThpt = long.Parse(request.id),
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
                    _context.BangDiemThpts.Add(newbangDiemCnLop12);
                    _context.BangDiemThpts.Remove(bangDiemCnLop11);
                    _context.BangDiemThpts.Remove(bangDiemHk1Lop12);
                }
                else
                {
                    bangDiemCnLop12.Toan = Double.Parse(diemtb_cnlop12.diemtoan);
                    bangDiemCnLop12.Van = Double.Parse(diemtb_cnlop12.diemvan);
                    bangDiemCnLop12.Anh = Double.Parse(diemtb_cnlop12.diemanh);
                    bangDiemCnLop12.Phap = Double.Parse(diemtb_cnlop12.diemphap);
                    bangDiemCnLop12.Ly = Double.Parse(diemtb_cnlop12.diemly);
                    bangDiemCnLop12.Hoa = Double.Parse(diemtb_cnlop12.diemhoa);
                    bangDiemCnLop12.Sinh = Double.Parse(diemtb_cnlop12.diemsinh);
                    bangDiemCnLop12.Su = Double.Parse(diemtb_cnlop12.diemsu);
                    bangDiemCnLop12.Dia = Double.Parse(diemtb_cnlop12.diemdia);
                    bangDiemCnLop12.Gdcd = Double.Parse(diemtb_cnlop12.diemgdcd);
                    _context.BangDiemThpts.Update(bangDiemCnLop12);
                }




            }


            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddImgPathHocBa(AddImgPath hocBa)
        {
            try
            {
                var data = new HocBa();
                string duongdan = null;
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "MaHoSoThpt_" + hocBa.MaHoSoThpt.ToString());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (var file in hocBa.imgFile)
                {
                    string imgName = new String(Path.GetFileNameWithoutExtension(file.FileName));
                    imgName = imgName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
                    var imgPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "MaHoSoThpt_" + hocBa.MaHoSoThpt.ToString(), imgName);
                    duongdan = Path.Combine("Images", "MaHoSoThpt_" + hocBa.MaHoSoThpt.ToString(), imgName);
                    data = new HocBa()
                    {
                        MaHoSoThpt = hocBa.MaHoSoThpt,
                        DuongDanHocBa = duongdan,
                    };
                    _context.HocBa.Add(data);
                    using (var fileStream = new FileStream(imgPath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                return _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
