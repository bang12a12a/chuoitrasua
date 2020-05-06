using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class NguyenLieuModel
    {
        public int Id { set; get; }

        public int STT { set; get; }

        [Required(ErrorMessage = "Mời nhập tên nguyên liệu")]
        [Display(Name = "Tên nguyên liệu")]
        public string TenNguyenLieu { set; get; }

        
    }
}