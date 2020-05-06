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
    public class DonViTinhAdminController : Controller
    {
        // GET: DonViTinhAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = new List<DonViTinhModel>();
            var list = db.DonViTinhs.OrderByDescending(x => x.Id).ToList();
            int i = 0;
            foreach (var item in list)
            {
                i++;
                var itemmodel = new DonViTinhModel();
                itemmodel.Id = item.Id;
                itemmodel.TenDonViTinh = item.TenDonViTinh;
                itemmodel.STT = i;
                model.Add(itemmodel);
            }
            return View(model);
        }
        public ActionResult CreateDonViTinh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDonViTinh(DonViTinhModel model)
        {
            var donvitinh = new DonViTinh();
            donvitinh.TenDonViTinh = model.TenDonViTinh;

            db.DonViTinhs.Add(donvitinh);
            db.SaveChanges();
            return RedirectToAction("Index", "DonViTinhAdmin");
        }
        public ActionResult EditDonViTinh(int? madonvitinh)
        {
            var model = new DonViTinhModel();
            var donvitinh = db.DonViTinhs.FirstOrDefault(x => x.Id == madonvitinh);
            model.Id = donvitinh.Id;
            model.TenDonViTinh = donvitinh.TenDonViTinh;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditDonViTinh(DonViTinhModel model)
        {
            var donvitinh = db.DonViTinhs.Find(model.Id);
            donvitinh.TenDonViTinh = model.TenDonViTinh;
            db.SaveChanges();
            return RedirectToAction("Index", "DonViTinhAdmin");
        }
        public ActionResult DeleteDonViTinh(int? madonvitinh)
        {
            var donvitinh = db.DonViTinhs.Find(madonvitinh);
            db.DonViTinhs.Remove(donvitinh);
            db.SaveChanges();
            return RedirectToAction("Index", "DonViTinhAdmin");
        }
    }
}