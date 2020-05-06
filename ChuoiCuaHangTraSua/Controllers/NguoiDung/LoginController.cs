using ChuoiCuaHangTraSua.Common;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.EF;
using System.Configuration;

namespace ChuoiCuaHangTraSua.Controllers.NguoiDung
{
    public class LoginController : Controller
    {
        // GET: Login
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            string UserName = string.Empty;
            string Password = string.Empty;
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserName = reqCookies["UserName"].ToString();
                Password = reqCookies["Password"].ToString();
            }
            var model = new LoginModel();
            model.Username = UserName;
            model.Password = Password;
            return View(model);
            
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username, Encrytor.MD5Hash(model.Password));
                if (result)
                {
                    var user = dao.GetById(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserId = user.Id;
                    userSession.UserName = user.TenDangNhap;
                    Session.Add(Common.Constants.USER_SESSION, userSession);
                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["UserName"] = model.Username;
                    userInfo["Password"] = model.Password;
                    userInfo.Expires.Add(new TimeSpan(1, 0, 0));
                    Response.Cookies.Add(userInfo);
                    if(user.TenDangNhap == "admin")
                    {
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng");
                }
            }
            return View("Index");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHangModel model)
        {
            
                var dao = new UserDao();
                if (dao.CheckUserName(model.TenDangNhap))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else
                 if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new KhachHang();
                    user.HoTen = model.HoTen;
                    user.TenDangNhap = model.TenDangNhap;
                    user.Email = model.Email;
                    user.MatKhau = Encrytor.MD5Hash(model.MatKhau);
                    user.SDT = model.SDT;
                    user.DiaChi = model.DiaChi;
                    
                    var result = dao.Insert(user);

                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                    }
                }

            
            return View();
        }
        public ActionResult Logout()
        {
            Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION] = null;
            return Redirect("/Home/Index");
        }
        public ActionResult QuenMatKhau()
        {

            return View();
        }
        [HttpPost]
        public ActionResult QuenMatKhau(KhachHangModel model)
        {
           
            var dao = new KhachHangDao();
            if (dao.CheckEmail(model.Email, model.TenDangNhap))
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/resetpassword.html"));
                content = content.Replace("{{CustomerName}}", model.TenDangNhap);
                content = content.Replace("{{Password}}", "@123456");
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(model.Email, "Đổi mật khẩu từ Estore", content);

               

                var khachhang = dao.getKhachHang(model.Email, model.TenDangNhap);
                var customer = db.KhachHangs.Find(khachhang.Id);
                customer.MatKhau = ChuoiCuaHangTraSua.Common.Encrytor.MD5Hash("@123456");
                db.SaveChanges();
                ViewBag.DoiMatKhau = "Mời bạn kiểm tra email";
            }
            else
            {
                ViewBag.SaiEmail = "Tên đăng nhập và email không khớp";
            }
           
            return View();
        }
        public ActionResult DangNhapNhanVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapNhanVien(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new NhanVienDao();
                var result = dao.DangNhapNhanVien(model.Username, Encrytor.MD5Hash(model.Password));
                if (result)
                {
                    var nhanvien = dao.getByName(model.Username);
                    var nhanvien_Session = new NhanVienLogin();
                    nhanvien_Session.UserId = nhanvien.Id;
                    nhanvien_Session.UserName = nhanvien.TenDangNhap;
                    Session.Add(Common.Constants.NHANVIEN_SESSION, nhanvien_Session);
                    return RedirectToAction("Index", "HomeNhanVien");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng");
                }
            }
            return RedirectToAction("DangNhapNhanVien","Login");
        }
    }
}