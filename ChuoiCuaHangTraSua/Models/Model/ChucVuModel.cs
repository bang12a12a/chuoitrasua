using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class ChucVuModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập tên chức vụ")]
        [Display(Name = "Tên chức vụ")]
        public string TenChucVu { set; get; }
        [Required(ErrorMessage = "Yêu cầu nhập lương")]
        [Display(Name = "Lương")]
        public int? Luong { set; get; }

        public int STT { set; get; }
    }
}