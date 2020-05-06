using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.Model;
using PagedList;
using PagedList.Mvc;
namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class KhachHangAdminController : Controller
    {
        // GET: KhachHangAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var list = new List<KhachHangModel>();
            
            var listproduct = db.KhachHangs.ToList();


            int i = 0;

            foreach (var item in listproduct)
            {
                i++;
                var model = new KhachHangModel();
                model.Id = item.Id;
                model.STT = i;
                model.HoTen = item.HoTen;
                model.TenDangNhap = item.TenDangNhap;
                model.DiaChi = item.DiaChi;
                model.SDT = item.SDT;
                model.Email = item.Email;

                list.Add(model);
            }
            return View(list);
        }
        public ActionResult DeleteKhachHang(int? userid)
        {
            var khachhang = db.KhachHangs.Find(userid);
            db.KhachHangs.Remove(khachhang);
            db.SaveChanges();
            return RedirectToAction("Index", "KhachHangAdmin");
        }
    }
}