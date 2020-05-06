using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Common;

namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class HoaDonNhapAdminController : Controller
    {
        // GET: HoaDonNhapAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model =new  List<HoaDonNhapModel>();
            var list = db.HoaDonNhaps.ToList();
            int i = 0;
            foreach(var item in list)
            {
                i++;
                var itemmodel = new HoaDonNhapModel();
                itemmodel.Id = item.Id;
                itemmodel.STT = i;
                itemmodel.TenNCC = new NhaCungCapDao().getById(item.MaNCC).TenNCC;
                itemmodel.TenDangNhap = new KhachHangDao().viewDetail(item.MaNhanVien).TenDangNhap;
                itemmodel.TongTien = item.TongTien;
                itemmodel.NgayNhap = item.NgayNhap;
                itemmodel.ChietKhau =String.Format("{0:0,0}", item.ChietKhau);
                model.Add(itemmodel);
                    
            }
            return View(model);
        }
        public ActionResult CreateHDN()
        {

            var session_mahdn = (ChuoiCuaHangTraSua.Common.MaHoaDonNhap)Session[ChuoiCuaHangTraSua.Common.Constants.MAHDN_SESSION];
            var list_cthdn = new List<CTHDNhapModel>();
            var model = new HoaDonNhapModel();
            if (session_mahdn != null)
            {
                var list = db.ChiTietHDNs.Where(x => x.MaHDN == session_mahdn.Id).ToList();
                int i = 0;
                foreach (var item in list)
                {
                    i++;
                    var item_cthdn = new CTHDNhapModel();
                    item_cthdn.Id = item.Id;
                    item_cthdn.STT = i;
                    item_cthdn.TenNguyenLieu = new NguyenLieuDao().getById(item.MaNguyenLieu).TenNguyenLieu;
                    item_cthdn.TenDonViTinh = new DonViTinhDao().getById(item.MaDonViTinh).TenDonViTinh;
                    item_cthdn.SoLuong = item.SoLuong.ToString();
                    item_cthdn.GiaNhap = item.GiaNhap.ToString();
                    item_cthdn.ChietKhau = item.ChietKhau.ToString();
                    item_cthdn.ThanhTien = item.ThanhTien;
                    list_cthdn.Add(item_cthdn);
                }
                var tongtienhdn = db.ChiTietHDNs.Where(x => x.MaHDN == session_mahdn.Id).Sum(x => x.ThanhTien);
                model.TongTien = tongtienhdn;
            }
            else
            {
                list_cthdn = null;
            }
            ViewBag.ListChiTietHDN = list_cthdn;
            model.SelectNCC = new SelectList(db.NhaCungCaps, "Id", "TenNCC", 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateHDN(HoaDonNhapModel model)
        {

                var session_hdn = (ChuoiCuaHangTraSua.Common.MaHoaDonNhap)Session[ChuoiCuaHangTraSua.Common.Constants.MAHDN_SESSION];
                var hoadonnhap = new HoaDonNhap();
                hoadonnhap.Id = session_hdn.Id;
                hoadonnhap.MaNCC = model.MaNhaCungCap;
                hoadonnhap.MaNhanVien = 3;
                DateTime now = DateTime.Now;
                hoadonnhap.NgayNhap = now;
                hoadonnhap.TongTien = db.ChiTietHDNs.Where(x => x.MaHDN == session_hdn.Id).Sum(x => x.ThanhTien);
                hoadonnhap.ChietKhau =new ChuoiCuaHangTraSua.Common.ConvertMoney().ConvertTien(model.ChietKhau);
                db.HoaDonNhaps.Add(hoadonnhap);
                db.SaveChanges();
                return RedirectToAction("Index", "HoaDonNhapAdmin");
            
            

        }
        public ActionResult XemChiTietHDN(string mahoadon)
        {
            var model = new List<CTHDNhapModel>();
            var list = db.ChiTietHDNs.Where(x => x.MaHDN == mahoadon).ToList();
            int i = 0;
            foreach(var item in list)
            {
                var itemmodel = new CTHDNhapModel();
                i++;
                itemmodel.TenNguyenLieu = new NguyenLieuDao().getById(item.MaNguyenLieu).TenNguyenLieu;
                itemmodel.TenDonViTinh = new DonViTinhDao().getById(item.MaDonViTinh).TenDonViTinh;
                itemmodel.GiaNhap = item.GiaNhap.ToString();
                itemmodel.SoLuong = item.SoLuong.ToString();
                itemmodel.ChietKhau = item.ChietKhau.ToString();
                itemmodel.ThanhTien = item.ThanhTien;
                itemmodel.STT = i;
                model.Add(itemmodel);
            }
            var hoadonnhap = db.HoaDonNhaps.FirstOrDefault(x => x.Id == mahoadon);
            var hdnmodel = new HoaDonNhapModel();
            hdnmodel.TenDangNhap = db.KhachHangs.FirstOrDefault(x => x.Id == hoadonnhap.MaNhanVien).TenDangNhap;
            hdnmodel.NgayNhap = hoadonnhap.NgayNhap;
            hdnmodel.TenNCC = db.NhaCungCaps.FirstOrDefault(x => x.Id == hoadonnhap.MaNCC).TenNCC;
            hdnmodel.TongTien = hoadonnhap.TongTien;
            hdnmodel.ChietKhau = String.Format("{0:0,0}", hoadonnhap.ChietKhau);
            var chietkhau = new ChuoiCuaHangTraSua.Common.ConvertMoney().ConvertTien(hdnmodel.ChietKhau);
            hdnmodel.PhaiTra = (hdnmodel.TongTien -  chietkhau);
            ViewBag.HoaDonNhap = hdnmodel;
            return View(model);
        }
        public ActionResult CreateCTHDN()
        {
            var model =  new CTHDNhapModel();
            model.SelectDonViTinh = new SelectList(db.DonViTinhs, "Id", "TenDonViTinh", 0);
            model.SelectNguyenLieu = new SelectList(db.NguyenLieux, "Id", "TenNguyenLieu", 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCTHDN(CTHDNhapModel model)
        {
            if (ModelState.IsValid)
            {
                var cthdn = new ChiTietHDN();
                var session_hdn = (ChuoiCuaHangTraSua.Common.MaHoaDonNhap)Session[ChuoiCuaHangTraSua.Common.Constants.MAHDN_SESSION];
                if (session_hdn != null)
                {
                    cthdn.MaHDN = session_hdn.Id;
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    var mahdn = dt.Day.ToString() + dt.Month.ToString() + dt.Year.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                    var session_mahdnhap = new ChuoiCuaHangTraSua.Common.MaHoaDonNhap();
                    session_mahdnhap.Id = mahdn;
                    Session.Add(ChuoiCuaHangTraSua.Common.Constants.MAHDN_SESSION, session_mahdnhap);
                    cthdn.MaHDN = mahdn;
                }

                cthdn.MaDonViTinh = model.MaDonViTinh;
                cthdn.MaNguyenLieu = model.MaNguyenLieu;

                cthdn.SoLuong = new ChuoiCuaHangTraSua.Common.ConvertMoney().ConvertTien(model.SoLuong);
                cthdn.GiaNhap = new ChuoiCuaHangTraSua.Common.ConvertMoney().ConvertTien(model.GiaNhap);
                cthdn.ChietKhau = new ChuoiCuaHangTraSua.Common.ConvertMoney().ConvertTien(model.ChietKhau);
                cthdn.ThanhTien = cthdn.GiaNhap * cthdn.SoLuong - cthdn.ChietKhau;
                db.ChiTietHDNs.Add(cthdn);

                var nguyenlieu_donvi = new NguyenLieuDonViDao().getBy_NLId_DVId(model.MaNguyenLieu,model.MaDonViTinh);
                if (nguyenlieu_donvi !=null)
                {
                    var nl_dv = db.NguyenLieu_DonVi.Find(nguyenlieu_donvi.Id);
                    int? soluong = nguyenlieu_donvi.SoLuong;
                    nl_dv.SoLuong = soluong +cthdn.SoLuong;
                    nl_dv.GiaNhap = cthdn.GiaNhap;
                    db.SaveChanges();
                }
                else
                {
                    var nl_dv = new NguyenLieu_DonVi();
                    nl_dv.MaNguyenLieu = cthdn.MaNguyenLieu;
                    nl_dv.MaDonViTinh = cthdn.MaDonViTinh;
                    nl_dv.SoLuong = cthdn.SoLuong;
                    nl_dv.GiaNhap = cthdn.GiaNhap;
                    db.NguyenLieu_DonVi.Add(nl_dv);
                }
                
                
                db.SaveChanges();
                return RedirectToAction("CreateHDN", "HoaDonNhapAdmin");
            }
            else
            {
                return RedirectToAction("CreateCTHDN", "HoaDonNhapAdmin");
            }
            
        }
        public ActionResult SuaCTHDN(int id,string soluong, string gianhap, string chietkhau)
        {
            var cthdn = db.ChiTietHDNs.FirstOrDefault(x=>x.Id == id);
            var convert = new ChuoiCuaHangTraSua.Common.ConvertMoney();
            cthdn.SoLuong = convert.ConvertTien(soluong);
            cthdn.GiaNhap = convert.ConvertTien(gianhap);
            cthdn.ChietKhau = convert.ConvertTien(chietkhau);
            cthdn.ThanhTien = cthdn.SoLuong * cthdn.GiaNhap - cthdn.ChietKhau;
            db.SaveChanges();
            return RedirectToAction("CreateHDN", "HoaDonNhapAdmin");
        }
    }
}