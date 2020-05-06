using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.EF;
namespace ChuoiCuaHangTraSua.Controllers.NguoiDung
{
    public class DanhSachCuaHangController : Controller
    {
        // GET: DanhSachCuaHang
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var list = db.ChiNhanhs.ToList();
            return View(list);
        }
    }
}