using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
namespace ChuoiCuaHangTraSua.Controllers.NguoiDung
{
    public class HomeController : Controller
    {
        // GET: Home
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult TenDangNhap()
        {
            return PartialView();
        }
        public ActionResult ThongTinTaiKhoan()
        {
            var model = new KhachHangModel();
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            var khachhang = new KhachHangDao().getById(session.UserId);
            model.Id = khachhang.Id;
            model.HoTen = khachhang.HoTen;
            model.Email = khachhang.Email;
            model.SDT = khachhang.SDT;
            model.DiaChi = khachhang.DiaChi;
            model.UserName = khachhang.TenDangNhap;
            return View(model);
        }
        [HttpPost]
        public ActionResult ThongTinTaiKhoan(KhachHangModel model)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            var khachhang = db.KhachHangs.Find(session.UserId);
            khachhang.HoTen = model.HoTen;
            khachhang.DiaChi = model.DiaChi;
            khachhang.SDT = model.SDT;
            db.SaveChanges();
            ViewBag.ThongTinTaiKhoan = "Bạn đã thay đổi thông tin thành công";
            return View();
        }
        public ActionResult MuaNgay(int productid)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if(session != null)
            {
                ProductDao dao = new ProductDao();
                ViewBag.Topping = dao.getTopping();
                ViewBag.Duong = dao.getDuong();
                ViewBag.Da = dao.getDa();
                ViewBag.Size = dao.getSize();
                var product = dao.getByid(productid);
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        
       public PartialViewResult GioHang()
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if (session != null)
            {
                var cart = new CartDao().GetProductsByIdUser(session.UserId);
                return PartialView(cart);
            }
            return PartialView();
            
        }
        public ActionResult DoiMatKhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(KhachHangModel model)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if(session != null)
            {
                var khachhang = db.KhachHangs.Find(session.UserId);
                khachhang.MatKhau =ChuoiCuaHangTraSua.Common.Encrytor.MD5Hash( model.MatKhau);
                db.SaveChanges();
                ViewBag.DoiMatKhau = "Bạn đã đổi mật khẩu thành công";
            }
            
            return View();
        }
        public ActionResult PhanHoi(string noidung)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            var phanhoi = new PhanHoi();
            DateTime now = DateTime.Now;
            phanhoi.NoiDung = noidung;
            phanhoi.ThoiGian = now;
            phanhoi.MaKhachHang = session.UserId;
            phanhoi.DaXem = 0;
            db.PhanHois.Add(phanhoi);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
    }
}