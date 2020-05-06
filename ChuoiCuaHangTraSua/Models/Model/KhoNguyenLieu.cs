using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class KhoNguyenLieu
    {
        public int Id { set; get; }
        public int STT { set; get; }

        public string TenDonVi { set; get; }

        public string TenNguyenLieu { set; get; }

        public int? SoLuong { set; get; }
    }
}