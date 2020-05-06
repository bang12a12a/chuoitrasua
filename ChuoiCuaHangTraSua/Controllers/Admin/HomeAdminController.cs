using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Common;

namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class HomeAdminController : Controller
    {
        // GET: HomeAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var session_user = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if(session_user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public PartialViewResult TongDoanhThu()
        {
            var list = db.HoaDonBans.ToList();
            ViewBag.TongDoanhThu = list.Sum(x => x.TongTien);
            return PartialView();
        }
        public PartialViewResult DonHang()
        {
            var list = db.HoaDonBans.ToList();
            ViewBag.DonHang = list.Count;
            return PartialView();
        }
        public PartialViewResult PhanHoi()
        {
            var list = db.PhanHois.ToList();
            ViewBag.PhanHoi = list.Count;
            return PartialView();
        }
        public PartialViewResult KhachHangOnline()
        {
            var list = db.KhachHangs.Where(x=>x.Id != 3).ToList();
            ViewBag.KhachHang = list.Count;
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult TopPhanHoi()
        {
            var list = (from ph in db.PhanHois
                        join kh in db.KhachHangs on ph.MaKhachHang equals kh.Id
                        
                        select new
                        {
                            Id = ph.Id,
                            MaKhachHang = ph.MaKhachHang,
                            NoiDung = ph.NoiDung,
                            TenKhachHang = kh.TenDangNhap,
                            DaXem = ph.DaXem
                        }).OrderByDescending(x => x.Id).Take(5).ToList();

            //var list = db.PhanHois.OrderByDescending(x => x.Id).Take(5).ToList();
            var model = new List<PhanHoiModel>();
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
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult Top5KhachHangMuaNhieuNhat()
        {
            var listTopKhachHang = new List<HoaDonBanModel>();
            var list = (from hdb in db.HoaDonBans where hdb.MaKhach != 4
                        group hdb by hdb.MaKhach into g
                        select new
                        {
                            MaKhach = g.FirstOrDefault().MaKhach,
                            TongTien = g.Sum(x => x.TongTien),
                            TongHoaDon = g.Count()
                        }).OrderByDescending(x => x.TongTien).ToList();
            
            foreach (var item in list)
            {
                
                var itemmodel = new HoaDonBanModel();
                
                itemmodel.MaKhach = item.MaKhach;
                var khachhang = new KhachHangDao().viewDetail(item.MaKhach);
                itemmodel.TongSoHoaDon = item.TongHoaDon;
                itemmodel.TenDangNhap = khachhang.TenDangNhap;
                itemmodel.TongTienDaMua = item.TongTien;

                listTopKhachHang.Add(itemmodel);
            }

            return PartialView(listTopKhachHang.Take(5));
        }
        [ChildActionOnly]
        public PartialViewResult TopSPBanChay()
        {
            var listTopProduct = new List<SanPhamModel>();
             var list = (from sp in db.SanPhams
                        join cthdb in db.ChiTietHDBs on sp.Id equals cthdb.MaSanPham
                        group cthdb by cthdb.MaSanPham into g
                        select new SanPhamModel
                        {
                            Id= g.FirstOrDefault().MaSanPham,
                            TongSL = g.Sum(x => x.SoLuong),
                            
                        }).OrderByDescending(x => x.TongSL).ToList();
            var i = 0;
            foreach(var item in list)
            {
                  i++;
                var itemmodel = new SanPhamModel();
                itemmodel.STT = i;
                itemmodel.Id = item.Id;
                var product = new ProductDao().viewDetail(item.Id);

                itemmodel.Ten = product.TenSanPham;
                itemmodel.Anh = product.Anh;
                itemmodel.TongSL = item.TongSL;
                if(new CategoryDao().getSPChinh(itemmodel.Id) == 1)
                {
                    listTopProduct.Add(itemmodel);
                }
                
            }
            
            return PartialView(listTopProduct.Take(5));
        }
        public PartialViewResult HoaDonChuaPheDuyet()
        {
            var hoadonchuapheduyet = db.HoaDonBans.Where(x => x.Duyet == 0).ToList();
            return PartialView(hoadonchuapheduyet);
        }
        public PartialViewResult PhanHoiChuaDoc()
        {
            var list = db.PhanHois.Where(x => x.DaXem == 0).ToList();
            var model = new List<PhanHoiModel>();
            DateTime now = DateTime.Now;
            foreach(var item in list)
            {
                var itemmodel = new PhanHoiModel();
                var khachhang = db.KhachHangs.FirstOrDefault(x => x.Id == item.MaKhachHang);
                itemmodel.UserName = khachhang.TenDangNhap;
                itemmodel.PhanHoiTu = new LamTronThoiGian().LamTron(now, item.ThoiGian);
                model.Add(itemmodel);
            }
            return PartialView(model);
        }
    }
}