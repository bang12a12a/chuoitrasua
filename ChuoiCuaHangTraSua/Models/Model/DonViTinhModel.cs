using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class DonViTinhModel
    {
        public int Id { set; get; }

        public int STT { set; get; }

        [Required(ErrorMessage = "Mời nhập tên đơn vị tính")]
        [Display(Name = "Tên đơn vị tính")]
        public string TenDonViTinh { set; get; }
    }
}