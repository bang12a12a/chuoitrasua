using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Common;
namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class TestController : Controller
    {
        // GET: Test
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = db.SanPhams.ToList();
            return View(model);
        }
        public ActionResult okoko()
        {
            return View();
        }
        public ActionResult Test()
        {
            var model =new List<TestModel>();
            var list = db.KhuyenMais.ToList();
            DateTime now =DateTime.Now;
            
            foreach(var item in list)
            {
                var itemmodel = new TestModel();
                itemmodel.NgayBD = item.NgayBatDau;
                itemmodel.NgayKT = item.NgayKetThuc;

                itemmodel.ChonNgay = new LamTronThoiGian().LamTron(item.NgayKetThuc, itemmodel.NgayBD);

                model.Add(itemmodel);
            }
            return View(model);
        }
    }
}