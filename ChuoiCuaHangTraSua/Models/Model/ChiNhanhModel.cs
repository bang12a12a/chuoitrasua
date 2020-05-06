using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class ChiNhanhModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập tên chi nhánh")]
        [Display(Name = "Tên chi nhánh")]
        public string TenChiNhanh { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { set; get; }
        [Display(Name = "Ghi Chú")]
        public string GhiChu { set; get; }

        public int STT { set; get; }

    }
}