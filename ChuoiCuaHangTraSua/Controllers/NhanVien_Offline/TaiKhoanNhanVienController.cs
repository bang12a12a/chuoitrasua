using ChuoiCuaHangTraSua.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;

namespace ChuoiCuaHangTraSua.Controllers.NhanVien_Offline
{
    public class TaiKhoanNhanVienController : Controller
    {
        // GET: TaiKhoanNhanVien
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult DoiMatKhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(NhanVienModel model)
        {
            var session = (ChuoiCuaHangTraSua.Common.NhanVienLogin)Session[ChuoiCuaHangTraSua.Common.Constants.NHANVIEN_SESSION];
            if (session != null)
            {
                var nhanvien = db.NhanViens.Find(session.UserId);
                nhanvien.MatKhau = ChuoiCuaHangTraSua.Common.Encrytor.MD5Hash(model.MatKhau);
                db.SaveChanges();
                ViewBag.DoiMatKhau = "Bạn đã đổi mật khẩu thành công";
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session[ChuoiCuaHangTraSua.Common.Constants.NHANVIEN_SESSION] = null;
            return Redirect("/Home/Index");
        }
    }
}