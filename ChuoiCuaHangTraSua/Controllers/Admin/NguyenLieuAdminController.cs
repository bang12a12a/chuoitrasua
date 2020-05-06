using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class NguyenLieuAdminController : Controller
    {
        // GET: NguyenLieu
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = new List<NguyenLieuModel>();
            var list = db.NguyenLieux.OrderByDescending(x=>x.Id).ToList();
            int i = 0;
            foreach(var item in list)
            {
                i++;
                var itemmodel = new NguyenLieuModel();
                itemmodel.Id = item.Id;
                itemmodel.TenNguyenLieu = item.TenNguyenLieu;
                itemmodel.STT = i;
                model.Add(itemmodel);
            }
            return View(model);
        }
        public ActionResult CreateNguyenLieu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNguyenLieu(NguyenLieuModel model)
        {
            var nguyenlieu = new NguyenLieu();
            nguyenlieu.TenNguyenLieu = model.TenNguyenLieu;
           
            db.NguyenLieux.Add(nguyenlieu);
            db.SaveChanges();
            return RedirectToAction("Index","NguyenLieuAdmin");
        }
        public ActionResult EditNguyenLieu(int? manguyenlieu)
        {
            var model = new NguyenLieuModel();
            var nguyenlieu = db.NguyenLieux.FirstOrDefault(x => x.Id == manguyenlieu);
            model.Id = nguyenlieu.Id;
            model.TenNguyenLieu = nguyenlieu.TenNguyenLieu;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditNguyenLieu(NguyenLieuModel model)
        {
            var nguyenlieu = db.NguyenLieux.Find(model.Id);
            nguyenlieu.TenNguyenLieu = model.TenNguyenLieu;
            db.SaveChanges();
            return RedirectToAction("Index", "NguyenLieuAdmin");
        }
        public ActionResult DeleteNguyenLieu(int? manguyenlieu)
        {
            var nguyenlieu = db.NguyenLieux.Find(manguyenlieu);
            db.NguyenLieux.Remove(nguyenlieu);
            db.SaveChanges();
            return RedirectToAction("Index", "NguyenLieuAdmin");
        }
    }
}