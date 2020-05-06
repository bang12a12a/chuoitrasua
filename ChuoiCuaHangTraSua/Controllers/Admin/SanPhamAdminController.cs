using ChuoiCuaHangTraSua.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
using PagedList;
using PagedList.Mvc;
namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class SanPhamAdminController : Controller
    {
        // GET: SanPhamAdmin
        TraSuaEntities db = new TraSuaEntities();
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return file.FileName;
        }
        public ActionResult SanPhamChinh()
        {
            var list = new List<SanPhamModel>();
            
            var listproduct = (from sp in db.SanPhams
                               join lsp in db.LoaiSanPhams on sp.MaLoaiSanPham equals lsp.Id
                               where lsp.SanPhamChinh == 1
                               select new
                               {
                                   Id = sp.Id,
                                   TenSanPham = sp.TenSanPham,
                                   GiaBan = sp.GiaBan,
                                   KhuyenMai = sp.KhuyenMai,
                                   Anh = sp.Anh,
                                   TenLoaiSanPham = lsp.TenLoaiSanPham
                               }).OrderByDescending(x => x.Id);
                              
            
            int i = 0;
            
            foreach (var item in listproduct)
            {
                i++;
                var model = new SanPhamModel();
                model.Id = item.Id;
                model.STT = i;
                model.Ten = item.TenSanPham;
                model.TenLoai =item.TenLoaiSanPham;
                model.GiaBan = item.GiaBan;
                model.KhuyenMai = item.KhuyenMai;
                model.Anh = item.Anh;
                
                list.Add(model);

            }
            return View(list);
        }
        
        public ActionResult CreateSanPhamChinh()
        {
            var model = new SanPhamModel();
            model.SelectMaLoai = new SelectList(db.LoaiSanPhams.Where(x=>x.SanPhamChinh==1), "Id", "TenLoaiSanPham", 1);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateSanPhamChinh(SanPhamModel model)
        {
            SanPham sanpham = new SanPham();
            sanpham.TenSanPham = model.Ten;
            sanpham.MaLoaiSanPham = model.MaLoai;
            sanpham.GiaBan = model.GiaBan;
            sanpham.KhuyenMai = model.GiaBan;
            sanpham.Anh = model.Anh;
            sanpham.MoTa = model.MoTa;
            var dao = new ProductDao();
            dao.Insert(sanpham);
            return RedirectToAction("SanPhamChinh", "SanPhamAdmin");
        }
        
        public ActionResult EditProductChinh(int? productid)
        {
            var product = new ProductDao().viewDetail(productid);
            var model = new SanPhamModel();
            model.SelectMaLoai = new SelectList(db.LoaiSanPhams.Where(x=>x.SanPhamChinh==1), "Id", "TenLoaiSanPham", product.MaLoaiSanPham);
            model.Id = product.Id;
            model.Ten = product.TenSanPham;
            model.Anh = product.Anh;
            model.MaLoai = product.MaLoaiSanPham;
            model.GiaBan = product.GiaBan;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProductChinh(SanPhamModel model)
        {
            SanPham book = db.SanPhams.FirstOrDefault(x=>x.Id==model.Id);
            book.TenSanPham = model.Ten;
            book.MaLoaiSanPham = model.MaLoai;
            book.GiaBan = model.GiaBan;
            book.Anh = model.Anh;
            db.SaveChanges();
            return RedirectToAction("SanPhamChinh", "SanPhamAdmin");

        }
        public ActionResult DeleteProductChinh(int productid)
        {
            new ProductDao().Delete(productid);
            return RedirectToAction("SanPhamChinh", "SanPhamAdmin");
        }

        // sản phẩm phụ
        public ActionResult SanPhamPhu()
        {
            var list = new List<SanPhamModel>();
            
            var listproduct = (from sp in db.SanPhams
                               join lsp in db.LoaiSanPhams on sp.MaLoaiSanPham equals lsp.Id
                               where  lsp.Id ==1
                               select new
                               {
                                   Id = sp.Id,
                                   TenSanPham = sp.TenSanPham,
                                   GiaBan = sp.GiaBan,
                                   KhuyenMai = sp.KhuyenMai,
                                   Anh = sp.Anh,
                                   TenLoaiSanPham = lsp.TenLoaiSanPham
                               }).OrderByDescending(x => x.Id);


            int i = 0;

            foreach (var item in listproduct)
            {
                i++;
                var model = new SanPhamModel();
                model.Id = item.Id;
                model.STT = i;
                model.Ten = item.TenSanPham;
                model.TenLoai = item.TenLoaiSanPham;
                model.GiaBan = item.GiaBan;
                model.KhuyenMai = item.KhuyenMai;
                model.Anh = item.Anh;

                list.Add(model);

            }
            return View(list);
        }

        public ActionResult CreateSanPhamPhu()
        {
            var model = new SanPhamModel();
            model.SelectMaLoai = new SelectList(db.LoaiSanPhams.Where(x => x.SanPhamChinh == 2), "Id", "TenLoaiSanPham", 1);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateSanPhamPhu(SanPhamModel model)
        {
            SanPham sanpham = new SanPham();
            sanpham.TenSanPham = model.Ten;
            sanpham.MaLoaiSanPham = model.MaLoai;
            sanpham.GiaBan = model.GiaBan;
            sanpham.KhuyenMai = model.GiaBan;
            sanpham.Anh = model.Anh;
            sanpham.MoTa = model.MoTa;
            var dao = new ProductDao();
            dao.Insert(sanpham);
            return RedirectToAction("SanPhamPhu", "SanPhamAdmin");
        }

        public ActionResult EditProductPhu(int? productid)
        {
            var product = new ProductDao().viewDetail(productid);
            var model = new SanPhamModel();
            model.SelectMaLoai = new SelectList(db.LoaiSanPhams.Where(x => x.SanPhamChinh == 2), "Id", "TenLoaiSanPham", product.MaLoaiSanPham);
            model.Id = product.Id;
            model.Ten = product.TenSanPham;
            model.Anh = product.Anh;
            model.MaLoai = product.MaLoaiSanPham;
            model.GiaBan = product.GiaBan;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProductPhu(SanPhamModel model)
        {
            SanPham book = db.SanPhams.FirstOrDefault(x => x.Id == model.Id);
            book.TenSanPham = model.Ten;
            book.MaLoaiSanPham = model.MaLoai;
            book.GiaBan = model.GiaBan;
            book.Anh = model.Anh;
            db.SaveChanges();
            return RedirectToAction("SanPhamPhu", "SanPhamAdmin");

        }
        public ActionResult DeleteProductPhu(int productid)
        {
            new ProductDao().Delete(productid);
            return RedirectToAction("SanPhamPhu", "SanPhamAdmin");
        }

    }
}