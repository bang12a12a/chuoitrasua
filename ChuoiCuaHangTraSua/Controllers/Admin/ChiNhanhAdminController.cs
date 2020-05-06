using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class ChiNhanhAdminController : Controller
    {
        // GET: ChiNhanhAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var list = db.ChiNhanhs.ToList();
            var model =new List<ChiNhanhModel>();
            int i = 0;
            foreach(var item in list)
            {
                var itemmodel = new ChiNhanhModel();
                itemmodel.Id = item.Id;
                itemmodel.TenChiNhanh = item.TenChiNhanh;
                itemmodel.DiaChi = item.DiaChi;
                i++;
                itemmodel.STT = i;
                itemmodel.GhiChu = item.GhiChu;
                model.Add(itemmodel);
            }
            return View(model);
        }
        public ActionResult CreateChiNhanh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateChiNhanh(ChiNhanhModel model)
        {
            var chinhanh = new ChiNhanh();
            chinhanh.TenChiNhanh = model.TenChiNhanh;
            chinhanh.DiaChi = model.DiaChi;
            chinhanh.GhiChu = model.GhiChu;
            new ChiNhanhDao().Insert(chinhanh);
            return RedirectToAction("Index","ChiNhanhAdmin");
        }
        public ActionResult EditChiNhanh(int machinhanh)
        {
            var chinhanh = db.ChiNhanhs.FirstOrDefault(x => x.Id == machinhanh);
            var model = new ChiNhanhModel();
            model.Id = machinhanh;
            model.TenChiNhanh = chinhanh.TenChiNhanh;
            model.DiaChi = chinhanh.DiaChi;
            model.GhiChu = chinhanh.GhiChu;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditChiNhanh(ChiNhanhModel model)
        {
            var chinhanh = db.ChiNhanhs.Find(model.Id);
            chinhanh.TenChiNhanh = model.TenChiNhanh;
            chinhanh.DiaChi = model.DiaChi;
            chinhanh.GhiChu = model.GhiChu;
            db.SaveChanges();
            return RedirectToAction("Index", "ChiNhanhAdmin");
        }
        public ActionResult DeleteChiNhanh(int machinhanh)
        {
            var chinhanh = db.ChiNhanhs.Find(machinhanh);
            db.ChiNhanhs.Remove(chinhanh);
            db.SaveChanges();
            return RedirectToAction("Index", "ChiNhanhAdmin");
        }
        public ActionResult XemChiTiet(int machinhanh)
        {
            var model = new List<NhanVienModel>();
            var list = db.NhanViens.Where(x => x.MaChiNhanh == machinhanh).ToList();
            int i = 0;
            foreach(var item in list)
            {
                i++;
                var itemmodel = new NhanVienModel();
                itemmodel.Id = item.Id;
                itemmodel.STT = i;
                itemmodel.HoTen = item.HoTen;
                itemmodel.DiaChi = item.DiaChi;
                itemmodel.SDT = item.SDT;
                itemmodel.TenDangNhap = item.TenDangNhap;
                itemmodel.TenChiNhanh = db.ChiNhanhs.FirstOrDefault(x => x.Id == item.MaChiNhanh).TenChiNhanh;
                itemmodel.TenChucVu = db.ChucVus.FirstOrDefault(x => x.Id == item.MaChucVu).TenChucVu;
                itemmodel.Luong = db.ChucVus.FirstOrDefault(x => x.Id == item.MaChucVu).Luong;
                model.Add(itemmodel);
            }
            ViewBag.ChiNhanh = db.ChiNhanhs.FirstOrDefault(x => x.Id == machinhanh);
            return View(model);
        }
        
    }
}