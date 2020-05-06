using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
using PagedList;
using PagedList.Mvc;

namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class HoaDonBanAdminController : Controller
    {

        // GET: HoaDonBanAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var list = new List<HoaDonBanModel>();
           
            var listproduct = (from hdb in db.HoaDonBans
                               join kh in db.KhachHangs on hdb.MaKhach equals kh.Id
                               where hdb.Duyet ==0
                               select new
                               {
                                   Id = hdb.Id,
                                   NgayBan = hdb.NgayBan,
                                   TenKhach = kh.HoTen,
                                   DiaChi = kh.DiaChi,
                                   DienThoai = kh.SDT,
                                   KhuyenMai = hdb.GiamGia,
                                   TongTien = hdb.TongTien,
                                   DaDuyet = hdb.Duyet,
                                   DaThanhToan = hdb.DaThanhToan
                               }).OrderByDescending(x => x.Id);


            int i = 0;

            foreach (var item in listproduct)
            {
                i++;
                var model = new HoaDonBanModel();
                model.Id =item.Id;
                model.STT = i;
                model.NgayBan = item.NgayBan;
                model.HoTen = item.TenKhach;
                model.DiaChi = item.DiaChi;
                model.KhuyenMai = item.KhuyenMai;
                model.SDT = item.DienThoai;
                model.TongTien = item.TongTien;
                model.Status = item.DaDuyet;
                model.DaThanhToan = item.DaThanhToan;
                list.Add(model);

            }
            return View(list);
        }
        public ActionResult DanhSachHDB_DaDuyet()
        {
            var list = new List<HoaDonBanModel>();

            var listproduct = (from hdb in db.HoaDonBans
                               join kh in db.KhachHangs on hdb.MaKhach equals kh.Id
                               where hdb.Duyet == 1
                               select new
                               {
                                   Id = hdb.Id,
                                   NgayBan = hdb.NgayBan,
                                   TenKhach = kh.HoTen,
                                   DiaChi = kh.DiaChi,
                                   DienThoai = kh.SDT,
                                   KhuyenMai = hdb.GiamGia,
                                   TongTien = hdb.TongTien,
                                   DaDuyet = hdb.Duyet,
                                   DaThanhToan = hdb.DaThanhToan
                               }).OrderByDescending(x => x.Id);


            int i = 0;

            foreach (var item in listproduct)
            {
                i++;
                var model = new HoaDonBanModel();
                model.Id = item.Id;
                model.STT = i;
                model.NgayBan = item.NgayBan;
                model.HoTen = item.TenKhach;
                model.DiaChi = item.DiaChi;
                model.KhuyenMai = item.KhuyenMai;
                model.SDT = item.DienThoai;
                model.TongTien = item.TongTien;
                model.Status = item.DaDuyet;
                model.DaThanhToan = item.DaThanhToan;
                list.Add(model);

            }
            return View(list);
        }
        public ActionResult EditDuyet(string mahdb)
        {
            var hoadonban = db.HoaDonBans.Find(mahdb);
            hoadonban.Duyet = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "HoaDonBanAdmin");
        }
        public ActionResult EditThanhToan(string mahdb)
        {
            var hoadonban = db.HoaDonBans.Find(mahdb);
            hoadonban.DaThanhToan = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "HoaDonBanAdmin");
        }
        public ActionResult DuyetHoaDon(string mahoadon)
        {
            var model = new HoaDonBanModel();
            var hoadonban = db.HoaDonBans.FirstOrDefault(x => x.Id == mahoadon);
            model.Id = hoadonban.Id;
            var khachhang = db.KhachHangs.FirstOrDefault(x => x.Id == hoadonban.MaKhach);
            model.HoTen = khachhang.HoTen;
            model.DiaChi =khachhang.DiaChi;
            model.SDT = khachhang.SDT;
            
            ViewBag.TotalMoney =hoadonban.TongTien;
            model.SelectChiNhanh = new SelectList(db.ChiNhanhs, "Id", "TenChiNhanh", 1);
            return View(model);
        }
        [HttpPost]
        public ActionResult DuyetHoaDon(HoaDonBanModel model)
        {
            var hoadonban = db.HoaDonBans.Find(model.Id);
            hoadonban.MaChiNhanh = model.MaChiNhanh;
            hoadonban.Duyet = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "HoaDonBanAdmin");
        }
    }
}