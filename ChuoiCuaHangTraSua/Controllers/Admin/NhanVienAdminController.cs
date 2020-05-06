using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
namespace ChuoiCuaHangTraSua.Controllers.Admin
{
    public class NhanVienAdminController : Controller
    {
        // GET: NhanVienAdmin
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var model = new List<NhanVienModel>();
            var list = db.NhanViens.ToList();
            int i = 0;
            foreach (var item in list)
            {
                i++;
                var itemmodel = new NhanVienModel();
                itemmodel.Id = item.Id;
                itemmodel.STT = i;
                itemmodel.HoTen = item.HoTen;
                itemmodel.DiaChi = item.DiaChi;
                itemmodel.SDT = item.SDT;
                itemmodel.TenDangNhap = item.TenDangNhap;
                itemmodel.TenChiNhanh = db.ChiNhanhs.FirstOrDefault(x => x.Id == item.MaChiNhanh).TenChiNhanh;
                itemmodel.TenChucVu = db.ChucVus.FirstOrDefault(x => x.Id == item.MaChucVu).TenChucVu;
                itemmodel.Luong = db.ChucVus.FirstOrDefault(x => x.Id == item.MaChucVu).Luong;
                model.Add(itemmodel);
            }
            
            return View(model);
        }
        public ActionResult CreateNhanVien()
        {
            var model = new NhanVienModel();
            model.SelectChiNhanh = new SelectList(db.ChiNhanhs, "Id", "TenChiNhanh", 0);
            model.SelectChucVu = new SelectList(db.LoaiSanPhams, "Id", "TenChucVu", 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateNhanVien(NhanVienModel model)
        {
            var nhanvien = new ChuoiCuaHangTraSua.Models.EF.NhanVien();
            nhanvien.HoTen = model.HoTen;
            nhanvien.TenDangNhap = model.TenDangNhap;
            nhanvien.MatKhau = ChuoiCuaHangTraSua.Common.Encrytor.MD5Hash("123456");
            nhanvien.MaChucVu = model.MaChucVu;
            nhanvien.MaChiNhanh = model.MaChiNhanh;
            db.NhanViens.Add(nhanvien);
            db.SaveChanges();
            return RedirectToAction("Index", "NhanVienAdmin");
        }

        public ActionResult DeleteNhanVien(int manhanvien)
        {
            var nhanvien = db.NhanViens.Find(manhanvien);
            db.NhanViens.Remove(nhanvien);
            db.SaveChanges();
            return RedirectToAction("Index", "NhanVienAdmin");
        }
    }
}