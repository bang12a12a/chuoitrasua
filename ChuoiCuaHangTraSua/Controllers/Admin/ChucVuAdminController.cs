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
    public class ChucVuAdminController : Controller
    {
        // GET: ChucVuAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = new List<ChucVuModel>();
            var list = db.ChucVus.ToList();
            int i = 0;
            foreach(var item in list)
            {
                i++;
                var itemmodel = new ChucVuModel();
                itemmodel.Id = item.Id;
                itemmodel.TenChucVu = item.TenChucVu;
                itemmodel.Luong = item.Luong;
                itemmodel.STT = i;
                model.Add(itemmodel);
            }
            return View(model);
        }
        public ActionResult CreateChucVu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateChucVu(ChucVuModel model)
        {
            var chucvu = new ChucVu();
            chucvu.TenChucVu = model.TenChucVu;
            chucvu.Luong = model.Luong;
            db.ChucVus.Add(chucvu);
            db.SaveChanges();
            return RedirectToAction("Index", "ChucVuAdmin");
        }

        public ActionResult EditChucVu(int machucvu)
        {
            var model = new ChucVuModel();
            var chucvu = db.ChucVus.FirstOrDefault(x => x.Id == machucvu);
            model.Id = chucvu.Id;
            model.TenChucVu = chucvu.TenChucVu;
            model.Luong = chucvu.Luong;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditChucVu(ChucVuModel model)
        {
            var chucvu = db.ChucVus.Find(model.Id);
            chucvu.TenChucVu = model.TenChucVu;
            chucvu.Luong = model.Luong;
            db.SaveChanges();
            return RedirectToAction("Index", "ChucVuAdmin");
        }
        public ActionResult DeleteChucVu(int machucvu)
        {
            var chucvu = db.ChucVus.Find(machucvu);
            db.ChucVus.Remove(chucvu);
            db.SaveChanges();
            return RedirectToAction("Index", "ChucVuAdmin");
        }
    }
}