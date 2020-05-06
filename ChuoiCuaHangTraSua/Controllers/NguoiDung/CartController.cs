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
    public class CartController : Controller
    {
        // GET: Cart
        TraSuaEntities db = new TraSuaEntities();
        public ActionResult Index()
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            var list = new List<GioHangModel>();
            if(session != null)
            {
                var cart = new CartDao().GetProductsByIdUser(session.UserId);
                foreach (var item in cart)
                {
                    var cartitem = new GioHangModel();
                    
                    var product = new ProductDao().viewDetail(item.MaSanPham);
                    var categorydao = new CategoryDao();
                    if(categorydao.getSPChinh(product.Id) == 1)
                    {
                        cartitem.MaSanPham = item.MaSanPham;
                        cartitem.Id = item.Id;
                        cartitem.TenSanPham = product.TenSanPham;
                        cartitem.Anh = product.Anh;
                        cartitem.SoLuong = item.SoLuong;
                        cartitem.GiaBan = new CartDao().Tien1LyTraSua(session.UserId, product.Id);
                        cartitem.ThanhTien = cartitem.GiaBan * cartitem.SoLuong;
                        cartitem.MoTa = new CartDao().getMoTa(product.Id, session.UserId);
                        list.Add(cartitem);
                    }
                    
                }
                return View(list);
            }
            else
            {
                return View("/SanPham/Index");
            }
            
        }
        public ActionResult CreateCart(string listproduct)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            string[] productsid=listproduct.Split(',');
            var session_spthu = (ChuoiCuaHangTraSua.Common.SanPhamThu)Session[ChuoiCuaHangTraSua.Common.Constants.SANPHAMTHU_SESSION];
            int sp_thu = 0;
            if (session_spthu != null)
            {
                sp_thu = session_spthu.SanPham_Thu +1;
            }
            else
            {
                sp_thu = 1;
               
            }
            var sanpham_thu = new ChuoiCuaHangTraSua.Common.SanPhamThu();
            sanpham_thu.SanPham_Thu = sp_thu;
            Session.Add(ChuoiCuaHangTraSua.Common.Constants.SANPHAMTHU_SESSION, sanpham_thu);

            foreach (var item in productsid)
            {

                GioHang giohang = new GioHang();
                giohang.MaKhachHang = session.UserId;
                giohang.MaSanPham =int.Parse(item);
                giohang.SoLuong = 1;
                giohang.ThuocSanPham =int.Parse( productsid[0]);
                giohang.SanPhamThu = sp_thu;
                new CartDao().Insert(giohang);
                
            }
            return Redirect("/SanPham/Index");
        }
        public ActionResult UpdateCart(int productid, int? soluong)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if (session != null)
            {
                var userid = session.UserId;
                var list = new CartDao().layDSSP(productid, userid);
                foreach(var item in list)
                {
                    var cart = new GioHang();
                    new CartDao().Update(item.Id, soluong);
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult DeleteCart(int productid)
        {
            var session = (ChuoiCuaHangTraSua.Common.UserLogin)Session[ChuoiCuaHangTraSua.Common.Constants.USER_SESSION];
            if (session != null)
            {
                var userid = session.UserId;
                var list = new CartDao().layDSSP(productid, userid);
                foreach (var item in list)
                {
                    var cart = new GioHang();
                    new CartDao().Delete(item.Id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}