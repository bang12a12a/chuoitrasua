using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Common;
using PagedList;

namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class KhuyenMaiAdminController : Controller
    {
        // GET: KhuyenMaiAdmin

        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model = new List<KhuyenMaiModel>();
            var list = db.KhuyenMais.ToList();
            var i = 0;
            foreach(var item in list)
            {
                i++;
                var khuyenmai = new KhuyenMaiModel();
                khuyenmai.STT = i;
                khuyenmai.Id = item.Id;
                khuyenmai.Ten = item.TenKhuyenMai;
                var lkm =(LoaiKhuyenMai) db.LoaiKhuyenMais.Where(x => x.Id == item.MaLoaiKhuyenMai).FirstOrDefault();
                khuyenmai.TenLoaiKhuyenMai = lkm.TenLoaiKhuyenMai;
                khuyenmai.PhanTram = item.PhanTram;
                khuyenmai.NgayBatDau = item.NgayBatDau;
                khuyenmai.NgayKetThuc = item.NgayKetThuc;
                khuyenmai.Status = item.Status;
                model.Add(khuyenmai);
            }
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CreateKhuyenMai()
        {
            
            var model = new KhuyenMaiModel();
            model.ListLoaiSanPham = db.LoaiSanPhams.Where(x=>x.Id==1 || x.SanPhamChinh==1).ToList();
            model.SelectLoaiKM = new SelectList(db.LoaiKhuyenMais, "Id", "TenLoaiKhuyenMai", 0);
            return View(model);
            
        }
        [HttpPost]
        public ActionResult CreateKhuyenMai(KhuyenMaiModel model)
        {
            var khuyenmai = new KhuyenMai();
            var makhuyenmai = new RandomId().MaNgauNhien_SoChu(5);
            khuyenmai.Id = makhuyenmai;
            khuyenmai.TenKhuyenMai = model.Ten;
            khuyenmai.PhanTram = model.PhanTram;
            khuyenmai.MaLoaiKhuyenMai = model.MaLoaiKhuyenMai;
            khuyenmai.NgayBatDau = model.NgayBatDau;
            khuyenmai.NgayKetThuc = model.NgayKetThuc;
            khuyenmai.Status = 1;
            
            var listloaisanpham = Request.Form["listlspdc"];
            if(listloaisanpham != "")
            {
                string[] listlsp = listloaisanpham.Split(',');
                foreach (var item in listlsp)
                {
                    var maloaisp = int.Parse(item);
                    var listsp = db.SanPhams.Where(x => x.MaLoaiSanPham == maloaisp).ToList();
                    foreach (var itemsp in listsp)
                    {
                        var product = db.SanPhams.Find(itemsp.Id);
                        product.KhuyenMai = product.GiaBan - (product.GiaBan * model.PhanTram / 100);
                        db.SaveChanges();
                    }
                }
            }
            else
            {

            }
            khuyenmai.MoTa = listloaisanpham;



            db.KhuyenMais.Add(khuyenmai);
            db.SaveChanges();
            return RedirectToAction("Index", "KhuyenMaiAdmin");
        }
        public ActionResult UpdateKhuyenMai(string makhuyenmai)
        {
            var khuyenmai =(KhuyenMai) db.KhuyenMais.FirstOrDefault(x => x.Id == makhuyenmai);
            khuyenmai.Status = 0;
            if(khuyenmai.MaLoaiKhuyenMai == 2)
            {
                var listnhomsp = khuyenmai.MoTa;
                string[] listlsp = listnhomsp.Split(',');
                foreach(var item in listlsp)
                {
                    var maloaisanpham = int.Parse(item);
                    var lstsanpham =db.SanPhams.Where(x => x.MaLoaiSanPham == maloaisanpham).ToList();
                    foreach(var sanpham in lstsanpham)
                    {
                        sanpham.KhuyenMai = sanpham.GiaBan;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "KhuyenMaiAdmin");
        }
    }
}