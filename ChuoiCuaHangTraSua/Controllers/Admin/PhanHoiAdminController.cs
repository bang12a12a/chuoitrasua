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
    public class PhanHoiAdminController : Controller
    {
        // GET: PhanHoi

        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = new List<PhanHoiModel>();
            
            var list = (from ph in db.PhanHois
                        join kh in db.KhachHangs on ph.MaKhachHang equals kh.Id
                        select new
                        {
                            Id = ph.Id,
                            MaKhachHang = ph.MaKhachHang,
                            NoiDung = ph.NoiDung,
                            TenKhachHang = kh.TenDangNhap,
                            DaXem = ph.DaXem
                        }).OrderByDescending(x => x.Id).ToList();

           
            var i = 0;
            foreach (var item in list)
            {
                i++;
                var itemmodel = new PhanHoiModel();
                itemmodel.STT = i;
                itemmodel.Id = item.Id;
                itemmodel.UserId = item.MaKhachHang;
                itemmodel.Content = item.NoiDung;
                itemmodel.UserName = item.TenKhachHang;
                itemmodel.DaXem = item.DaXem;
                model.Add(itemmodel);
            }
            


            
            return View(model);
        }
        
        public ActionResult SuaPhanHoi(int? maphanhoi)
        {
            
            var phanhoi = db.PhanHois.Find(maphanhoi);
            phanhoi.DaXem = 1;
            db.SaveChanges();
            return RedirectToAction("Index", "PhanHoiAdmin");
        }
    }
}