using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class NhomSanPhamModel
    {
        public int? Id { set; get; }

        [Required(ErrorMessage = "Mời nhập tên")]
        [Display(Name = "Tên")]
        public string Ten { set; get; }

        public int STT { set; get; }

        [Required(ErrorMessage = "Mời chọn loại sản phẩm")]
        [Display(Name = "Loại sản phẩm")]
        public SelectList SelectSPChinhPhu { set; get; }

        [Required(ErrorMessage = "Mời chọn loại sản phẩm")]
        [Display(Name = "Loại sản phẩm")]
        public int? SPChinhPhu { set; get; }

        public string TenChinhPhu { set; get; }
    }
}