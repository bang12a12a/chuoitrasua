using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Common;
namespace ChuoiCuaHangTraSua.Controllers.NguoiDung
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Latte()
        {
            return PartialView(db.SanPhams.Where(x=>x.MaLoaiSanPham==6).ToList());
        }
        public PartialViewResult DacBietEstore()
        {
            return PartialView(db.SanPhams.Where(x => x.MaLoaiSanPham == 8).ToList());
        }
        public PartialViewResult TraSua()
        {
            return PartialView(db.SanPhams.Where(x => x.MaLoaiSanPham == 10).ToList());
        }
        public PartialViewResult TraNguyenChat()
        {
            return PartialView(db.SanPhams.Where(x => x.MaLoaiSanPham == 4).ToList());
        }
        public PartialViewResult SangTao()
        {
            return PartialView(db.SanPhams.Where(x => x.MaLoaiSanPham == 9).ToList());
        }
        public PartialViewResult DaXay()
        {
            return PartialView(db.SanPhams.Where(x => x.MaLoaiSanPham == 11).ToList());
        }
        public PartialViewResult Topping()
        {
            return PartialView(db.SanPhams.Where(x => x.MaLoaiSanPham == 1).ToList());
        }
        public ActionResult ChiTietSanPham(int? masanpham)
        {
            ViewBag.SanPham = db.SanPhams.FirstOrDefault(x => x.Id == masanpham);

            

            var model = new List<DanhGiaModel>();
            var list = db.BinhLuans.Where(x => x.MaSanPham == masanpham).OrderByDescending(x=>x.Id).ToList();
            foreach (var item in list)
            {
                var itemmodel = new DanhGiaModel();
                itemmodel.Id = item.Id;
                itemmodel.MaKhachHang = item.MaKhachHang;
                itemmodel.MaSanPham = masanpham;
                itemmodel.NoiDung = item.NoiDung;
                itemmodel.TenDangNhap = new KhachHangDao().viewDetail(item.MaKhachHang).TenDangNhap;
                itemmodel.DanhGia = item.DanhGia;
                DateTime dtime = DateTime.Now;
                itemmodel.ThoiGian = new LamTronThoiGian().LamTron(dtime, item.ThoiGian);
                model.Add(itemmodel);
            }
            var tongsao = list.Sum(x => x.DanhGia);
            var tongdanhgia = list.Count;
            if(tongdanhgia == 0)
            {
                ViewBag.Star = 0;
            }
            else
            {
                var chia = tongsao / tongdanhgia;
                ViewBag.Star = chia;
            }
           

            return View(model);
            
        }
    }
}