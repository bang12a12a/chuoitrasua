using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class NhanVienModel
    {
        public int STT { set; get; }

        public int Id { set; get; }
        [Required(ErrorMessage = "Mời nhập tên nhân viên")]
        [Display(Name = "Tên nhân viên")]
        public string HoTen { set; get; }
        [Required(ErrorMessage = "Mời nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string SDT { set; get; }
        [Required(ErrorMessage = "Mời nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { set; get; }
        [Required(ErrorMessage = "Mời nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { set; get; }


        [StringLength(50)]
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu cũ")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string MatKhau { set; get; }


        [StringLength(50)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu xác nhận mật khẩu")]
        public string XacNhanMatKhau { set; get; }

        [Required(ErrorMessage = "Mời chọn chức vụ")]
        [Display(Name = "Chức vụ")]
        public int MaChucVu { set; get; }
        [Required(ErrorMessage = "Mời chọn chi nhánh")]
        [Display(Name = "Chi nhánh")]
        public int MaChiNhanh { set; get; }

        public string TenChucVu { set; get; }

        public string TenChiNhanh { set; get; }
        [Required(ErrorMessage = "Mời nhập lương")]
        [Display(Name = "Lương")]
        public int? Luong { set; get; }

        public SelectList SelectChiNhanh { set; get; }

        public SelectList SelectChucVu { set; get; }

    }
}