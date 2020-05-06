using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using PagedList;

namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class NhomSanPhamAdminController : Controller
    {
        // GET: NhomSanPhamAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model =new List<NhomSanPhamModel>();
            var list = db.LoaiSanPhams.ToList();
            
            var i = 0;
            foreach (var item in list)
            {
                i++;
                var itemmodel = new NhomSanPhamModel();
                itemmodel.STT = i;
                itemmodel.Id = item.Id;
                itemmodel.Ten = item.TenLoaiSanPham;
                itemmodel.SPChinhPhu = item.SanPhamChinh;
                if(item.SanPhamChinh == 1)
                {
                    itemmodel.TenChinhPhu = "Sản phẩm chính";
                }
                else
                {
                    itemmodel.TenChinhPhu = "Sản phẩm phụ";
                }
                if(item.Id == 12 || item.Id == 13 || item.Id == 14)
                {

                }
                else
                {
                    model.Add(itemmodel);
                }
               
            }
            return View(model);
        }
        public ActionResult CreateNhomSanPham()
        {
            var model = new NhomSanPhamModel();
            model.SelectSPChinhPhu = new SelectList(db.SPChinh_Phu, "Id", "SPChinh_Phu1", 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateNhomSanPham(NhomSanPhamModel model)
        {
            LoaiSanPham loaisanpham = new LoaiSanPham();
            loaisanpham.TenLoaiSanPham = model.Ten;
            loaisanpham.SanPhamChinh = model.SPChinhPhu;
           
            var dao = new CategoryDao();
            dao.Insert(loaisanpham);
            return RedirectToAction("Index", "NhomSanPhamAdmin");
        }
        public ActionResult EditNhomSanPham(int? manhomsp)
        {
            var nhomsp = db.LoaiSanPhams.Find(manhomsp);
            var model = new NhomSanPhamModel();
            model.Id = nhomsp.Id;
            model.Ten = nhomsp.TenLoaiSanPham;
            model.SelectSPChinhPhu = new SelectList(db.SPChinh_Phu, "Id", "SPChinh_Phu1",nhomsp.SanPhamChinh);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditNhomSanPham(NhomSanPhamModel model)
        {
            var nhomsp = db.LoaiSanPhams.Find(model.Id);

            nhomsp.TenLoaiSanPham = model.Ten;
            nhomsp.SanPhamChinh = model.SPChinhPhu;
            db.SaveChanges();
            return RedirectToAction("Index", "NhomSanPhamAdmin");
        }
        public ActionResult DeleteNhomSanPham(int manhomsp)
        {
            new CategoryDao().Delete(manhomsp);
            return RedirectToAction("Index", "NhomSanPhamAdmin");
        }
    }
}