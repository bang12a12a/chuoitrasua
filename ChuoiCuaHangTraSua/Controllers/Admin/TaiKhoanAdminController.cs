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
    public class TaiKhoanAdminController : Controller
    {
        // GET: Default
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult DoiMatKhau()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(KhachHangModel model)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if (session != null)
            {
                var khachhang = db.KhachHangs.Find(session.UserId);
                khachhang.MatKhau = ChuoiCuaHangTraSua.Common.Encrytor.MD5Hash(model.MatKhau);
                db.SaveChanges();
                ViewBag.DoiMatKhau = "Bạn đã đổi mật khẩu thành công";
            }

            return View();
        }
    }
}