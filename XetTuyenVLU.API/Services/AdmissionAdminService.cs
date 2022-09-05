using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using XetTuyenVLU.Models;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.ViewModels.AdmissionAdmin;
using XetTuyenVLU.ViewModels.Admission;
using XetTuyenVLU.SettingsForMail;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using XetTuyenVLU.Helpers;
using System.Net.Mail;
using System.IO.Compression;

namespace XetTuyenVLU.Services
{
    public class AdmissionAdminService : IAdmisionAdminService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly MailSettings _mailSettings;
        private readonly AlternateViewHelper _alternateViewHelper;
        private readonly IWebHostEnvironment _webHostEvironment;
        public AdmissionAdminService(XetTuyenVLUContext context, IOptions<MailSettings> mailSettings, IWebHostEnvironment webHostEvironment)
        {
            _context = context;
            _mailSettings = mailSettings.Value;
            _alternateViewHelper = new AlternateViewHelper();
            _webHostEvironment = webHostEvironment;
        }

        public List<Hoso> GetDatainHoso()
        {

            var data = _context.HoSoThpts
                .Select(n => new Hoso
                {
                    Id = n.Id,
                    HoVaTen = n.HoVaTen,
                    Email = n.Email,
                    Cmnd = n.Cmnd,
                    TenNoiSinh = n.TenNoiSinh,
                    GioiTinh = n.GioiTinh == true ? "Nam" : "Nữ",
                    DienThoaiDd = n.DienThoaiDd,
                    NgaySinh = n.NgaySinh,
                    TenDanToc = n.TenDanToc,
                    QuocTich = n.QuocTich,
                    TenTonGiao = n.TenTonGiao,
                    HoKhau = n.HoKhau + ", " + n.HoKhauTenPhuong + ", " + n.HoKhauTenQh + ", " + n.HoKhauTenTinhTp,
                    NamTotNghiep = n.NamTotNghiep,
                    SoBaoDanh = n.SoBaoDanh,
                    HocLucLop12 = n.HocLucLop12,
                    HanhKiemLop12 = n.HanhKiemLop12,
                    LoaiHinhTn = n.LoaiHinhTn,
                    TenTruongThpt = n.TenTruongThpt,
                    TenLop12 = n.TenLop12,
                    KhuVuc = n.KhuVuc,
                    DoiTuongUuTien = n.DoiTuongUuTien,
                    Chungchingoaingu = n.Ccnn,
                    TenNganhTenToHop1 = n.TenNganhTenToHop1,
                    ChuongTrinhHoc1 = n.ChuongTrinhHoc1,
                    TenNganhTenToHop2 = n.TenNganhTenToHop2,
                    ChuongTrinhHoc2 = n.ChuongTrinhHoc2,
                    TenNganhTenToHop3 = n.TenNganhTenToHop3,
                    ChuongTrinhHoc3 = n.ChuongTrinhHoc3,
                    DaNhanHoSo = n.DaNhanHoSo,
                    DaGuiMail = n.DaGuiMail,
                    DiemVeMyThuat = n.DiemVeMyThuat,
                    DiemVeNangKhieu = n.DiemVeNangKhieu,
                    Phase = n.Dot.DotThu,
                    DotId = n.DotId,
                    Khoa = n.Dot.Khoa,
                })
                .ToList();

            return data;
        }

        public async Task<bool> DeleteHoSoThpts(long Id)
        {
            var data = _context.HoSoThpts.Where(x => x.Id == Id).ToList();
            if (data == null)
            {
                throw new Exception($"Không tìm thấy Hồ sơ có ID là {Id}");
            }
            _context.RemoveRange(data);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteBangDiemThpts(long MaHoSoThpt)
        {
            var data = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == MaHoSoThpt);
            if (data == null)
            {
                throw new Exception($"Không tìm thấy Mã hồ sơ là {MaHoSoThpt}");
            }
            _context.RemoveRange(data);
            return _context.SaveChanges() > 0;
        }

        public AdmissionVM GetAdmissionById(int id)
        {
            var admission = _context.HoSoThpts.FirstOrDefault(x => x.Id == id);
            if (admission == null)
                throw new Exception($"Không tìm thấy hồ sơ có ID {id}!");
            var bangDiemList = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == admission.Id).ToList();
            if (bangDiemList == null)
                throw new Exception($"Không tìm thấy bảng điểm của hồ sơ có ID {id}!");
            var phase = _context.Dot.FirstOrDefault(x => x.ID == admission.DotId);
            if (phase == null)
                throw new Exception($"Không tìm thấy đợt xét tuyển của hồ sơ có ID {id}!");
            var admissionVM = new AdmissionVM
            {
                Id = admission.Id,
                DotId = admission.DotId,
                DotThu = phase.DotThu,
                Khoa = phase.Khoa,
                NgayBatDau = phase.NgayBatDau,
                NgayKetThuc = phase.NgayKetThuc,
                HoVaTen = admission.HoVaTen,
                Email = admission.Email,
                GioiTinh = admission.GioiTinh == true ? "1" : "0",
                NgaySinh = admission.NgaySinh?.ToString("yyyy-MM-dd"),
                MaNoiSinh = admission.MaNoiSinh,
                MaDanToc = admission.MaDanToc,
                MaTonGiao = admission.MaTonGiao,
                Cmnd = admission.Cmnd,
                MaQuocTich = admission.QuocTich?.Split("|")[0],
                DiaChiHoKhau = admission.HoKhau,
                HoKhauMaPhuong = admission.HoKhauMaPhuong,
                HoKhauMaTinhTp = admission.HoKhauMaTinhTp,
                HoKhauMaQh = admission.HoKhauMaQh,
                NamTotNghiep = admission.NamTotNghiep,
                SoBaoDanh = admission.SoBaoDanh,
                HocLucLop12 = admission.HocLucLop12,
                HanhKiemLop12 = admission.HanhKiemLop12,
                LoaiHinhTn = admission.LoaiHinhTn,
                TruongThptMaTinhTp = admission.TruongThptMaTinhTp,
                TruongThptMaQh = admission.TruongThptMaQh,
                MaTruongThpt = admission.MaTruongThpt,
                TenLop12 = admission.TenLop12,
                KhuVuc = admission.KhuVuc,
                DoiTuongUuTien = admission.DoiTuongUuTien,
                MaCcnn = admission.MaCcnn,
                MaNganh1 = admission.MaNganhToHop1?.Split("#")[0],
                MaToHop1 = admission.MaNganhToHop1?.Split("#")[1],
                ChuongTrinhHoc1 = admission.ChuongTrinhHoc1,
                MaNganh2 = admission.MaNganhToHop2?.Split("#")[0],
                MaToHop2 = admission.MaNganhToHop2?.Split("#")[1],
                ChuongTrinhHoc2 = admission.ChuongTrinhHoc2,
                MaNganh3 = admission.MaNganhToHop3?.Split("#")[0],
                MaToHop3 = admission.MaNganhToHop3?.Split("#")[1],
                ChuongTrinhHoc3 = admission.ChuongTrinhHoc3,
                DiaChiLienLac = admission.LienLacDiaChi,
                LienLacMaPhuongXa = admission.LienLacMaPhuongXa,
                LienLacMaTp = admission.LienLacMaTp,
                LienLacMaQh = admission.LienLacMaQh,
                DienThoaiDd = admission.DienThoaiDd,
                DienThoaiPhuHuynh = admission.DienThoaiPhuHuynh,
                DaNhanHoSo = admission.DaNhanHoSo,
                DaGuiMail = admission.DaGuiMail,
                phuongAn = admission.PhuongAn,
                diemCnlop11 = new BangDiemVM
                {
                    diemtoan = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Toan.ToString(),
                    diemvan = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Van.ToString(),
                    diemanh = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Anh.ToString(),
                    diemphap = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Phap.ToString(),
                    diemly = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Ly.ToString(),
                    diemhoa = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Hoa.ToString(),
                    diemsinh = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Sinh.ToString(),
                    diemsu = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Su.ToString(),
                    diemdia = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Dia.ToString(),
                    diemgdcd = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11")?.Gdcd.ToString()
                },
                diemHk1lop12 = new BangDiemVM
                {
                    diemtoan = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Toan.ToString(),
                    diemvan = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Van.ToString(),
                    diemanh = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Anh.ToString(),
                    diemphap = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Phap.ToString(),
                    diemly = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Ly.ToString(),
                    diemhoa = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Hoa.ToString(),
                    diemsinh = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Sinh.ToString(),
                    diemsu = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Su.ToString(),
                    diemdia = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Dia.ToString(),
                    diemgdcd = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12")?.Gdcd.ToString()
                },
                diemCnlop12 = new BangDiemVM
                {
                    diemtoan = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Toan.ToString(),
                    diemvan = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Van.ToString(),
                    diemanh = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Anh.ToString(),
                    diemphap = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Phap.ToString(),
                    diemly = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Ly.ToString(),
                    diemhoa = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Hoa.ToString(),
                    diemsinh = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Sinh.ToString(),
                    diemsu = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Su.ToString(),
                    diemdia = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Dia.ToString(),
                    diemgdcd = bangDiemList.FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12")?.Gdcd.ToString()
                }
            };
            return admissionVM;
        }

        public async Task<bool> EditAdmission(AdmissionEditingRequest request)
        {
            var diemCnlop11 = request.DiemCnlop11 != null ? JsonConvert.DeserializeObject<BangDiemVM>(request.DiemCnlop11) : null;
            var diemHk1lop12 = request.DiemHk1lop12 != null ? JsonConvert.DeserializeObject<BangDiemVM>(request.DiemHk1lop12) : null;
            var diemCnlop12 = request.DiemCnlop12 != null ? JsonConvert.DeserializeObject<BangDiemVM>(request.DiemCnlop12) : null;
            var noiSinh = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaTinhTp == request.MaNoiSinh);
            var danToc = await _context.DanTocs.FirstOrDefaultAsync(x => x.MaDantoc == request.MaDanToc);
            var tonGiao = await _context.TonGiaos.FirstOrDefaultAsync(x => x.MaTongiao == request.MaTonGiao);
            var quocTich = await _context.QuocTiches.FirstOrDefaultAsync(x => x.MaQt.ToString() == request.MaQuocTich);
            var chungChiNgoaiNgu = await _context.ChungChiNns.FirstOrDefaultAsync(x => x.Id.ToString() == request.MaCcnn);
            //Ho khau
            var tinhTP = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaTinhTp == request.HoKhauMaTinhTp);
            var quanHuyen = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaQh == request.HoKhauMaQh);
            var phuongXa = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaPx == request.HoKhauMaPhuong);
            //Truong THPT
            var tinhTP_THPT = await _context.TruongThpts.FirstOrDefaultAsync(x => x.MaTinhtp == request.TruongThptMaTinhTp);
            var quanHuyen_THPT = await _context.TruongThpts.FirstOrDefaultAsync(x => x.MaQh == request.TruongThptMaQh);
            var truongTHPT = await _context.TruongThpts.FirstOrDefaultAsync(x =>
                                                                            x.MaTinhtp == request.TruongThptMaTinhTp &&
                                                                            x.MaQh == request.TruongThptMaQh &&
                                                                            x.MaTruong == request.MaTruongThpt);
            //Lien he
            var tinhTP_LienHe = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaTinhTp == request.LienLacMaTp);
            var quanHuyen_LienHe = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaQh == request.LienLacMaQh);
            var phuongXa_LienHe = await _context.TpQhPxes.FirstOrDefaultAsync(x => x.MaPx == request.LienLacMaPhuongXa);
            //Nguyen vong 1
            var nganh1 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaNganh == request.MaNganh1);
            var toHopMon1 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaTohop == request.MaToHop1);
            //Nguyen vong 2
            var nganh2 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaNganh == request.MaNganh2);
            var toHopMon2 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaTohop == request.MaToHop2);
            //Nguyen vong 3
            var nganh3 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaNganh == request.MaNganh3);
            var toHopMon3 = await _context.Nganhs.FirstOrDefaultAsync(x => x.MaTohop == request.MaToHop3);

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

            var admission = await _context.HoSoThpts.FirstOrDefaultAsync(x => x.Id.ToString() == request.Id);
            if (admission == null)
                throw new Exception($"Không tìm thấy hồ sơ xét tuyển có ID {request.Id}!");
            admission.HoVaTen = request.HoVaTen;
            admission.Email = request.Email;
            admission.GioiTinh = request.GioiTinh == "1" ? true : false;
            admission.NgaySinh = DateTime.Parse(request.NgaySinh);
            admission.MaNoiSinh = request.MaNoiSinh;
            admission.TenNoiSinh = noiSinh.TenTinhTp;
            admission.MaDanToc = request.MaDanToc;
            admission.TenDanToc = danToc.TenDantoc;
            admission.MaTonGiao = request.MaTonGiao;
            admission.TenTonGiao = tonGiao.TenTongiao;
            admission.Cmnd = request.Cmnd;
            admission.QuocTich = request.MaQuocTich + "|" + quocTich?.TenQt;
            admission.HoKhau = request.DiaChiHoKhau;
            admission.HoKhauMaTinhTp = request.HoKhauMaTinhTp;
            admission.HoKhauTenTinhTp = tinhTP.TenTinhTp;
            admission.HoKhauMaQh = request.HoKhauMaQh;
            admission.HoKhauTenQh = quanHuyen.TenQh;
            admission.HoKhauMaPhuong = request.HoKhauMaPhuong;
            admission.HoKhauTenPhuong = phuongXa.TenPx;
            admission.NamTotNghiep = request.NamTotNghiep;
            admission.HocLucLop12 = request.HocLucLop12;
            admission.HanhKiemLop12 = request.HanhKiemLop12;
            admission.LoaiHinhTn = request.LoaiHinhTn;
            admission.TruongThptMaTinhTp = request.TruongThptMaTinhTp;
            admission.TruongThptTenTinhTp = tinhTP_THPT.TenTinhtp;
            admission.TruongThptMaQh = request.TruongThptMaQh;
            admission.TruongThptTenQh = quanHuyen_THPT.TenQh;
            admission.MaTruongThpt = request.MaTruongThpt;
            admission.TenTruongThpt = truongTHPT.TenTruong;
            admission.TenLop12 = request.TenLop12;
            admission.KhuVuc = request.KhuVuc;
            admission.DoiTuongUuTien = request.DoiTuongUuTien;
            admission.PhuongAn = request.PhuongAn;
            admission.MaCcnn = request.MaCcnn;
            admission.Ccnn = request.MaCcnn != null ? chungChiNgoaiNgu?.ChungChi + " - quy đổi: " + chungChiNgoaiNgu?.DiemQuiDoi + "đ" : "";
            admission.MaNganhToHop1 = request.MaNganh1 + "#" + request.MaToHop1;
            admission.TenNganhTenToHop1 = nganh1?.TenNganh + "#" + toHopMon1?.MaTohop + " - " + toHopMon1?.TenTohop;
            admission.ChuongTrinhHoc1 = request.ChuongTrinhHoc1 != null ? request.ChuongTrinhHoc1 : "Tiêu chuẩn";
            admission.LienLacDiaChi = request.DiaChiLienLac;
            admission.LienLacMaTp = request.LienLacMaTp;
            admission.LienLacTenTp = tinhTP_LienHe?.TenTinhTp;
            admission.LienLacMaQh = request.LienLacMaQh;
            admission.LienLacTenQh = quanHuyen_LienHe?.TenQh;
            admission.LienLacMaPhuongXa = request.LienLacMaPhuongXa;
            admission.LienLacTenPhuongXa = phuongXa_LienHe?.TenPx;
            admission.DienThoaiDd = request.DienThoaiDd;
            admission.DienThoaiPhuHuynh = request.DienThoaiPhuHuynh;
            admission.DateEdited = DateTime.Now;

            if (request.MaNganh2 != null)
            {
                admission.MaNganhToHop2 = request.MaNganh2 + "#" + request.MaToHop2;
                admission.TenNganhTenToHop2 = nganh2?.TenNganh + "#" + toHopMon2?.MaTohop + " - " + toHopMon2?.TenTohop;
                if (request.ChuongTrinhHoc2 != null)
                    admission.ChuongTrinhHoc2 = request.ChuongTrinhHoc2;
                else
                    admission.ChuongTrinhHoc2 = "Tiêu chuẩn";
            }
            if (request.MaNganh3 != null)
            {
                admission.MaNganhToHop3 = request.MaNganh3 + "#" + request.MaToHop3;
                admission.TenNganhTenToHop3 = nganh3?.TenNganh + "#" + toHopMon3?.MaTohop + " - " + toHopMon3?.TenTohop;
                if (request.ChuongTrinhHoc3 != null)
                    admission.ChuongTrinhHoc3 = request.ChuongTrinhHoc3;
                else
                    admission.ChuongTrinhHoc3 = "Tiêu chuẩn";
            }
            _context.HoSoThpts.Update(admission);

            if (request.PhuongAn == "1" && diemCnlop11 != null && diemHk1lop12 != null)
            {
                var bangDiemCnLop11 = await _context.BangDiemThpts.FirstOrDefaultAsync(
                    x => x.MaHoSoThpt == admission.Id && x.MaHocKyLop == "CN_LOP11");
                if (bangDiemCnLop11 != null)
                {
                    bangDiemCnLop11.Toan = Double.Parse(diemCnlop11.diemtoan);
                    bangDiemCnLop11.Van = Double.Parse(diemCnlop11.diemvan);
                    bangDiemCnLop11.Anh = Double.Parse(diemCnlop11.diemanh);
                    bangDiemCnLop11.Phap = Double.Parse(diemCnlop11.diemphap);
                    bangDiemCnLop11.Ly = Double.Parse(diemCnlop11.diemly);
                    bangDiemCnLop11.Hoa = Double.Parse(diemCnlop11.diemhoa);
                    bangDiemCnLop11.Sinh = Double.Parse(diemCnlop11.diemsinh);
                    bangDiemCnLop11.Su = Double.Parse(diemCnlop11.diemsu);
                    bangDiemCnLop11.Dia = Double.Parse(diemCnlop11.diemdia);
                    bangDiemCnLop11.Gdcd = Double.Parse(diemCnlop11.diemgdcd);
                    _context.BangDiemThpts.Update(bangDiemCnLop11);
                }
                else
                {
                    var bangDiemNew = new BangDiemThpt()
                    {
                        MaHocKyLop = "CN_LOP11",
                        MaHoSoThpt = admission.Id,
                        Toan = Double.Parse(diemCnlop11.diemtoan),
                        Van = Double.Parse(diemCnlop11.diemvan),
                        Anh = Double.Parse(diemCnlop11.diemanh),
                        Phap = Double.Parse(diemCnlop11.diemphap),
                        Ly = Double.Parse(diemCnlop11.diemly),
                        Hoa = Double.Parse(diemCnlop11.diemhoa),
                        Sinh = Double.Parse(diemCnlop11.diemsinh),
                        Su = Double.Parse(diemCnlop11.diemsu),
                        Dia = Double.Parse(diemCnlop11.diemdia),
                        Gdcd = Double.Parse(diemCnlop11.diemgdcd)
                    };
                    _context.BangDiemThpts.Add(bangDiemNew);
                }

                var bangDiemHk1Lop12 = await _context.BangDiemThpts.FirstOrDefaultAsync(
                    x => x.MaHoSoThpt == admission.Id && x.MaHocKyLop == "HK1_LOP12");
                if (bangDiemHk1Lop12 != null)
                {
                    bangDiemHk1Lop12.Toan = Double.Parse(diemHk1lop12.diemtoan);
                    bangDiemHk1Lop12.Van = Double.Parse(diemHk1lop12.diemvan);
                    bangDiemHk1Lop12.Anh = Double.Parse(diemHk1lop12.diemanh);
                    bangDiemHk1Lop12.Phap = Double.Parse(diemHk1lop12.diemphap);
                    bangDiemHk1Lop12.Ly = Double.Parse(diemHk1lop12.diemly);
                    bangDiemHk1Lop12.Hoa = Double.Parse(diemHk1lop12.diemhoa);
                    bangDiemHk1Lop12.Sinh = Double.Parse(diemHk1lop12.diemsinh);
                    bangDiemHk1Lop12.Su = Double.Parse(diemHk1lop12.diemsu);
                    bangDiemHk1Lop12.Dia = Double.Parse(diemHk1lop12.diemdia);
                    bangDiemHk1Lop12.Gdcd = Double.Parse(diemHk1lop12.diemgdcd);
                    _context.BangDiemThpts.Update(bangDiemHk1Lop12);
                }
                else
                {
                    var bangDiemNew = new BangDiemThpt()
                    {
                        MaHocKyLop = "HK1_LOP12",
                        MaHoSoThpt = admission.Id,
                        Toan = Double.Parse(diemHk1lop12.diemtoan),
                        Van = Double.Parse(diemHk1lop12.diemvan),
                        Anh = Double.Parse(diemHk1lop12.diemanh),
                        Phap = Double.Parse(diemHk1lop12.diemphap),
                        Ly = Double.Parse(diemHk1lop12.diemly),
                        Hoa = Double.Parse(diemHk1lop12.diemhoa),
                        Sinh = Double.Parse(diemHk1lop12.diemsinh),
                        Su = Double.Parse(diemHk1lop12.diemsu),
                        Dia = Double.Parse(diemHk1lop12.diemdia),
                        Gdcd = Double.Parse(diemHk1lop12.diemgdcd)
                    };
                    _context.BangDiemThpts.Add(bangDiemNew);
                }
                var removeBangDiemCnLop12 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == admission.Id && x.MaHocKyLop == "CN_LOP12");
                if (removeBangDiemCnLop12 != null)
                    _context.BangDiemThpts.Remove(removeBangDiemCnLop12);
            }

            if (request.PhuongAn == "2" && diemCnlop12 != null)
            {
                var bangDiemCnLop12 = await _context.BangDiemThpts.FirstOrDefaultAsync(
                    x => x.MaHoSoThpt == admission.Id && x.MaHocKyLop == "CN_LOP12");
                if (bangDiemCnLop12 != null)
                {
                    bangDiemCnLop12.Toan = Double.Parse(diemCnlop12.diemtoan);
                    bangDiemCnLop12.Van = Double.Parse(diemCnlop12.diemvan);
                    bangDiemCnLop12.Anh = Double.Parse(diemCnlop12.diemanh);
                    bangDiemCnLop12.Phap = Double.Parse(diemCnlop12.diemphap);
                    bangDiemCnLop12.Ly = Double.Parse(diemCnlop12.diemly);
                    bangDiemCnLop12.Hoa = Double.Parse(diemCnlop12.diemhoa);
                    bangDiemCnLop12.Sinh = Double.Parse(diemCnlop12.diemsinh);
                    bangDiemCnLop12.Su = Double.Parse(diemCnlop12.diemsu);
                    bangDiemCnLop12.Dia = Double.Parse(diemCnlop12.diemdia);
                    bangDiemCnLop12.Gdcd = Double.Parse(diemCnlop12.diemgdcd);
                    _context.BangDiemThpts.Update(bangDiemCnLop12);
                }
                else
                {
                    var bangDiemNew = new BangDiemThpt()
                    {
                        MaHocKyLop = "CN_LOP12",
                        MaHoSoThpt = admission.Id,
                        Toan = Double.Parse(diemCnlop12.diemtoan),
                        Van = Double.Parse(diemCnlop12.diemvan),
                        Anh = Double.Parse(diemCnlop12.diemanh),
                        Phap = Double.Parse(diemCnlop12.diemphap),
                        Ly = Double.Parse(diemCnlop12.diemly),
                        Hoa = Double.Parse(diemCnlop12.diemhoa),
                        Sinh = Double.Parse(diemCnlop12.diemsinh),
                        Su = Double.Parse(diemCnlop12.diemsu),
                        Dia = Double.Parse(diemCnlop12.diemdia),
                        Gdcd = Double.Parse(diemCnlop12.diemgdcd)
                    };
                    _context.BangDiemThpts.Add(bangDiemNew);
                }
                var removeBangDiemCnLop11 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == admission.Id && x.MaHocKyLop == "CN_LOP11");
                var removeBangDiemHk1Lop12 = _context.BangDiemThpts.FirstOrDefault(x => x.MaHoSoThpt == admission.Id && x.MaHocKyLop == "HK1_LOP12");
                if (removeBangDiemCnLop11 != null && removeBangDiemHk1Lop12 != null)
                {
                    _context.BangDiemThpts.Remove(removeBangDiemCnLop11);
                    _context.BangDiemThpts.Remove(removeBangDiemHk1Lop12);
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SendEmailForAdmission(AdmissionSendMailRequest request)
        {
            var thongBao = await _context.ThongBao.FirstOrDefaultAsync(x => x.TrangThaiId == 1 && x.LoaiThongBaoId == 2);
            if (thongBao == null)
                throw new Exception($"Không tìm thấy thông báo có ID {thongBao.ID}!");
            string MailText = thongBao.Content;
            MailText = MailText.Replace("[0]", request.HoVaTen);
            var mailMessage = new MailMessage();
            AlternateView alterView = _alternateViewHelper.ContentToAlternateView(MailText);
            mailMessage.AlternateViews.Add(alterView);
            var email = (MimeMessage)mailMessage;
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = "Thư báo kết quả xét tuyển từ Trường đại học Văn Lang";
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            var result = await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return result != "";
        }

        public AdmissionMailVM GetMailBeforeSend()
        {
            var thongBao = _context.ThongBao.FirstOrDefault(x => x.TrangThaiId == 1 && x.LoaiThongBaoId == 2);
            if (thongBao == null)
                throw new Exception($"Không tìm thấy thông báo có ID {thongBao.ID}!");
            return new AdmissionMailVM
            {
                Content = thongBao.Content,
                LoaiThongBaoId = thongBao.LoaiThongBaoId,
                TenThongBao = _context.LoaiThongBao.FirstOrDefault(x => x.ID == thongBao.LoaiThongBaoId)?.TenThongBao
            };
        }

        public bool ReceiveAdmissionProfileById(int id)
        {
            var admission = _context.HoSoThpts.FirstOrDefault(x => x.Id == id);
            if (admission == null)
                throw new Exception($"Không tìm thấy hồ sơ có ID {id}!");
            admission.DaNhanHoSo = "N";
            _context.HoSoThpts.Update(admission);
            return _context.SaveChanges() > 0;
        }

        public bool RejectAdmissionProfileById(int id)
        {
            var admission = _context.HoSoThpts.FirstOrDefault(x => x.Id == id);
            if (admission == null)
                throw new Exception($"Không tìm thấy hồ sơ có ID {id}!");
            admission.DaNhanHoSo = "C";
            _context.HoSoThpts.Update(admission);
            return _context.SaveChanges() > 0;
        }

        public List<Phase> GetPhase()
        {
            var result = _context.Dot
                .Select(n => new Phase
                {
                    Id = n.ID,
                    DotThu = n.DotThu,
                    Khoa = n.Khoa,
                })
                .ToList();
            return result;
        }

        public FileDownloadVM DownloadAdmissionFiles(int id)
        {
            var hocBa = _context.HocBa.Where(x => x.MaHoSoThpt == id).ToList();
            if (hocBa.Count <= 0)
                throw new Exception("Thí sinh chưa upload học bạ!");
            var webRoot = _webHostEvironment.ContentRootPath;
            var fileName = $"HocBa_MaHoSoTHPT_{id}.zip";

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
                {
                    for (int i = 0; i < hocBa.Count; i++)
                    {
                        var pathFile = webRoot + hocBa[i].DuongDanHocBa;
                        zipArchive.CreateEntryFromFile(pathFile, Path.GetFileName(pathFile));
                    }
                }
                byte[] fileContents = memoryStream.ToArray();
                if (fileContents == null || !fileContents.Any())
                    throw new Exception("Không tìm thấy files học bạ");

                return new FileDownloadVM
                {
                    fileContents = fileContents,
                    fileType = "application/zip",
                    fileName = fileName
                };
            }
        }

        public bool UpdateSendMailStatusById(int id)
        {
            var admission = _context.HoSoThpts.FirstOrDefault(x => x.Id == id);
            if (admission == null)
                throw new Exception($"Không tìm thấy hồ sơ có ID {id}!");
            admission.DaGuiMail = true;
            _context.HoSoThpts.Update(admission);
            return _context.SaveChanges() > 0;
        }

        public List<BangDiem> GetBangDiem()
        {
            var data = _context.HoSoThpts
               .Select(n => new BangDiem
               {
                   Id = n.Id,
                   HoVaTen = n.HoVaTen,
                   DotId = n.DotId,
                   Toan11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Toan,
                   Van11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Van,
                   Anh11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Anh,
                   Phap11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Phap,
                   Ly11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Ly,
                   Hoa11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Hoa,
                   Sinh11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Sinh,
                   Su11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Su,
                   Dia11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Dia,
                   Gdcd11 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP11").Gdcd,
                   ToanHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Toan,
                   VanHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Van,
                   AnhHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Anh,
                   PhapHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Phap,
                   LyHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Ly,
                   HoaHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Hoa,
                   SinhHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Sinh,
                   SuHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Su,
                   DiaHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Dia,
                   GdcdHK1_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "HK1_LOP12").Gdcd,
                   ToanCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Toan,
                   VanCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Van,
                   AnhCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Anh,
                   PhapCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Phap,
                   LyCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Ly,
                   HoaCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Hoa,
                   SinhCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Sinh,
                   SuCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Su,
                   DiaCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Dia,
                   GdcdCN_Lop12 = _context.BangDiemThpts.Where(x => x.MaHoSoThpt == n.Id).FirstOrDefault(x => x.MaHocKyLop == "CN_LOP12").Gdcd,
               })
               .ToList();

            return data;
        }
    }
}
