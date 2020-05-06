using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
namespace ChuoiCuaHangTraSua.Models.Model
{
    public class KhoNguyenLieuAdminController : Controller
    {
        // GET: KhoNguyenLieuAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = new List<KhoNguyenLieu>();
            var list = db.NguyenLieu_DonVi.ToList();
            int i = 0;
            foreach(var item in list)
            {
                i++;
                var itemmodel = new KhoNguyenLieu();
                itemmodel.Id = item.Id;
                itemmodel.STT = i;
                itemmodel.TenDonVi = new DonViTinhDao().getById(item.MaDonViTinh).TenDonViTinh;
                itemmodel.TenNguyenLieu = new NguyenLieuDao().getById(item.MaNguyenLieu).TenNguyenLieu;
                itemmodel.SoLuong = item.SoLuong;
                model.Add(itemmodel);
            }
            return View(model);
        }
    }
}